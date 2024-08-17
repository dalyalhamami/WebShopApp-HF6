using WebShopApp_CL.Models;

namespace WebShopApp_CL.Services;

public interface IWebShopAppService
{
    Task<List<ProductModel>> GetProductsAsync();
}
