﻿using API.Core.Contracts;
using API.Core.CustomExceptions;
using API.Core.DTOs.Products;
using API.Core.Models;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService repository, ILogger<ProductsController> logger)
        {
            _context = repository;
            _logger = logger;
        }

        [HttpGet("GetProducts")]
        public IActionResult GetEntities()
        {
            IEnumerable<Product> products = _context.GetAllEntities();

            return Ok(products.Select(p => new ProductDisplayDTO(p)));
        }

        [HttpGet("GetProduct/id")]
        public async Task<IActionResult> GetEntity(string id)
        {
            try
            {
                Product product = await _context.FindAsync(id);

                return Ok(new ProductDisplayDTO(product));
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

        [HttpPost("NewProduct")]
        public async Task<IActionResult> InsertProduct(ProductMakerDTO productMaker)
        {
            Product newProduct = await _context.InsertAsync(new Product(productMaker));
            Product completeProduct = await _context.AddImagesToProduct(newProduct.mID, productMaker.images);
            
            return Created("", new ProductDisplayDTO(completeProduct));
        }

        [HttpPut("UpdateProduct/Id")]
        public async Task<IActionResult> UpdateProduct(string id, ProductMakerDTO productMaker)
        {
            try
            {
                Product updatedProduct = await _context.UpdateAsync(id, new Product(productMaker));
                Product completeProduct = await _context.AddImagesToProduct(updatedProduct.mID, productMaker.images);

                return Ok(new ProductDisplayDTO(updatedProduct));
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

        [HttpDelete("DeleteProduct/Id")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                await _context.DeleteAsync(id);

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
    }
}
