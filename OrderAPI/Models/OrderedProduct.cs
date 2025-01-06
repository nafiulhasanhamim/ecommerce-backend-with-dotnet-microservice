using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Models
{
    public class OrderedProduct
    {
        [Key]
        public string OrderedProductId { get; set; } = null!;

        [ForeignKey("Order")]
        public string OrderId { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitProductPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}