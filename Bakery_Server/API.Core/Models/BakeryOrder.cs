using API.Core.Contracts;
using API.Core.DTOs.Orders;
using FluentValidation;
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

    public class OrderValidation : AbstractValidator<BakeryOrder>
    {
        public OrderValidation()
        {
            RuleFor(item => item.name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Customer name required");

            RuleFor(item => item.addressLine1)
                .NotNull()
                .NotEmpty()
                .WithMessage("Address Line 1 required");

            RuleFor(item => item.city)
                .NotEmpty()
                .NotNull()
                .WithMessage("City required");

            RuleFor(item => item.state)
                .NotEmpty()
                .NotNull()
                .WithMessage("State required");

            RuleFor(item => item.zipCode)
                .NotEmpty()
                .NotNull()
                .WithMessage("Zipcode required");

            RuleFor(item => item.customerPhone)
                .NotEmpty()
                .NotNull()
                .WithMessage("Phone number required");

            RuleFor(item => item.customerEmail)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email required");
        }
    }
}
