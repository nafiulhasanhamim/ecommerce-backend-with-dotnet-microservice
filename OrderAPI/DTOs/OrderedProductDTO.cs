using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.DTOs
{
    public class OrderedProductCreateDto
    {
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
    }
    public class OrderedProductReadDto
    {
        public string OrderedProductId { get; set; } = null!;
        public string OrderId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitProductPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class OrderedProductUpdateDto
    {
        public string OrderedProductId { get; set; } = null!;
        public int Quantity { get; set; }
    }

}