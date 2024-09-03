namespace WebShopApp_API.Models;

public class UserOrder
{
    [Key]
    public int Id { get; set; }

    [Required, ForeignKey("User")]
    public int UserId { get; set; }

    [Required, ForeignKey("OrderDetail")]
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
