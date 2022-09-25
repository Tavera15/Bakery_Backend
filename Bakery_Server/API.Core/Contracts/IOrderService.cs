using API.Core.DTOs.Orders;
using API.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Contracts
{
    public interface IOrderService
    {
        IEnumerable<BakeryOrder> GetBakeryOrders();
        Task<BakeryOrder> FindOrderAsync(string orderId);
        Task<BakeryOrder> CreateNewOrder(OrderMakerDTO orderDetails, Basket basket);
    }
}
