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
        public string mDescription { get; set; }
        public double mUnitPrice { get; set; }
        public bool mIsAvailable { get; set; }

        public Product() : base() {}

        public Product(ProductMakerDTO maker) : this()
        {
            name = maker.name;
            mDescription = maker.description;
            mUnitPrice = maker.unitPrice;
            mIsAvailable = maker.isAvailable;
        }
    }
}
