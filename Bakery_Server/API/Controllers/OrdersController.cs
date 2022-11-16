using API.Core.Contracts;
using API.Core.CustomExceptions;
using API.Core.DTOs.Orders;
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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderContext;
        private readonly IBasketService _basketService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, IBasketService basketService, ILogger<OrdersController> logger)
        {
            _orderContext = orderService;
            _basketService = basketService;
            _logger = logger;
        }

        [HttpGet("GetOrders")]
        public IActionResult GetAllOrders()
        {
            IEnumerable<BakeryOrder> rawOrders = _orderContext.GetBakeryOrders();

            return Ok(rawOrders.Select(x => new OrderDisplayDTO(x)));
        }

        [HttpGet("GetOrder/{entityId}")]
        public async Task<IActionResult> GetOrder(string entityId)
        {
            try
            {
                BakeryOrder target = await _orderContext.FindOrderAsync(entityId);
            
                return Ok(new OrderDisplayDTO(target));
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

        [HttpPost("CreateNewOrder")]
        public async Task<IActionResult> CreateNewOrder(OrderMakerDTO orderDetails)
        {
            Basket basket = await _basketService.GetBasket(HttpContext);
            BakeryOrder newOrder = await _orderContext.CreateNewOrder(orderDetails, basket);

            OrderValidation basketItemValidator = new OrderValidation();
            ValidationResult validation = await basketItemValidator.ValidateAsync(newOrder);

            if (!validation.IsValid)
            {
                throw new Exception(String.Join("", validation.Errors.Select(e => e.ErrorMessage + "\n")));
            }

            OrderDisplayDTO res = new OrderDisplayDTO(newOrder);
            res.basketId = basket.mID;
            await _basketService.ClearBasket(HttpContext);

            return Created("", res);
        }
    }
}
