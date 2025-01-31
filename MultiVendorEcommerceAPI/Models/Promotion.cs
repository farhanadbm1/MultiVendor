using System;

namespace MultiVendorEcommerceAPI.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public int StoreId { get; set; } // FK to Store
        public string Code { get; set; } // e.g., "SAVE20"
        public decimal DiscountValue { get; set; } // Percentage or Fixed Amount
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Relationships
        public Store Store { get; set; }
    }
}
