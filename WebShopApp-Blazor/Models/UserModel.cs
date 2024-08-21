namespace WebShopApp_Blazor.Models;
public class UserModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
    [JsonIgnore]  // Prevent ConfirmPassword from being serialized and sent to the server
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Mobile is required")]
    public string? Mobile { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string? Address { get; set; }

    public int Roles { get; set; }
}
