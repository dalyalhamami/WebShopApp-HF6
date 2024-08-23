﻿namespace WebShopApp_Maui.Models;
public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockId { get; set; }
    public int CategoryId { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public bool CartFlag { get; set; }
}
