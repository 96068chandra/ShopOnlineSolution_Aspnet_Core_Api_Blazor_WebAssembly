using ShopOnline.Models.Dtos;

namespace ShopOnlineWeb.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetCartItems(int userid);
       // Task<CartItemDto> GetCartItem();
        Task<CartItemDto> AddCartItem(CartItemToAddDto cartItem);
        Task<CartItemDto> DeleteItem(int id);

        Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto quantityUpdateDto);
    }
}
