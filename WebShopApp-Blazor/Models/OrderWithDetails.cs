namespace WebShopApp_Blazor.Models;

public class OrderWithDetails
{
    public string OrderId { get; set; }
    public List<CartModel> Details { get; set; }
}
