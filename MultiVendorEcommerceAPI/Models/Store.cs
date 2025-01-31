using System;
using System.Collections.Generic;

namespace MultiVendorEcommerceAPI.Models
{
    public class Store
    {
        public int Id { get; set; }
        public int OwnerId { get; set; } // FK to User
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public User Owner { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
    }
}
