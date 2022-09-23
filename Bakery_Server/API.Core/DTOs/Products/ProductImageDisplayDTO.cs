using API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.DTOs.Products
{
    public class ProductImageDisplayDTO
    {
        public string productImageId { get; set; }
        public string productId { get; set; }
        public string imageSource { get; set; }

        public ProductImageDisplayDTO() { }

        public ProductImageDisplayDTO(ProductImage i)
        {
            productImageId  = i.mID;
            productId       = i.product.mID;
            imageSource     = i.imageSource;
        }
    }
}
