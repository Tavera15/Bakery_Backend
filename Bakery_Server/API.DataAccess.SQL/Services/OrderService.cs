using API.Core.Contracts;
using API.Core.CustomExceptions;
using API.Core.DTOs.Orders;
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
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly DbSet<BakeryOrder> _orderDBset;
        private readonly DbSet<OrderItem> _orderItemDBset;

        public OrderService(DataContext c)
        {
            _context = c;
            _orderDBset = c.Set<BakeryOrder>();
            _orderItemDBset = c.Set<OrderItem>();
        }

        public IEnumerable<BakeryOrder> GetBakeryOrders()
        {
            IEnumerable<BakeryOrder> allOrders = _orderDBset.Include(x => x.orderItems);
            
            return allOrders.OrderByDescending(x => x.mTimeModified);
        }

        public async Task<BakeryOrder> FindOrderAsync(string orderId)
        {
            BakeryOrder order = await _orderDBset.Include(x => x.orderItems).SingleOrDefaultAsync(x => x.mID == orderId);

            if (order == null)
            {
                throw new EntityNotFoundException("Order not found.");
            }

            return order;
        }

        public async Task<BakeryOrder> CreateNewOrder(OrderMakerDTO orderDetails, Basket basket)
        {
            if (basket.basketItems.Count < 1)
            {
                throw new Exception("Shopping cart is empty");
            }

            // Create a new order
            BakeryOrder newOrder = new BakeryOrder(orderDetails);
            _orderDBset.Add(newOrder);
            double grandTotal = 0.00f;

            // Create order items using basket items, and attach them to parent order
            foreach (BasketItem basketItem in basket.basketItems)
            {
                OrderItem orderItem = new OrderItem(basketItem, newOrder);
                grandTotal += orderItem.total;
                _orderItemDBset.Add(orderItem);
            }

            newOrder.grandTotal = grandTotal.ToString("0.00");
            await _context.SaveChangesAsync();
            return newOrder;
        }
    }
}
