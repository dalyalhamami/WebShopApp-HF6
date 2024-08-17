using System.Net.Http.Json;
using WebShopApp_CL.Models;

namespace WebShopApp_CL.Services;

public class WebShopAppService
{
    private readonly HttpClient httpClient;
    public WebShopAppService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    //Get all products
    public async Task<List<ProductModel>> GetProductsAsync()
    {
        try
        {
            var products = await httpClient.GetFromJsonAsync<List<ProductModel>>("api/Product/GetProducts");
            return products;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return null;
        }
    }
}
