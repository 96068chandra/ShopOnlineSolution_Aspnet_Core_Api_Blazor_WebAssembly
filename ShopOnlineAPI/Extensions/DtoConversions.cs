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
    }
}
