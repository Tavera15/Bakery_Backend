using API.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Contracts
{
    public interface IBasketItemService
    {
        Task<IEnumerable<BasketItem>> GetBasketItemsAsync(HttpContext httpContext);
        Task<BasketItem> FindBasketItemAsync(HttpContext httpContext, string entityId);
        Task<BasketItem> InsertNewBasketItemAsync(HttpContext httpContext, BasketItem newEntity);
        Task<int> DeleteAsync(HttpContext httpContext, string entityId);
        Task<BasketItem> UpdateEntityAsync(HttpContext httpContext, string entityId, BasketItem updatedData);
    }
}
