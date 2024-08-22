namespace WebShopApp_Maui.Services;
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

    //...................................................... Product ......................................................//

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
}

