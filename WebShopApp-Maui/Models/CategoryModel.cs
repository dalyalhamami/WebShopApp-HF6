﻿namespace WebShopApp_Maui.Models;

public class CategoryModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; }
}
