﻿namespace WebShopApp_API.Controller;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly WebShopAppDBContext webShopAppDBContext;
    public CategoryController(WebShopAppDBContext webShopAppDBContext)
    {
        this.webShopAppDBContext = webShopAppDBContext;
    }

    // Create a new category
    [HttpPost("CreateCategory")]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        if (category != null)
        {
            var result = webShopAppDBContext.Category.Add(category).Entity;
            await webShopAppDBContext.SaveChangesAsync();
            return Ok(result);
        }
        return BadRequest("Invalid Request");
    }

    // Get all categories
    [HttpGet("GetCategories")]
    public async Task<ActionResult<List<Category>>> GetCategories()
    {
        try
        {
            var categories = await webShopAppDBContext.Category.ToListAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Get category by Id
    [HttpGet("GetCategoryById/{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        try
        {
            var category = await webShopAppDBContext.Category.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                return NotFound("Category not found");
            }

            return Ok(category);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Update an existing category
    [HttpPut("EditCategory")]
    public async Task<ActionResult<Category>> EditCategory(Category editedCategory)
    {
        if (editedCategory.Id == null)
        {
            return BadRequest("Invalid ID");
        }

        webShopAppDBContext.Entry(editedCategory).State = EntityState.Modified;

        try
        {
            await webShopAppDBContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(editedCategory.Id))
            {
                return NotFound("Category not found");
            }
            else
            {
                throw;
            }
        }

        return Ok(editedCategory);
    }

    private bool CategoryExists(int id)
    {
        return webShopAppDBContext.Category.Any(e => e.Id == id);
    }

    // Delete an existing category
    [HttpDelete("DeleteCategory/{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var category = await webShopAppDBContext.Category.FindAsync(id);
        if (category == null)
        {
            return NotFound("Category not found");
        }

        webShopAppDBContext.Category.Remove(category);
        await webShopAppDBContext.SaveChangesAsync();

        return NoContent();
    }
}
