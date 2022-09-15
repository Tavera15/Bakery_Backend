using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class Basket : BaseEntity
    {
        public virtual ICollection<BasketItem> basketItems { get; set; } = new List<BasketItem>();
    }
}
