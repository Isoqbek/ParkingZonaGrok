using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
    {
        var payments = await _paymentRepository.GetAllAsync();
        return payments.Select(p => new PaymentDto
        {
            Id = p.Id,
            VehicleId = p.VehicleId,
            Amount = p.Amount,
            PaymentTime = p.PaymentTime,
            PaymentMethod = p.PaymentMethod,
            IsSuccessful = p.IsSuccessful
        });
    }

    public async Task<PaymentDto?> GetPaymentByIdAsync(int id)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);
        return payment == null ? null : new PaymentDto
        {
            Id = payment.Id,
            VehicleId = payment.VehicleId,
            Amount = payment.Amount,
            PaymentTime = payment.PaymentTime,
            PaymentMethod = payment.PaymentMethod,
            IsSuccessful = payment.IsSuccessful
        };
    }

    public async Task AddPaymentAsync(PaymentDto paymentDto)
    {
        var payment = new Payment
        {
            VehicleId = paymentDto.VehicleId,
            Amount = paymentDto.Amount,
            PaymentTime = paymentDto.PaymentTime,
            PaymentMethod = paymentDto.PaymentMethod,
            IsSuccessful = paymentDto.IsSuccessful
        };
        await _paymentRepository.AddAsync(payment);
    }

    public async Task UpdatePaymentAsync(PaymentDto paymentDto)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentDto.Id);
        if (payment == null) return;

        payment.Amount = paymentDto.Amount;
        payment.PaymentMethod = paymentDto.PaymentMethod;
        payment.IsSuccessful = paymentDto.IsSuccessful;

        await _paymentRepository.UpdateAsync(payment);
    }
}
