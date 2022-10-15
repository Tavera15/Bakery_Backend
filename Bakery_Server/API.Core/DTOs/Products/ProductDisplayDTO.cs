using API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.DTOs.Products
{
    public class ProductDisplayDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string productType { get; set; }
        public string description { get; set; }
        public double unitPrice { get; set; }
        public bool isProductAvailable { get; set; }
        public string availableSizes { get; set; }
        public DateTime lastTimeModified { get; set; }
        public IEnumerable<ProductImageDisplayDTO> images { get; set; }

        public ProductDisplayDTO() { }

        public ProductDisplayDTO(Product product)
        {
            id                  = product.mID;
            name                = product.name;
            productType         = product.mProductType;
            description         = product.mDescription;
            unitPrice           = product.mUnitPrice;
            isProductAvailable  = product.mIsAvailable;
            availableSizes      = product.mAvailableSizes;
            lastTimeModified    = product.mTimeModified;
            images              = product?.productImages.Select(i => new ProductImageDisplayDTO(i));
        }
    }
}
