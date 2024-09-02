namespace WebShopApp_API.DTO;
    public class UserRegisterDto
    {
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    
    [Required]
    public string Mobile { get; set; }
 
    [Required]
    public string Address { get; set; }
}

