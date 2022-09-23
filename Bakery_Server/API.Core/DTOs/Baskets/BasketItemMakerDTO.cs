using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.DTOs.Baskets
{
    public class BasketItemMakerDTO
    {
        public string productId { get; set; }
        public int quantity { get; set; }
        public string sizeSelected { get; set; }
    }
}
