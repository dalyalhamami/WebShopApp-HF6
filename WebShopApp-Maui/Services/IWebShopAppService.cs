using WebShopApp_Maui.Models;

namespace WebShopApp_Maui.Services
{
    public interface IWebShopAppService
    {
        Task<List<ProductModel>> GetProductsAsync();
    }
}
