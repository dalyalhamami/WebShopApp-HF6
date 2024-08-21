namespace WebShopApp_API.Models;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string? Name { get; set; }

    [Required, MaxLength(100)]
    public string? Email { get; set; }

    [Required, MaxLength(100)]
    public string? Password { get; set; }

    [MaxLength(100)]
    public string? Mobile { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }

    public int Roles { get; set; }
}
