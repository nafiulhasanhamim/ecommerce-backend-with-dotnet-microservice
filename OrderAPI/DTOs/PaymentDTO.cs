namespace OrderAPI.DTOs
{
    public class PaymentDTO
    {
        public string orderId { get; set; } = null!;
        public string userId { get; set; } = null!;
        public decimal amount { get; set; }
    }
    public class PaymentResponseDTO
    {
        public string bkashUrl { get; set; } = null!;
    }
    
}