namespace OrderAPI.DTOs
{
    public class PaymentEventDTO
    {
        public string orderId { get; set; } = null!;
        public string eventType { get; set; } = null!;
        
    }
}