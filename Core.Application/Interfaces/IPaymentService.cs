using Core.Application.DTOs;

namespace Core.Application.Interfaces;

public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
    Task<PaymentDto?> GetPaymentByIdAsync(int id);
    Task AddPaymentAsync(PaymentDto paymentDto);
    Task UpdatePaymentAsync(PaymentDto paymentDto);
}
