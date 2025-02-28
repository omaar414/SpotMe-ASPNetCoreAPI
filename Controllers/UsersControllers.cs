using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SpotMeAPI.DTOs.User;
using SpotMeAPI.Mappers;
using SpotMeAPI.Models;
using SpotMeAPI.Repositories.Interfaces;


namespace SpotMeAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UsersController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet("info")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var IdFromToken = User.FindFirst("userId");   
            if (IdFromToken == null) return Unauthorized(new { message = "User not authenticated" });

            if (!int.TryParse(IdFromToken.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID" });
            }
            
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return BadRequest(new {message = "User not found"});

            var userDto = user.FromUserToUserDTO();
            
            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto model)
        {
            if(!ModelState.IsValid) {return BadRequest(ModelState);}
        
            var userExist = await _userRepository.UsernameExists(model.Username);
            if (userExist){return BadRequest(new{message="Username already exist"});}
            
            
            var NewUser = model.FromCreateUserToUser();
            NewUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _userRepository.AddUser(NewUser);

            return Ok(new {meessage = "User created successfully"});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userRepository.GetUserByUsername(model.Username);
            if (user == null) { return NotFound(new { message = "User not found" });}

            var validPassword = _userRepository.IsValidPassword(model.Password, user.PasswordHash);
            if(!validPassword) { return Unauthorized(new{message = "Invalid Username or Password"}); }

            var token = GenerateJwtToken(user);
            return Ok(new {token = token});
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("Secret") ?? "default_secret_key"));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.GetValue<int?>("ExpirationInMinutes") ?? 60),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    
}