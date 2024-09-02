namespace WebShopApp_API.DTO;
public class PasswordDTO
{
    public int UserId { get; set; }
    public string OldPassword { get; set; }
    public string HashedPassword { get; set; }
}
