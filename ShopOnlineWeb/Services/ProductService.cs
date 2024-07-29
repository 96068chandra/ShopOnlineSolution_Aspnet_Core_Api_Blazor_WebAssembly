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
                var products = await this.client.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
                return products;
               

            }
            catch (Exception)
            {

                throw;
            }
          
        }
    }
}
