namespace WebShopApp_Blazor.Models;

public class UserOrderModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string OrderId { get; set; }
    public string PaymentMode { get; set; }
    public string ShippingAddress { get; set; }
    public int ShippingCharges { get; set; }
    public int SubTotal { get; set; }
    public int Total { get; set; }
    public string ShippingStatus { get; set; }
    public string CreatedOn { get; set; }
    public string UpdatedOn { get; set; }
}
