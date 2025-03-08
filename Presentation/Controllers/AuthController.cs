using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register-admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto dto)
    {
        if (dto.Role != "Admin")
        {
            return BadRequest("Role must be Admin");
        }

        var result = await _authService.RegisterUserAsync(dto.Username, dto.Password, dto.Role);
        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result);
    }

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        if (!string.Equals(dto.Role, "User", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Invalid role. Only 'User' can register.");
        }

        var result = await _authService.RegisterUserAsync(dto.Username, dto.Password, dto.Role);
        if (!result.Success)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto.Username, dto.Password);
        if (!result.Success)
        {
            return Unauthorized(result.Errors);
        }

        return Ok(result);
    }

    [HttpDelete("delete-user")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> DeleteUser([FromBody] RegisterDto dto)
    {
        try
        {
            await _authService.DeleteAsync(dto.Username);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

public class RegisterDto
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string Role { get; set; } = null!;
}

public class LoginDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
