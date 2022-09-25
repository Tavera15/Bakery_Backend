using API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.DTOs.Orders
{
    public class OrderItemDisplayDTO
    {
        public string productName { get; set; }
        public string productDesc { get; set; }
        public double productPrice { get; set; }
        public int quantity { get; set; }
        public double total { get; set; }

        // Set customizations
        public string sizeSelected { get; set; }

        public OrderItemDisplayDTO() { }

        public OrderItemDisplayDTO(OrderItem o)
        {
            productName = o.name;
            productDesc = o.productDesc;
            productPrice = o.productPrice;
            quantity = o.quantity;
            total = o.total;
            sizeSelected = o.sizeSelected;
        }
    }
}
