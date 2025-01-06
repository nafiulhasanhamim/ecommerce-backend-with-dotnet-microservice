using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Models
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public bool OrderConfirmationStatus { get; set; }
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();

    }
}