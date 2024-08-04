using Microsoft.EntityFrameworkCore;
using ShopOnline.Models.Dtos;
using ShopOnlineAPI.Data;
using ShopOnlineAPI.Entities;

namespace ShopOnlineAPI.Repositories.Contracts
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext context;

        public ShoppingCartRepository(ShopOnlineDbContext context) 
        {
            this.context = context;
        }

        private async Task<bool> CartItemExist(int cartId,int productId)
        {
            return await this.context.CartItems.AnyAsync(c=>c.CartId==cartId && c.ProductId==productId);
        }
        public async Task<CartItem> AddItem(CartItemToAddDto itemToAdd)
        {
            if(await CartItemExist(itemToAdd.CartId, itemToAdd.ProductId)==false)
            {
                var item = await (from product in context.Products
                                  where product.Id == itemToAdd.ProductId
                                  select new CartItem
                                  {
                                      CartId = itemToAdd.CartId,
                                      ProductId = product.Id,
                                      Qty = itemToAdd.Qty

                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var res = await context.CartItems.AddAsync(item);
                    await context.SaveChangesAsync();
                    return res.Entity;
                }

            }
            return null;
            
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CartItem>> GetAll(int userId)
        {
            return await (from cart in this.context.Carts
                          join cartItem in this.context.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id= cartItem.Id,
                              ProductId= cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId= cartItem.CartId

                              
                          }).ToListAsync();
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this.context.Carts
                          join cartItem in this.context.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
