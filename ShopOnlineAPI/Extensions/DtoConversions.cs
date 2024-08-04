using ShopOnline.Models.Dtos;
using ShopOnlineAPI.Entities;
using System.Runtime.CompilerServices;

namespace ShopOnlineAPI.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertDto( this IEnumerable<Product> products,
                                                         IEnumerable<ProductCategory> categories)
        {
            return (from product in products
                    join category in categories
                    on product.CategoryId equals category.Id
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        ImageURL= product.ImageURL,
                        Description= product.Description,
                        Qty= product.Qty,
                        CategoryId= category.Id,
                        CategoryName= category.Name,

                    }).ToList();
        }

        public static ProductDto ConvertDto(this Product product,
                                                        ProductCategory category)
        {
            return new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        ImageURL = product.ImageURL,
                        Description = product.Description,
                        Qty = product.Qty,
                        CategoryId = category.Id,
                        CategoryName = category.Name,

                    };
        }

        public static IEnumerable<CartItemDto> ConvertDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products
                    on cartItem.ProductId equals product.Id
                    select new CartItemDto
                    {
                        Id = cartItem.Id,
                        
                        ProuctId=cartItem.ProductId,
                        ProductName=product.Name,
                        ProductDescription=product.Description,
                        ProductImageURL=product.ImageURL
                        ,CartId=cartItem.CartId,
                        Qty=cartItem.Qty,
                        TotalPrice=product.Price

                    }).ToList();
        }

        public static CartItemDto ConvertDto(this CartItem cartItem,Product product)
        {
            return new CartItemDto
            {
                Id = cartItem.Id,
                ProuctId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                CartId = cartItem.CartId,
                Qty = cartItem.Qty,
                TotalPrice = product.Price


            };
        }
    }
}
