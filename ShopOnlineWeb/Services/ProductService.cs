using ShopOnline.Models.Dtos;
using ShopOnlineWeb.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnlineWeb.Services
{
    public class ProductService:IProductService
    {
        private readonly HttpClient client;

        public ProductService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                var response=await this.client.GetAsync($"api/Product");
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode==System.Net.HttpStatusCode.NoContent )
                    {
                        return Enumerable.Empty<ProductDto>();
                    }
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }


            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            try
            {
                var response = await this.client.GetAsync($"api/Product/{id}");
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode==System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductDto);
                    }
                    return await response.Content.ReadFromJsonAsync<ProductDto>();

                }
                else
                {
                    var message=await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }


            }
            catch (Exception)
            {
                //Log Exception

                throw;
            }
        }
    }
}
