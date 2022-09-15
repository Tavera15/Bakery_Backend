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
    }
}
