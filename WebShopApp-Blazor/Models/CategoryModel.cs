namespace WebShopApp_Blazor.Models;

public class CategoryModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Navn er påkrævet")]
    public string Name { get; set; }
}
