using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.DTO
{
    public class OrderItemDTO
    {
        public string ProductId { get; set; } = null!;
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
}