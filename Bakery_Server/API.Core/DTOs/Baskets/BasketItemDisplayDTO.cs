using API.Core.DTOs.Products;
using API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.DTOs.Baskets
{
    public class BasketItemDisplayDTO
    {
        public string basketItemId { get; set; }
        public string basketId { get; set; }
        public ProductDisplayDTO product { get; set; }
        public int quantity { get; set; }
        public string sizeSelected { get; set; }

        public BasketItemDisplayDTO() { }

        public BasketItemDisplayDTO(BasketItem v)
        {
            basketItemId        = v.mID;
            basketId            = v.basket.mID;
            product             = new ProductDisplayDTO(v.product);
            quantity            = v.qty;
            sizeSelected        = v.sizeSelected;
        }
    }
}
