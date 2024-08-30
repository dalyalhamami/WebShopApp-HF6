namespace WebShopApp_Maui.Models;

public class PasswordModel
{
    public int UserId { get; set; }
    public string OldPassword { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
