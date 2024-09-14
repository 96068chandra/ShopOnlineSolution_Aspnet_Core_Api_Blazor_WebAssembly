using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnlineWeb.Services.Contracts;

namespace ShopOnlineWeb.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IShoppingCartService shoppingCartService { get; set; }
        public List<CartItemDto> ShoppingCartItems { get; set; }

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

        protected async Task DeleteCartItem_Click(int id)
        {
            var cartitem=await shoppingCartService.DeleteItem(id);
            RemoveCartItem(id);

        }
        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            if (qty > 0)
            {
                var updateItemDto = new CartItemQtyUpdateDto
                {
                    CartItemId = id,
                    Qty = qty

                };
                var returnedUpdateItemDto=await this.shoppingCartService.UpdateQty(updateItemDto);  
                }
            else
            {
                var item=this.ShoppingCartItems.FirstOrDefault(i=>i.Id==id);
                if (item != null)
                {
                    item.Qty = 1;
                    item.TotalPrice = item.Price;
                }
            }
        }
        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);

        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto=GetCartItem(id);
            ShoppingCartItems.Remove(cartItemDto);


        }


    }
}
