namespace WebShopApp_Maui.Services;
public interface IWebShopAppService
{
    //....................................................... User ........................................................//

    Task<(UserModel user, string errorMessage)> RegisterUser(UserModel userModel);
    Task<UserModel> UpdateUserAsync(UserModel editedUser);
    Task<UserModel> LoginAsync(string email, string password);
    Task<bool> CheckEmailAsync(string email);

    //...................................................... Category ......................................................//
    Task<List<CategoryModel>> GetCategoriesAsync();
    Task<CategoryModel> GetCategoryByIdAsync(int? id);

    //...................................................... Products ......................................................//

    Task<ProductModel> SaveProductAsync(ProductModel newProduct);
    Task<List<ProductModel>> GetProductsAsync();
    Task<ProductModel> UpdateProductAsync(ProductModel editedProduct);
    Task<bool> DeleteProductAsync(int productId);

}
