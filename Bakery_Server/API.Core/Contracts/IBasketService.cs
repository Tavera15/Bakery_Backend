using API.Core.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Contracts
{
    public interface IBasketService
    {
        public Task<Basket> CreateNewBasket();
        public Task<Basket> GetBasket(HttpContext httpContext);
        public Task<Basket> ClearBasket(HttpContext httpContext);
    }
}
