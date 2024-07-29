using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;

namespace ShopOnlineWeb.Pages
{
    public class DisplayProductsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto>  Products { get; set; }
    }
}
