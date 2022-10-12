using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.DTOs.Products
{
    public class ProductMakerDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public double unitPrice { get; set; }
        public bool isProductAvailable { get; set; }
        public string availableSizes { get; set; }
        public IEnumerable<ProductImageMakerDTO> images { get; set; }

    }
}
