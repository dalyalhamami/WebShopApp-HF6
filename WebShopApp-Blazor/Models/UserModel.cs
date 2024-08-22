namespace WebShopApp_Blazor.Models;
public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public int Roles { get; set; }
}
