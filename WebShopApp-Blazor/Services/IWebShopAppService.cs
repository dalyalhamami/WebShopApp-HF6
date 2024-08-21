namespace WebShopApp_Blazor.Services;
public interface IWebShopAppService
{
    
    //...................................................... Product ......................................................//
    Task<List<ProductModel>> GetProductsAsync();


    //....................................................... User .......................................................//
    Task<(UserModel user, string errorMessage)> RegisterUser(UserModel userModel);
}
