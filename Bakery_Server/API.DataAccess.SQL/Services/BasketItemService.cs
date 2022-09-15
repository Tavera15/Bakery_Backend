using API.Core.Contracts;
using API.Core.CustomExceptions;
using API.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.SQL.Services
{
    public class BasketItemService : IBasketItemService
    {
        protected readonly DataContext _context;
        protected readonly DbSet<BasketItem> _dbSet;
        protected readonly IBasketService _basketContext;

        public BasketItemService(DataContext c, IBasketService basketRepository)
        {
            _context = c;
            _dbSet = c.Set<BasketItem>();
            _basketContext = basketRepository;
        }

        public async Task<IEnumerable<BasketItem>> GetBasketItemsAsync(HttpContext httpContext)
        {
            Basket basket = await _basketContext.GetBasket(httpContext);

            if (basket == null)
            {
                throw new EntityNotFoundException("Basket not found");
            }

            return basket.basketItems;
        }

        public async Task<BasketItem> FindBasketItemAsync(HttpContext httpContext, string entityId)
        {
            IEnumerable<BasketItem> basketItems = await GetBasketItemsAsync(httpContext);
            BasketItem target = basketItems.FirstOrDefault(i => i.mID == entityId);

            if (target == null)
            {
                throw new EntityNotFoundException("Basket Item not found");
            }

            return target;
        }

        public async Task<BasketItem> InsertNewBasketItemAsync(HttpContext httpContext, BasketItem newEntity)
        {
            IEnumerable<BasketItem> userBasketItems = await GetBasketItemsAsync(httpContext);
            BasketItem equalBasketItem = userBasketItems.SingleOrDefault(x => x == newEntity);

            if (equalBasketItem != null)
            {
                // If an equivalant item exist, just combine the quantities
                newEntity.qty += equalBasketItem.qty;
                return await UpdateEntityAsync(httpContext, equalBasketItem.mID, newEntity);
            }
            else
            {
                // ...Else create a new basket item
                _dbSet.Add(newEntity);
                await _context.SaveChangesAsync();
                return newEntity;
            }
        }

        public async Task<BasketItem> UpdateEntityAsync(HttpContext httpContext, string entityId, BasketItem updatedData)
        {
            // Check if an equivalant basket item exists.
            IEnumerable<BasketItem> userBasketItems = await GetBasketItemsAsync(httpContext);
            BasketItem equalBasketItem = userBasketItems.SingleOrDefault(x => x == updatedData && x.mID != entityId);

            // If so, delete the basket item that you're trying to update, 
            // and combine the quantities with its equivalant
            if (equalBasketItem != null)
            {
                updatedData.qty += equalBasketItem.qty;
                await DeleteAsync(httpContext, entityId);
            }

            BasketItem basketItemToUpdate = equalBasketItem ?? await FindBasketItemAsync(httpContext, entityId);

            basketItemToUpdate.mTimeModified = DateTime.Now;
            basketItemToUpdate.qty = updatedData.qty;
            basketItemToUpdate.sizeSelected = updatedData.sizeSelected;

            await _context.SaveChangesAsync();
            return basketItemToUpdate;
        }

        public async Task<int> DeleteAsync(HttpContext httpContext, string entityId)
        {
            BasketItem target = await FindBasketItemAsync(httpContext, entityId);
            _dbSet.Remove(target);

            return await _context.SaveChangesAsync();
        }
    }
}
