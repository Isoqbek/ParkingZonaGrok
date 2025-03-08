using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private static readonly string[]  AllowedRoles = { "User", "Admin" };

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<AuthResultDto> RegisterUserAsync(string username, string password, string role)
    {

        if (!AllowedRoles.Contains(role))
        {
            return new AuthResultDto { Success = false, Errors = new[] { "Invalid role , Allowed roles are: User, Admin" } };
        }

        var existingUser = await _userRepository.GetByUsernameAsync(username);
        if (existingUser != null)
        {
            return new AuthResultDto { Success = false, Errors = new[] { "User already exists" } };
        }

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };

        await _userRepository.AddAsync(user);

        return await GenerateTokenAsync(user);
    }

    public async Task<AuthResultDto> LoginAsync(string username, string password)
    {
        // In-Memiory Authentication SuperAdmin 
        var superAdminUsername = _configuration["SuperAdmin:Username"];
        var superAdminPassword = _configuration["SuperAdmin:Password"];

        if (username == superAdminUsername && password == superAdminPassword)
        {
            var superAdmin = new User
            {
                Username = superAdminUsername,
                Role = "SuperAdmin"
            };
            return await GenerateTokenAsync(superAdmin);
        }

        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return new AuthResultDto { Success = false, Errors = new[] { "Invalid username or password" } };
        }

        return await GenerateTokenAsync(user);
    }

    public async Task DeleteAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        await _userRepository.DeleteAsync(user);
    }

    private async Task<AuthResultDto> GenerateTokenAsync(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!)),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthResultDto { Success = true, Token = tokenHandler.WriteToken(token) };
    }
}
