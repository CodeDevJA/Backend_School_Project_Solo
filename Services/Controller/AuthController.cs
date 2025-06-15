using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext context;
    private readonly UserManager<UserEntity> userManager;

    public AuthController(
        AppDbContext context,
        UserManager<UserEntity> userManager
    )
    {
        this.context = context;
        this.userManager = userManager;
    }

    /// <summary>
    /// A secured test endpoint that returns a greeting for the authenticated user.
    /// </summary>
    /// <returns>
    /// 200 OK with a greeting message if the user is authenticated and exists;
    /// otherwise, 401 Unauthorized.
    /// </returns>
    [HttpGet("first")]
    [Authorize]
    public IActionResult SecuredEndpoint()
    {
        foreach (var claim in User.Claims)
        {
            Console.WriteLine(claim.ToString());
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized();
        }

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized(); // Invalid GUID format
        }

        var user = context.Users.Find(userId);
        if (user == null)
        {
            return Unauthorized();
        }

        return Ok("Hello " + user.UserName + "!");
    }

    /// <summary>
    /// Registers a new user with the provided username and password.
    /// </summary>
    /// <param name="request">The registration request containing username and password.</param>
    /// <returns>
    /// 200 OK if registration succeeds; otherwise, 400 Bad Request with validation errors.
    /// </returns>
    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var userEntity = new UserEntity();
        userEntity.UserName = request.UserName;

        IdentityResult result = await userManager.CreateAsync(userEntity, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}
