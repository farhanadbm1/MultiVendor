using System;
using System.Collections.Generic;

namespace MultiVendorEcommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK to User
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } // Pending, Shipped, Delivered, Cancelled
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }

        // Relationships
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
