namespace OrderAPI.DTOs
{
    public class OrderCreateDto
    {
        public List<OrderedProductCreateDto> Products { get; set; } = null!;
        public string CouponName { get; set; } = null!;
        public string Address { get; set; } = null!;
        // public string UserId { get; set; } = null!;
    }
    public class OrderReadDto
    {
        public string OrderId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public bool OrderConfirmationStatus { get; set; }
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public List<OrderedProductReadDto> OrderedProducts { get; set; } = new List<OrderedProductReadDto>();
    }

    public class OrderStatusUpdateDto
    {
        public string Status { get; set; } // Example: "Pending", "Shipped", "Delivered"
    }

    public class OrderCreatedEventDto
    {
        public Guid OrderId { get; set; }
        public string ProductId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int Quantity { get; set; }
    }

    public class OrderResponseDto
    {
        public string ProductId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int OrderedQuantity { get; set; }
        public decimal TotalItemPrice { get; set; }
    }

    public class OrderResultDto
    {
        public IEnumerable<OrderResponseDto> OrderDetails { get; set; }
        public decimal TotalOrderPrice { get; set; }
    }


}