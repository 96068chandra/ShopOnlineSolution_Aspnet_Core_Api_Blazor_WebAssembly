﻿using ShopOnlineAPI.Entities;

namespace ShopOnlineAPI.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetProduct(int id);
        Task<ProductCategory> GetCategory(int id);




    }
}
