using Microsoft.AspNetCore.Mvc;
using GovernmentPortal.Models;
using Serilog;

namespace GovernmentPortal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            var user = new User { Id = "user-123", Name = request.Username, Email = $"{request.Username}@example.gov" };
            Log.Information("Login attempt for user: {Username} at {time}", request.Username,DateTime.Now);
            return Ok(new { UserId = user.Id, Message = "Login successful" });
        }
        catch (Exception ex) {
            Log.Error(ex, "");
            return BadRequest(new { message = "Failed To Login, Username or Password is wrong" });
        }
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
