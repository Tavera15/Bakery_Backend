using API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.DTOs.Baskets
{
    public class BasketDisplayDTO
    {
        public IEnumerable<BasketItemDisplayDTO> basketItems { get; set; } = new List<BasketItemDisplayDTO>();
        public string basketId { get; set; }

        public BasketDisplayDTO() { }

        public BasketDisplayDTO(Basket v)
        {
            basketId = v.mID;
            basketItems = v.basketItems.Select(i => new BasketItemDisplayDTO(i));
        }
    }
}
