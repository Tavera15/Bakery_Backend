using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public virtual BakeryOrder parentOrder { get; set; }
        public string productDesc { get; set; }
        public double productPrice { get; set; }
        public int quantity { get; set; }
        public double total { get; set; }
        public string sizeSelected { get; set; }

        public OrderItem() { }

        public OrderItem(BasketItem b, BakeryOrder order)
        {
            parentOrder = order;
            name = b.product.name;
            productDesc = b.product.mDescription;
            productPrice = b.product.mUnitPrice;
            quantity = b.qty;
            total = b.product.mUnitPrice * b.qty;
            sizeSelected = b.sizeSelected;
        }
    }
}
