namespace WebShopApp_Maui.Services;
public interface IWebShopAppService
{
    //....................................................... User ........................................................//

    Task<(UserModel user, string errorMessage)> RegisterUser(UserModel userModel);
    Task<UserModel> LoginAsync(string email, string password);
    Task<bool> CheckEmailAsync(string email);

    //...................................................... Category ......................................................//
    Task<List<CategoryModel>> GetCategoriesAsync();

    //...................................................... Products ......................................................//

    Task<ProductModel> SaveProductAsync(ProductModel newProduct);
    Task<List<ProductModel>> GetProductsAsync();

}
