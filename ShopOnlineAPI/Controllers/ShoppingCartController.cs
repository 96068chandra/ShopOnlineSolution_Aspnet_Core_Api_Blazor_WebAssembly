using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models.Dtos;
using ShopOnlineAPI.Extensions;
using ShopOnlineAPI.Repositories.Contracts;

namespace ShopOnlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }
        [HttpGet]
        [Route("{userid}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userid)
        {
            try
            {
                var cartItems = await this.shoppingCartRepository.GetAll(userid);
                if (cartItems == null)
                {
                    return BadRequest();
                }
                var products = await this.productRepository.GetItems();
                if (products == null)
                {
                    throw new Exception("No Products Exist in the System");
                }
               var cartItemsDto=cartItems.ConvertDto(products);
                return Ok(cartItemsDto);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {
            try
            {
                var carttem = await this.shoppingCartRepository.GetItem(id);
                if (carttem == null) return NotFound();
                var product = await this.productRepository.GetProduct(carttem.ProductId);
                if (product == null) return NotFound();
                var cartItemDto=carttem.ConvertDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);

            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> PostItem([FromBody] CartItemToAddDto item)
        {
            try
            {
                var newCartItem = await this.shoppingCartRepository.AddItem(item);
                if(newCartItem == null) return NotFound();
                var product=await productRepository.GetProduct(newCartItem.ProductId);
                if (product == null) throw new Exception($"Something went wrong when attempting to retrive a product(productId:({item.ProductId})");
                var newCartItemDto=newCartItem.ConvertDto(product);
                return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
