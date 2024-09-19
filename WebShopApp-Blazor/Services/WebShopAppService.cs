namespace WebShopApp_Blazor.Services;
public class WebShopAppService : IWebShopAppService
{
    private readonly HttpClient httpClient;
    public WebShopAppService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = new Uri("http://localhost:5031/");
    }

    //....................................................... User .......................................................//

    // Create user
    public async Task<(UserModel user, string errorMessage)> RegisterUser(RegisterModel userModel)
    {
        try
        {
            var jsonContent = JsonContent.Create(userModel);
            var response = await httpClient.PostAsync("api/User/CreateUser", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var savedUser = await response.Content.ReadFromJsonAsync<UserModel>();
                return (savedUser, null);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (null, errorContent);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return (null, "Exception occurred: " + ex.Message);
        }
    }

    // Get all users
    public async Task<List<UserModel>> GetUsersAsync()
    {
        try
        {
            var users = await httpClient.GetFromJsonAsync<List<UserModel>>("api/User/GetUsers");
            return users;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return null;
        }
    }

    // Update user
    public async Task<UserModel> UpdateUserAsync(UserModel editedUser)
    {
        try
        {
            // Serialize the editedUser object to JSON
            var jsonContent = JsonContent.Create(editedUser);

            // Log the request content for debugging
            var requestContent = await jsonContent.ReadAsStringAsync();
            Console.WriteLine($"Request Content: {requestContent}");

            var response = await httpClient.PutAsync("api/User/UpdateUser", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to UserModel object
                var updatedUser = await response.Content.ReadFromJsonAsync<UserModel>();
                return updatedUser;
            }
            else
            {
                // Log the response content for debugging
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, {errorContent}");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Exception occurred: {ex.Message}");
        }
        return null;
    }

    // Login
    public async Task<UserModel> LoginAsync(string email, string password)
    {
        var loginUrl = "api/User/Login";
        var loginData = new { Email = email, Password = password };

        var response = await httpClient.PostAsJsonAsync(loginUrl, loginData);

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<UserModel>();
            return user;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // Email not found
            throw new Exception("Email not registered");
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            // Incorrect password
            throw new Exception("Incorrect password");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            throw new Exception("An error occurred during login");
        }
    }

    // Check if email exists
    public async Task<bool> CheckEmailAsync(string email)
    {
        var response = await httpClient.GetAsync($"api/User/CheckEmail/{email}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        Console.WriteLine($"Error checking email: {response.StatusCode}");
        return false;
    }

    // Update password
    public async Task<HttpResponseMessage> ChangePassword(PasswordModel passwordModel)
    {
        var jsonContent = JsonContent.Create(passwordModel);
        var response = await httpClient.PutAsync("api/User/ChangePassword", jsonContent);
        return response;
    }

    // Reset password
    public async Task<HttpResponseMessage> ResetPassword(PasswordModel passwordModel)
    {
        var jsonContent = JsonContent.Create(passwordModel);
        var response = await httpClient.PutAsync("api/User/ResetPassword", jsonContent);
        return response;
    }
    //...................................................... Category ......................................................//

    // Create new category
    public async Task<CategoryModel> SaveCategoryAsync(CategoryModel newCategory)
    {
        try
        {
            // Serialize the newCategory object to JSON
            var jsonContent = JsonContent.Create(newCategory);

            var response = await httpClient.PostAsync("api/Category/CreateCategory", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var savedCategory = await response.Content.ReadFromJsonAsync<CategoryModel>();
                return savedCategory;
            }
            else
            {
                // Handle other error cases
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
        }
        return null;
    }

    // Get all categories
    public async Task<List<CategoryModel>> GetCategoriesAsync()
    {
        try
        {
            var categories = await httpClient.GetFromJsonAsync<List<CategoryModel>>("api/Category/GetCategories");
            return categories;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return null;
        }
    }

    // Get category
    public async Task<CategoryModel> GetCategoryByIdAsync(int? id)
    {
        try
        {
            var response = await httpClient.GetAsync($"api/WebApp/GetCategoryById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var category = await response.Content.ReadFromJsonAsync<CategoryModel>();
                return category;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Category not found");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
        }
        return null; // Return null in case of errors
    }

    // Update category
    public async Task<CategoryModel> UpdateCategoryAsync(CategoryModel editedCategory)
    {
        try
        {
            // Serialize the editedCategory object to JSON
            var jsonContent = JsonContent.Create(editedCategory);

            var response = await httpClient.PutAsync($"api/Category/EditCategory", jsonContent);

            if (response != null)
            {
                // Deserialize the response content to CategoryModel object
                var updatedCategory = await response.Content.ReadFromJsonAsync<CategoryModel>();
                return updatedCategory;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
        }
        return null; // Return null in case of errors
    }

    // Delete category
    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        try
        {
            // Make a DELETE request to the appropriate API endpoint
            var response = await httpClient.DeleteAsync($"api/Category/DeleteCategory/{categoryId}");

            // Check if the request was successful
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return false; // Return false in case of errors
        }
    }


    //...................................................... Product ......................................................//

    // Create new product
    public async Task<ProductModel> SaveProductAsync(ProductModel newProduct)
    {
        try
        {
            // Serialize the newProduct object to JSON
            var jsonContent = JsonContent.Create(newProduct);

            var response = await httpClient.PostAsync("api/Product/CreateProduct", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to ProductModel object
                var savedProduct = await response.Content.ReadFromJsonAsync<ProductModel>();
                return savedProduct;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return null;
        }
    }

    // Get all products with category
    public async Task<List<ProductModel>> GetProductsAsync()
    {
        try
        {
            // Fetch products
            var response = await httpClient.GetAsync($"api/Product/GetProducts");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<ProductModel>>();

            // Fetch categories
            var categories = await GetCategoriesAsync();

            // Match products with their respective category names
            if (products != null && categories != null)
            {
                foreach (var product in products)
                {
                    var category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
                    if (category != null)
                    {
                        product.CategoryName = category.Name;
                    }
                }
            }

            return products;
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"HTTP Request failed: {httpEx.Message}");
            Console.WriteLine($"Status Code: {httpEx.StatusCode}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            return null;
        }
    }

    // Update product
    public async Task<ProductModel> UpdateProductAsync(ProductModel editedProduct)
    {
        //Retrieve category details based on editedProduct.CategoryId
        var category = await GetCategoryByIdAsync(editedProduct.CategoryId);
        if (category != null)
        {
            editedProduct.CategoryName = category.Name;
        }

        try
        {
            // Serialize the editedProduct object to JSON
            var jsonContent = JsonContent.Create(editedProduct);

            var response = await httpClient.PutAsync($"api/Product/UpdateProduct", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to ProductModel object
                var updatedProduct = await response.Content.ReadFromJsonAsync<ProductModel>();
                return updatedProduct;
            }
            else
            {
                // Handle other error cases
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine($"Exception occurred: {ex.Message}");
        }
        return null; // Return null in case of errors
    }

    // Delete product
    public async Task<bool> DeleteProductAsync(int productId)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/Product/DeleteProduct/{productId}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return false; // Return false in case of errors
        }
    }

    //...................................................... Order .......................................................//

    public async Task<string> Checkout(List<CartModel> cartItems)
    {
        try
        {
            var jsonContent = JsonContent.Create(cartItems);
            var response = await httpClient.PostAsync($"api/Order/Checkout", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Read the orderId from the API response
                var orderId = await response.Content.ReadAsStringAsync();
                return orderId;  // Return the orderId to Blazor page
            }
            else
            {
                return null;  // Return null if the request fails
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return null;
        }
    }

    public async Task<List<UserOrderModel>> GetOrdersByUserId(int userId)
    {
        try
        {
            var userOrders = await httpClient.GetFromJsonAsync<List<UserOrderModel>>($"api/Order/GetOrdersByUserId?userId=" + userId);
            return userOrders;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return null;
        }
    }

    public async Task<List<CartModel>> GetOrderDetailForUser(int userId, string orderNumber)
    {
        return await httpClient.GetFromJsonAsync<List<CartModel>>($"api/Order/GetOrderDetailForUser?userId={userId}&orderNumber={orderNumber}");
    }

    public async Task<List<string>> GetShippingStatusForOrder(string orderNumber)
    {
        var statuses = await httpClient.GetFromJsonAsync<List<string>>($"api/Order/GetShippingStatusForOrder?orderNumber=" + orderNumber);
        return statuses ?? new List<string>();
    }
}

