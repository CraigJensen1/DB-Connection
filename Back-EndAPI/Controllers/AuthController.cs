using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back_EndAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        //for now we aren't checking database or caring about encryption
        if (request.Username != "Admin" || request.Password != "password")
        {
            return Unauthorized();
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_MY_SECRET_KEY_1234567890"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("permission", "users.create")
        };

        var token = new JwtSecurityToken(
            claims:claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new { token = tokenString });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
