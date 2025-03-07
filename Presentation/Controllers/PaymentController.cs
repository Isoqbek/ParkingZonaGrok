using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPayments()
    {
        var payments = await _paymentService.GetAllPaymentsAsync();
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        var payment = await _paymentService.GetPaymentByIdAsync(id);
        if (payment == null)
            return NotFound();

        return Ok(payment);
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] PaymentDto paymentDto)
    {
        await _paymentService.AddPaymentAsync(paymentDto);
        return CreatedAtAction(nameof(GetPaymentById), new { id = paymentDto.Id }, paymentDto);
    }
}
