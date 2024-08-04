using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnlineWeb.Services.Contracts;

namespace ShopOnlineWeb.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IShoppingCartService shoppingCartService { get; set; }
        public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMsg { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems=await shoppingCartService.GetCartItems(HardCoded.Userid);
            }
            catch (Exception ex)
            {

                ErrorMsg=ex.Message;
            }
        }
    }
}
