using ShopOnline.Models.Dtos;

namespace ShopOnlineWeb.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDto>> GetCartItems(int userid);
       // Task<CartItemDto> GetCartItem();
        Task<CartItemDto> AddCartItem(CartItemToAddDto cartItem);
    }
}
