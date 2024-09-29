namespace WebShopApp_API.DTO;

public class CartDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public int AvailableStock { get; set; }
    public string ShippingAddress { get; set; }
    public int ShippingCharges { get; set; }
    public int SubTotal { get; set; }
    public string PaymentMode { get; set; }
    public int UserId { get; set; }
    public int Total { get; set; }
}
