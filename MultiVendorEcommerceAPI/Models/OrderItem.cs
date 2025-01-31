namespace MultiVendorEcommerceAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // FK to Order
        public int ProductId { get; set; } // FK to Product
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; } // Quantity * UnitPrice

        // Relationships
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
