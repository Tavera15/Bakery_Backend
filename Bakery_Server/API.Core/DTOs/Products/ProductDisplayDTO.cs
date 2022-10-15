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
        public string lastTimeModified { get; set; }
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
            lastTimeModified    = String.Format("{0:MMMM dd, yyyy}", product.mTimeEntered);
            images              = product?.productImages.Select(i => new ProductImageDisplayDTO(i));
        }
    }
}
