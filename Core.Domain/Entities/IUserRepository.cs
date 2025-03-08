using Core.Domain.Entities;

namespace Core.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task DeleteAsync(User user);
}
