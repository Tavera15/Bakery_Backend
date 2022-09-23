using API.Core.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class ProductImage : BaseEntity
    {
        public virtual Product product { get; set; }
        public string imageSource { get; set; }

        public ProductImage() { }

        public ProductImage(ProductImageMakerDTO v, Product p)
        {
            imageSource = v.imageSource;
            product = p;
        }
    }
}
