namespace OrderAPI.DTOs
{
    public class CouponReadDto
    {
        public string? CouponId { get; set; }
        public string? CouponName { get; set; }
        public bool Status { get; set; }
        public decimal MinimumShoppingAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}