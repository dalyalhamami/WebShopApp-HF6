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
