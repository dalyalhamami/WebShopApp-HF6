namespace WebShopApp_API.Data;

public class WebShopAppDBContext : DbContext
{
    public WebShopAppDBContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Product> Product { get; set; }

}

