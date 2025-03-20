using event_management_system_backend.Data;
using event_management_system_backend.DTOs;
using event_management_system_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace event_management_system_backend.Service
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? "Unknown"),
                new Claim(ClaimTypes.Email, user.Email ?? "no-email@example.com"),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"] ?? string.Empty,
                audience: _config["Jwt:Audience"] ?? string.Empty,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> Register(UserDTO userDTO, string role = "User")
        {
            if (await _context.Users.AnyAsync(u => u.Email == userDTO.Email))
                throw new Exception("Email already exists");

            var user = new User
            {
                Name = userDTO.Name ?? "Unknown",
                Email = userDTO.Email ?? "no-email@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password ?? string.Empty),
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Authenticate(LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password ?? string.Empty, user.PasswordHash ?? string.Empty))
                return new User(); // Ensure non-null return

            return user;
        }
    }
}
