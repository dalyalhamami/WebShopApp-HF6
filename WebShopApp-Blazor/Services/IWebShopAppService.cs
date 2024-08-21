namespace WebShopApp_Blazor.Services;
public interface IWebShopAppService
{
    Task<List<ProductModel>> GetProductsAsync();
}
