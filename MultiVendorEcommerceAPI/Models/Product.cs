using System;

namespace MultiVendorEcommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int StoreId { get; set; } // FK to Store
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; } // FK to Category
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public Store Store { get; set; }
        public Category Category { get; set; }
    }
}
