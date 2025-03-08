using Core.Application.DTOs;

namespace Core.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto> RegisterUserAsync(string username, string password, string role);
    Task<AuthResultDto> LoginAsync(string username, string password);
    // Delete 
    Task DeleteAsync(string username);
}
