﻿namespace WebShopApp_API.DTO;

public class UserUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string? Address { get; set; }
    public int Roles { get; set; }
}
