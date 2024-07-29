using Microsoft.EntityFrameworkCore;
using ShopOnlineAPI.Data;
using ShopOnlineAPI.Entities;
using ShopOnlineAPI.Repositories.Contracts;

namespace ShopOnlineAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext context;

        public ProductRepository(ShopOnlineDbContext context) 
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories=await this.context.ProductCategories.ToListAsync();
            return categories;

        }

        public Task<ProductCategory> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.context.Products.ToListAsync();
            return products;

        }

        public Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
