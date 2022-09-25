using API.Core.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class BakeryOrder : BaseEntity
    {
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }

        public virtual ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        public string grandTotal { get; set; }

        public BakeryOrder() { }

        public BakeryOrder(OrderMakerDTO orderDetails)
        {
            name = orderDetails.customerName;
            customerEmail = orderDetails.email;
            customerPhone = orderDetails.phone;
            addressLine1 = orderDetails.addressLine1;
            addressLine2 = orderDetails.addressLine2;
            city = orderDetails.city;
            state = orderDetails.state;
            zipCode = orderDetails.zipCode;
        }
    }
}
