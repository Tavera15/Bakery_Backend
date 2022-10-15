using API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.DTOs.Orders
{
    public class OrderDisplayDTO
    {
        public string invoiceID { get; set; }
        public string customerName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string grandTotal { get; set; }
        public string timeCreated { get; set; }

        public IEnumerable<OrderItemDisplayDTO> orderItems { get; set; } = new List<OrderItemDisplayDTO>();

        public OrderDisplayDTO() { }

        public OrderDisplayDTO(BakeryOrder o)
        {
            invoiceID = o.mID;
            customerName = o.name;
            addressLine1 = o.addressLine1;
            addressLine2 = o.addressLine2;
            city = o.city;
            state = o.state;
            zipCode = o.zipCode;
            email = o.customerEmail;
            phone = o.customerPhone;
            grandTotal = o.grandTotal;
            timeCreated = String.Format("{0:MMMM dd, yyyy}", o.mTimeEntered);
            orderItems = o.orderItems.Select(i => new OrderItemDisplayDTO(i));
        }
    }
}
