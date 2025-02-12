using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MultiVendorEcommerceAPI.Data;
using MultiVendorEcommerceAPI.Models;
using MultiVendorEcommerceAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;

namespace MultiVendorEcommerceAPI.Controllers
{
	[ApiController]
	[Route("MultiVendorEcommerceAPI/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;
		private readonly ILogger<AuthController> _logger;

		public AuthController(ApplicationDbContext context, IConfiguration configuration, ILogger<AuthController> logger)
		{
			_context = context;
			_configuration = configuration;
			_logger = logger;
		}

		// Register a new user
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserRegistrationDTO userDto)
		{
			// Check if the email is already registered
			if (_context.Users.Any(u => u.Email == userDto.Email))
			{
				return BadRequest("Email already exists.");
			}

			// Hash the password
			var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

			// Create a new user
			var user = new User
			{
				Name = userDto.Name,
				Email = userDto.Email,
				Password = hashedPassword,
				Role = userDto.Role,
				Address = userDto.Address,
				PhoneNumber = userDto.PhoneNumber
			};

			// Save the user to the database
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return Ok("User registered successfully.");
		}

		// Login and generate JWT token
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
		{
			// Find the user by email
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);

			// Check if the user exists and the password is correct
			if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
			{
				return Unauthorized("Invalid email or password.");
			}

			// Generate a JWT token
			var token = GenerateJwtToken(user);

			_logger.LogInformation("Generated JWT Token: {Token}", token);

			return Ok(new { Token = token });
		}

		// Helper method to generate JWT token
		private string GenerateJwtToken(User user)
		{
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
			var tokenHandler = new JwtSecurityTokenHandler();

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.Role, user.Role)
				}),
				Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"]
			};

			var token = tokenHandler.CreateToken
			(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			// Log the token and its claims
			_logger.LogInformation("Generated JWT Token: {Token}", tokenString);
			_logger.LogInformation("Token Claims: {Claims}", string.Join(", ", tokenDescriptor.Subject.Claims.Select(c => $"{c.Type}: {c.Value}")));

			return tokenString;
		}
	}
}