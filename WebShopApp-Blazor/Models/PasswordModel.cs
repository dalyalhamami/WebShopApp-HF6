namespace WebShopApp_Blazor.Models;

public class PasswordModel
{
    public int UserId { get; set; }
    public string OldPassword { get; set; }
    public string Password { get; set; }
    public string HashedPassword { get; set; }
}
