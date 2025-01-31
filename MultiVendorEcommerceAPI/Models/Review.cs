using System;

namespace MultiVendorEcommerceAPI.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK to User
        public int ProductId { get; set; } // FK to Product
        public int Rating { get; set; } // 1 to 5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
