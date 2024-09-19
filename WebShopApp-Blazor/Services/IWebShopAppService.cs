namespace WebShopApp_Blazor.Services;
public interface IWebShopAppService
{
    //....................................................... User ........................................................//

    Task<(UserModel user, string errorMessage)> RegisterUser(RegisterModel userModel);
    Task<List<UserModel>> GetUsersAsync();
    Task<UserModel> UpdateUserAsync(UserModel editedUser);
    Task<UserModel> LoginAsync(string email, string password);
    Task<bool> CheckEmailAsync(string email);
    Task<HttpResponseMessage> ChangePassword(PasswordModel passwordModel);
    Task<HttpResponseMessage> ResetPassword(PasswordModel passwordModel);

    //...................................................... Category ......................................................//
    Task<CategoryModel> SaveCategoryAsync(CategoryModel newCategory);
    Task<List<CategoryModel>> GetCategoriesAsync();
    Task<CategoryModel> GetCategoryByIdAsync(int? id);
    Task<CategoryModel> UpdateCategoryAsync(CategoryModel editedCategory);
    Task<bool> DeleteCategoryAsync(int categoryId);

    //...................................................... Products ......................................................//

    Task<ProductModel> SaveProductAsync(ProductModel newProduct);
    Task<List<ProductModel>> GetProductsAsync();
    Task<ProductModel> UpdateProductAsync(ProductModel editedProduct);
    Task<bool> DeleteProductAsync(int productId);


    //...................................................... Order .......................................................//

    Task<string> Checkout(List<CartModel> cartItems);
    Task<List<UserOrderModel>> GetOrdersByUserId(int userId);
    Task<List<CartModel>> GetOrderDetailForUser(int userId, string orderNumber);
    Task<List<string>> GetShippingStatusForOrder(string orderNumber);
}
