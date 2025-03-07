namespace Core.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentTime { get; set; }
    public string PaymentMethod { get; set; } = null!; // QR, Card, Cash
    public bool IsSuccessful { get; set; } = false; // To'lov muvaffaqiyatli bo'lganmi
}
