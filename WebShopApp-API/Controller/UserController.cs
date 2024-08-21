namespace Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly WebShopAppDBContext webShopAppDBContext;
    public UserController(WebShopAppDBContext webShopAppDBContext)
    {
        this.webShopAppDBContext = webShopAppDBContext;
    }

    // Create a new user
    [HttpPost("CreateUser")]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        if (user == null)
        {
            return BadRequest("Invalid Request");
        }

        var existingUser = await webShopAppDBContext.User.FirstOrDefaultAsync(u => u.Email!.ToLower() == user.Email!.ToLower());
        if (existingUser != null)
        {
            return BadRequest("Email already exists");
        }

        var existingPassword = await webShopAppDBContext.User.FirstOrDefaultAsync(p => p.Password == user.Password);
        if (existingPassword != null)
        {
            return BadRequest("You can't use that password. Please choose another");
        }

        var result = webShopAppDBContext.User.Add(user).Entity;
        await webShopAppDBContext.SaveChangesAsync();
        return Ok(result);
    }
}
