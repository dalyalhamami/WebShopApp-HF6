namespace WebShopApp_Blazor.Services;
public class WebShopAppService : IWebShopAppService
{
    private readonly HttpClient httpClient;
    public WebShopAppService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        httpClient.BaseAddress = new Uri("http://localhost:5031/");
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
}
