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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == registerDto.UserName))
            {
                return BadRequest("Username is already taken.");
            }

            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return BadRequest("Email is already in use.");
            }

            var user = new User(registerDto.UserName, registerDto.Password, registerDto.Email);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful." });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);

            if (user == null || user.PasswordHash != user.HashPassword(loginDto.Password))
            {
                return Unauthorized("Invalid username or password.");
            }
          
            return Ok(new { message = "Login successful.", userId = user.Id });
        }
    }
}
