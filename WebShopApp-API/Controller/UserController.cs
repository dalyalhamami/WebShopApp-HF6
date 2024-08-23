
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

    [HttpPost("CreateUser")]
    public async Task<ActionResult<UserResponseDto>> CreateUser(User user)
    {
        if (user == null)
        {
            return BadRequest("Invalid Request");
        }

        var existingUser = await webShopAppDBContext.User
            .FirstOrDefaultAsync(u => u.Email!.ToLower() == user.Email!.ToLower());
        if (existingUser != null)
        {
            return BadRequest("Email already exists");
        }

        var existingPassword = await webShopAppDBContext.User
            .FirstOrDefaultAsync(p => p.Password == user.Password);
        if (existingPassword != null)
        {
            return BadRequest("You can't use that password. Please choose another");
        }

        var result = webShopAppDBContext.User.Add(user).Entity;
        await webShopAppDBContext.SaveChangesAsync();

        var response = new UserResponseDto
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email,
            Mobile = result.Mobile,
            Address = result.Address,
            Roles = result.Roles
        };

        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserResponseDto>> Login([FromBody] UserLoginDto loginDto)
    {
        var user = await webShopAppDBContext.User
            .Where(x => x.Email!.ToLower().Equals(loginDto.Email.ToLower()) &&
                        x.Password == loginDto.Password)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("User not found");
        }

        var response = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Mobile = user.Mobile,
            Address = user.Address,
            Roles = user.Roles
        };

        return Ok(response);
    }

    // Check if an email is exist
    [HttpGet("CheckEmail/{email}")]
    public async Task<ActionResult<bool>> CheckEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Invalid Request");
        }

        var userWithEmailExists = await webShopAppDBContext.User
            .AnyAsync(x => x.Email!.ToLower() == email.ToLower());

        return Ok(userWithEmailExists);
    }

 
}
