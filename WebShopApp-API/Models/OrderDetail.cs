namespace WebShopApp_API.Models;

public class OrderDetail
{
    [Key]
    public int Id { get; set; }
    public string OrderId { get; set; }

    [Required, ForeignKey("Product")]
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public int SubTotal { get; set; }
    public string CreatedOn { get; set; }
    public string UpdatedOn { get; set; }
}
