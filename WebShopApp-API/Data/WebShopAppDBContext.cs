namespace WebShopApp_API.Data;

public class WebShopAppDBContext : DbContext
{
    public WebShopAppDBContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserOrder> UserOrder { get; set; }
    public DbSet<OrderDetail> OrderDetail { get; set; }
}

