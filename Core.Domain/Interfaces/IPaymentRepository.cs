using Core.Domain.Entities;

namespace Core.Domain.Interfaces;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(int id);
    Task AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
}
