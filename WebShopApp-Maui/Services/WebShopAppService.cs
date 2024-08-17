using System.Net.Http.Json;
using WebShopApp_Maui.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace WebShopApp_Maui.Services
{
    public class WebShopAppService : IWebShopAppService
    {
        private readonly HttpClient httpClient;

        // Inject HttpClient via constructor
        public WebShopAppService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            // Assign Base Address here if needed
            httpClient.BaseAddress = DeviceInfo.Platform == DevicePlatform.Android
                ? new Uri("http://10.0.2.2:5031/")
                : new Uri("http://localhost:5031/");
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

    }

}
