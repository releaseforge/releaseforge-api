using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReleaseForge.Infrastructure.Identity;

namespace ReleaseForge.Api.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    IOptions<JwtSettings> jwtSettings) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            return Conflict(new { Message = "Email is already registered" });

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            DisplayName = request.DisplayName
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

        var token = GenerateToken(user);
        return Ok(new AuthResponse(token, user.Email!, user.DisplayName));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Unauthorized(new { Message = "Invalid email or password" });

        var valid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!valid)
            return Unauthorized(new { Message = "Invalid email or password" });

        var token = GenerateToken(user);
        return Ok(new AuthResponse(token, user.Email!, user.DisplayName));
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return Unauthorized();

        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return Unauthorized();

        return Ok(new AuthResponse("", user.Email!, user.DisplayName));
    }

    private string GenerateToken(ApplicationUser user)
    {
        var settings = jwtSettings.Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.DisplayName ?? user.Email!)
        };

        var token = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(settings.ExpirationInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
