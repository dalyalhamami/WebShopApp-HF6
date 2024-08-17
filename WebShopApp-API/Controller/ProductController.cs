namespace WebShopApp_API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly WebShopAppDBContext webShopAppDBContext;
    public ProductController(WebShopAppDBContext webShopAppDBContext)
    {
        this.webShopAppDBContext = webShopAppDBContext;
    }

    // Create a new product
    [HttpPost("SaveProduct")]
    public async Task<ActionResult<Product>> SaveProduct(Product product)
    {
        if (product != null)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                StockId = product.StockId,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
            };
            webShopAppDBContext.Product.Add(newProduct);
            await webShopAppDBContext.SaveChangesAsync();
            return Ok(product);
        }
        else
        {
            return BadRequest("Category not found.");
        }
    }

    // Get all products
    [HttpGet("GetProducts")]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        try
        {
            var products = await webShopAppDBContext.Product.ToListAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
