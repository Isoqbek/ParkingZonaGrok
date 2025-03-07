namespace Core.Application.DTOs;

public class PaymentDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentTime { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public bool IsSuccessful { get; set; }
}
