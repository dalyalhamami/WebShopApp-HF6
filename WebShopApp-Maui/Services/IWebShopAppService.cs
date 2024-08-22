namespace WebShopApp_Maui.Services;
public interface IWebShopAppService
{
    //...................................................... Products ......................................................//
    Task<List<ProductModel>> GetProductsAsync();

    //....................................................... User ........................................................//

    Task<(UserModel user, string errorMessage)> RegisterUser(UserModel userModel);
    Task<UserModel> LoginAsync(string email, string password);
}
