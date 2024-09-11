namespace WebShopApp_Maui.Models;

public class OrderWithDetails
{
    public string OrderId { get; set; }
    public List<CartModel> Details { get; set; }
}
