﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models.Dtos;
using ShopOnlineAPI.Extensions;
using ShopOnlineAPI.Repositories.Contracts;

namespace ShopOnlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await this.repository.GetItems();
                var categories=await this.repository.GetCategories();
                if(products==null||categories==null) return NotFound();

                var productDtos = products.ConvertDto(categories);
                return Ok(productDtos);


            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }


        [HttpGet("{id:int}")]

        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await repository.GetProduct(id);

                if (product == null)
                {
                    return BadRequest();
                }
                else
                {
                    var category = await repository.GetCategory(product.CategoryId);

                    var productDto = product.ConvertDto(category);

                    return Ok(productDto);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }

        
    }
}
