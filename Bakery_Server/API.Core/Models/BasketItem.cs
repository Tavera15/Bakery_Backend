using API.Core.Contracts;
using API.Core.DTOs.Baskets;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class BasketItem : BaseEntity
    {
        public virtual Basket basket { get; set; }
        public virtual Product product { get; set; }
        public int qty { get; set; }
        public string sizeSelected { get; set; }

        public BasketItem() { }

        public BasketItem(BasketItemMakerDTO v)
        {
            qty = v.quantity;
            sizeSelected = v.sizeSelected;
        }

        public BasketItem(BasketItemMakerDTO v, Basket userBasket) : this(v)
        {
            basket = userBasket;
        }

        public static bool operator ==(BasketItem a, BasketItem b)
        {
            // Test if both are null or the same instance, then return true
            if (ReferenceEquals(a, b))
                return true;

            // If only one of them null return false
            if (((object)a == null) || ((object)b == null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BasketItem a, BasketItem b)
        {
            // Test if both are null or the same instance, then return true
            if (ReferenceEquals(a, b))
                return false;

            // If only one of them null return false
            if (((object)a == null) || ((object)b == null))
                return true;

            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            BasketItem other = (BasketItem)obj;

            return (
                this.product.mID == other.product.mID &&
                this.sizeSelected == other.sizeSelected
            );
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public class BasketItemValidation : AbstractValidator<BasketItem>
    {
        private readonly IRepository<Product> _productContext;

        public BasketItemValidation(IRepository<Product> productRepository)
        {
            _productContext = productRepository;

            RuleFor(item => item.qty)
                .GreaterThan(0)
                .WithMessage("Quanitity must be greater than 0");

            RuleFor(item => item.product)
                .NotNull()
                .WithMessage("Product does not exist");

            RuleFor(item => item.sizeSelected)
                .Must((item, sizeSelected) => ValidateSizeSelected(item.product, sizeSelected))
                .WithMessage("Invalid size selected");
        }

        private bool ValidateSizeSelected(Product product, string sizeSelected)
        {
            if (String.IsNullOrWhiteSpace(product.mAvailableSizes)) { return true; }

            string[] productSizes = product.mAvailableSizes.Split(',');
         
            return productSizes.Contains(sizeSelected);
        }
    }
}
