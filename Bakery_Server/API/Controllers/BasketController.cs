using API.Core.Contracts;
using API.Core.CustomExceptions;
using API.Core.DTOs.Baskets;
using API.Core.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketContext;
        private readonly IBasketItemService _basketItemContext;
        private readonly IProductService _productContext;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketItemService basketItemRepository, IBasketService basketRepository, IProductService productRepository, ILogger<BasketController> logger)
        {
            _basketContext = basketRepository;
            _basketItemContext = basketItemRepository;
            _productContext = productRepository;
            _logger = logger;
        }

        [HttpGet("GetBasket")]
        public async Task<IActionResult> GetBasket()
        {
            try
            {
                Basket basket = await _basketContext.GetBasket(HttpContext);
                BasketDisplayDTO basketDisplay = new BasketDisplayDTO(basket);

                return Ok(basketDisplay);
            }
            catch (EntryPointNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetBasketItem/{basketItemId}")]
        public async Task<IActionResult> GetBasketItem(string basketItemId)
        {
            try
            {
                BasketItem target = await _basketItemContext.FindBasketItemAsync(HttpContext, basketItemId);
                return Ok(new BasketItemDisplayDTO(target));
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(BasketItemMakerDTO basketItemData)
        {
            try
            {
                Basket basket = await _basketContext.GetBasket(HttpContext);

                BasketItem newBasketItem = new BasketItem(basketItemData, basket);
                newBasketItem.product = await _productContext.FindAsync(basketItemData.productId);

                BasketItemValidation basketItemValidator = new BasketItemValidation(_productContext);
                ValidationResult validation = await basketItemValidator.ValidateAsync(newBasketItem);

                if (!validation.IsValid)
                {
                    throw new Exception(String.Join("", validation.Errors.Select(e => e.ErrorMessage + "\n")));
                }

                BasketItem newBasketItemCreated = await _basketItemContext
                    .InsertNewBasketItemAsync(HttpContext, newBasketItem);

                BasketItemDisplayDTO basketItemDisplay = new BasketItemDisplayDTO(newBasketItemCreated);

                return Created("", basketItemDisplay);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("RemoveFromCart/{basketItemId}")]
        public async Task<IActionResult> RemoveFromCart(string basketItemId)
        {
            try
            {
                await _basketItemContext.DeleteAsync(HttpContext, basketItemId);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateCartItem/{basketItemId}")]
        public async Task<IActionResult> UpdateCartItem(string basketItemId, BasketItemMakerDTO updatedBasketItemData)
        {
            try
            {
                BasketItem target = await _basketItemContext.FindBasketItemAsync(HttpContext, basketItemId);

                BasketItem updatedBasketItem = new BasketItem(updatedBasketItemData);
                updatedBasketItem.product = target.product;

                BasketItemValidation basketItemValidator = new BasketItemValidation(_productContext);
                ValidationResult validation = await basketItemValidator.ValidateAsync(updatedBasketItem);

                if (!validation.IsValid)
                {
                    throw new Exception(String.Join("", validation.Errors.Select(e => e.ErrorMessage + "\n")));
                }

                BasketItem newBasketItemState = await _basketItemContext
                    .UpdateEntityAsync(HttpContext, basketItemId, updatedBasketItem);

                BasketItemDisplayDTO basketItemDisplay = new BasketItemDisplayDTO(newBasketItemState);

                return Ok(basketItemDisplay);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                Basket userBasket = await _basketContext.ClearBasket(HttpContext);
                BasketDisplayDTO basketDisplay = new BasketDisplayDTO(userBasket);

                return Ok(basketDisplay);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
