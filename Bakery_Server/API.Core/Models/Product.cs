using API.Core.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class Product : BaseEntity
    {
        public string mProductType { get; set; } = "Treats";
        public string mDescription { get; set; }
        public double mUnitPrice { get; set; }
        public bool mIsAvailable { get; set; }
        public string mAvailableSizes { get; set; }
        public virtual ICollection<ProductImage> productImages { get; set; } = new List<ProductImage>();


        public Product() : base() {}

        public Product(ProductMakerDTO maker) : this()
        {
            name                = maker.name;
            mProductType        = maker.productType;
            mDescription        = maker.description;
            mUnitPrice          = maker.unitPrice;
            mIsAvailable        = maker.isProductAvailable;
            mAvailableSizes     = maker.availableSizes;
        }
    }
}
