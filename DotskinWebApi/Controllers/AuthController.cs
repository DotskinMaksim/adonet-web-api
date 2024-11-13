// Controllers/AuthController.cs
using DotskinWebApi.Data;
using DotskinWebApi.DTOs;
using DotskinWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DotskinWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Очистить сессию
            return Ok("Logout successful.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == registerDto.UserName))
            {
                return BadRequest("Username is already taken.");
            }

            var user = new User
            {
                UserName = registerDto.UserName,
                PasswordHash = HashPassword(registerDto.Password),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registration successful.");
        }
        [HttpGet("check")]
        public IActionResult CheckAuth()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                return Ok();
            }
            return Unauthorized();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);

            if (user == null || user.PasswordHash != HashPassword(loginDto.Password))
            {
                return Unauthorized("Invalid username or password.");
            }
            HttpContext.Session.SetInt32("UserId", user.Id);

            return Ok("Login successful.");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
