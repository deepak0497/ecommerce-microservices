using Microsoft.AspNetCore.Mvc;
using AuthService.Data;
using AuthService.DTOs;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;

namespace AuthService.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthDbContext _context;
    private readonly JwtService _jwtService;
    public AuthController(AuthDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "Customer"
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User Registered Successfully");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email);

        if (user == null)
            return Unauthorized("Invalid Email");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid Password");
        var token = _jwtService.GenerateToken(user);    
        return Ok(new {token});
    }
    [Authorize]
    [HttpGet ("profile")]
    public IActionResult GetProfile()
    {

        return Ok("Authenticated User Profile");
        //var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        //if (userId == null)
        //    return Unauthorized();
        //var user = _context.Users.FirstOrDefault(x => x.Id.ToString() == userId);
        //if (user == null)
        //    return NotFound();
        //return Ok(new { user.Email, user.Role });
    }
}