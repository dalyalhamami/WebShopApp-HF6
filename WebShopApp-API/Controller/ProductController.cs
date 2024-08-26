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
    [HttpPost("CreateProduct")]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
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
            return BadRequest("Category not found");
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

    // Update an existing product
    [HttpPut("UpdateProduct")]
    public async Task<ActionResult<Product>> UpdateProduct(Product editedProduct)
    {
        if (editedProduct.Id == null)
        {
            return BadRequest("Invalid ID");
        }

        webShopAppDBContext.Entry(editedProduct).State = EntityState.Modified;

        try
        {
            await webShopAppDBContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(editedProduct.Id))
            {
                return NotFound("Product not found");
            }
            else
            {
                throw;
            }
        }
        return Ok(editedProduct);
    }
    private bool ProductExists(int id)
    {
        return webShopAppDBContext.Product.Any(e => e.Id == id);
    }

    // Delete a product
    [HttpDelete("DeleteProduct/{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await webShopAppDBContext.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound("Product not found");
        }

        webShopAppDBContext.Product.Remove(product);
        await webShopAppDBContext.SaveChangesAsync();

        return NoContent();
    }

}
