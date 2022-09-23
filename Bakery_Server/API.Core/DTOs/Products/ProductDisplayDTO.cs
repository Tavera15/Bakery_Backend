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
        public string description { get; set; }
        public double unitPrice { get; set; }
        public bool isAvailable { get; set; }
        public DateTime lastTimeModified { get; set; }

        public ProductDisplayDTO() { }

        public ProductDisplayDTO(Product product)
        {
            id                  = product.mID;
            name                = product.name;
            description         = product.mDescription;
            unitPrice           = product.mUnitPrice;
            isAvailable         = product.mIsAvailable;
            lastTimeModified    = product.mTimeModified;
        }
    }
}
