using Newtonsoft.Json;
using ShopOnline.Models.Dtos;
using System.Net.Http.Json;
using System.Text;

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

        public async Task<CartItemDto> DeleteItem(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"api/ShoppingCart/{id}");
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();

                }
                return default(CartItemDto);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<CartItemDto>> GetCartItems(int userid)
        {
            try
            {
                var response = await this.client.GetAsync($"api/ShoppingCart/{userid}/GetItems");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDto>().ToList();

                    }
                    return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();

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

        public async Task<CartItemDto> UpdateQty(CartItemQtyUpdateDto quantityUpdateDto)
        {
            try
            {
                var jsonRequest=JsonConvert.SerializeObject(quantityUpdateDto);
                var content=new StringContent(jsonRequest,Encoding.UTF8,"application/json-patch+json");
                var response = await client.PatchAsync($"api/ShoppingCart/{quantityUpdateDto.CartItemId}", content);
                if(response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();

                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
