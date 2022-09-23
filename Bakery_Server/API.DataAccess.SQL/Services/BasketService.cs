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
    public class BasketService : IBasketService
    {
        protected readonly DataContext _context;
        protected readonly DbSet<Basket> _basketDBset;
        protected readonly DbSet<BasketItem> _basketItemsDBset;
        private const string HEADER_BASKET_ID_NAME = "BasketId";

        public BasketService(DataContext c)
        {
            _context = c;
            _basketDBset = c.Set<Basket>();
            _basketItemsDBset = c.Set<BasketItem>();
        }

        public async Task<Basket> CreateNewBasket()
        {
            Basket newBasket = new Basket();
            _context.Add(newBasket);
            await _context.SaveChangesAsync();

            return newBasket;
        }

        public async Task<Basket> GetBasket(HttpContext httpContext)
        {
            var allBaskets = _basketDBset.Include(b => b.basketItems).ThenInclude(p => p.product);

            string basketId = httpContext.Request.Headers[HEADER_BASKET_ID_NAME];
            Basket basket = !String.IsNullOrWhiteSpace(basketId)
                ? await allBaskets.FirstOrDefaultAsync(b => b.mID == basketId)
                : await CreateNewBasket();

            if(basket == null)
            {
                throw new EntityNotFoundException();
            }

            return basket;
        }

        public async Task<Basket> ClearBasket(HttpContext httpContext)
        {
            Basket basket = await GetBasket(httpContext);

            foreach(BasketItem basketItem in basket.basketItems)
            {
                BasketItem target = await _basketItemsDBset.FindAsync(basketItem.mID);

                if(target != null)
                {
                    _basketItemsDBset.Remove(target);
                }
            }
            
            await _context.SaveChangesAsync();
            return basket;
        }
    }
}
