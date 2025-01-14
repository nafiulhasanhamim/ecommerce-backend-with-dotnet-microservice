namespace ProductAPI.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ProductImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public int ThreshholdQuantity { get; set; }
        public string CategoryId { get; set; } = null!;
    }

    public class ProductReadDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string ProductImageUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public int ThreshholdQuantity { get; set; }
        public bool CategoryVerify { get; set; }
        public string CategoryId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

    public class ProductUpdateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ProductImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public int ThreshholdQuantity { get; set; }
        public string CategoryId { get; set; } = null!;
        public bool CategoryVerify { get; set; }

    }
}