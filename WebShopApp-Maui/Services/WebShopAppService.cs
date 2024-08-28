﻿namespace WebShopApp_Maui.Services;
public class WebShopAppService : IWebShopAppService
{
    private readonly HttpClient httpClient;

    // Inject HttpClient via constructor
    public WebShopAppService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = DeviceInfo.Platform == DevicePlatform.Android
            ? new Uri("http://10.0.2.2:5031/")
            : new Uri("http://localhost:5031/");
    }

    //....................................................... User .......................................................//

    // Create user
    public async Task<(UserModel user, string errorMessage)> RegisterUser(UserModel userModel)
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

    // Login
    public async Task<UserModel> LoginAsync(string email, string password)
    {
        var loginUrl = "api/User/Login";
        var loginData = new { Email = email, Password = password };

        var response = await httpClient.PostAsJsonAsync(loginUrl, loginData);

        if (response != null)
        {
            var user = await response.Content.ReadFromJsonAsync<UserModel>();
            return user;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("User not found");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
        return null;
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


    //...................................................... Category ......................................................//

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

    // Get all products
    public async Task<List<ProductModel>> GetProductsAsync()
    {
        try
        {
            var response = await httpClient.GetAsync($"api/Product/GetProducts");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<ProductModel>>();
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
}

