namespace Core.Application.DTOs;

public class AuthResultDto
{
    public string Token { get; set; } = null!;
    public bool Success { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
