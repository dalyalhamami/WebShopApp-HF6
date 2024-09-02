namespace WebShopApp_Maui.Models;

public class UserRegisterModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Mobile is required")]
    [Phone(ErrorMessage = "Invalid mobile number")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
}
