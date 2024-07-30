using ShopOnline.Models.Dtos;

namespace ShopOnlineWeb.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
        Task<ProductDto> GetProductById(int id);

    }
}
