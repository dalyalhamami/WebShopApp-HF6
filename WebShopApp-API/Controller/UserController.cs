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

    // Get all users
    [HttpGet("GetUsers")]
    public async Task<ActionResult<List<UserResponseDto>>> GetUsers()
    {
        var users = await webShopAppDBContext.User
            .Select(user => new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Mobile = user.Mobile,
                Address = user.Address,
                Roles = user.Roles
            }).ToListAsync();

        return Ok(users);
    }

    // Get user by Id
    [HttpGet("GetUser/{userId}")]
    public async Task<ActionResult<UserResponseDto>> GetUser(int userId)
    {
        var user = await webShopAppDBContext.User
            .Where(x => x.Id == userId)
            .Select(user => new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Mobile = user.Mobile,
                Address = user.Address,
                Roles = user.Roles
            }).FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }

    [HttpPut("UpdateUser")]
    public async Task<ActionResult<UserResponseDto>> UpdateUser([FromBody] UserUpdate userUpdate)
    {
        if (userUpdate == null)
        {
            return BadRequest("Invalid request: user is null");
        }

        var existingUser = await webShopAppDBContext.User.FindAsync(userUpdate.Id);

        if (existingUser == null)
        {
            return NotFound("User not found");
        }

        // Update only the fields provided in the request
        if (userUpdate.Name != null)
            existingUser.Name = userUpdate.Name;

        if (userUpdate.Email != null)
            existingUser.Email = userUpdate.Email;

        if (userUpdate.Mobile != null)
            existingUser.Mobile = userUpdate.Mobile;

        if (userUpdate.Address != null)
            existingUser.Address = userUpdate.Address;

        try
        {
            await webShopAppDBContext.SaveChangesAsync();

            var response = new UserResponseDto
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email,
                Mobile = existingUser.Mobile,
                Address = existingUser.Address,
                Roles = existingUser.Roles
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Login user
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
