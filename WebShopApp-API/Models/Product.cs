namespace WebShopApp_API.Models;

public class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } 

    [Required] 
    public decimal Price { get; set; }

    [Required, ForeignKey("Stock")]
    public int StockId { get; set; }

    [Required, ForeignKey("Category")]
    public int CategoryId { get; set; }

    [Required]  
    public string ImageUrl { get; set; }

    [Required, MaxLength(1000)]  
    public string Description { get; set; }
}
