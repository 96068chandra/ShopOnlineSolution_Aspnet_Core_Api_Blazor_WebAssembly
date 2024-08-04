using ShopOnline.Models.Dtos;
using System.Net.Http.Json;

namespace ShopOnlineWeb.Services.Contracts
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient client;

        public ShoppingCartService(HttpClient client) 
        {
            this.client = client;
        }

        public async Task<CartItemDto> AddCartItem(CartItemToAddDto cartItem)
        {
            try
            {
                var response = await client.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart", cartItem);
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode==System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CartItemDto);
                    }
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();

                }
                else
                {
                    var msg=await response.Content.ReadAsStringAsync();
                    throw new Exception($"HttpStatus:{response.StatusCode} Message-{msg}");
                }

            }
            catch (Exception ex)
            {
                throw;

            }
        }

        

        public async Task<IEnumerable<CartItemDto>> GetCartItems(int userid)
        {
            try
            {
                var response = await this.client.GetAsync($"api/ShoppingCart/{userid}/GetItems");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDto>();

                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>();

                }
                else
                {
                    var msg= await response.Content.ReadAsStringAsync();
                    throw new Exception($"HttpStatus:{response.StatusCode} Message-{msg}");
                }
            }
            catch(Exception ex)
            {
                throw new NotImplementedException();

            }
        }
    }
}
