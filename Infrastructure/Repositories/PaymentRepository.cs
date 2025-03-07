using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync() =>
        await _context.Payments.ToListAsync();

    public async Task<Payment?> GetByIdAsync(int id) =>
        await _context.Payments.FindAsync(id);

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Payment payment)
    {
        _context.Payments.Update(payment);
        await _context.SaveChangesAsync();
    }
}
