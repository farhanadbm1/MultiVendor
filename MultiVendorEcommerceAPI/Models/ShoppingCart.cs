namespace MultiVendorEcommerceAPI.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK to User
        public int ProductId { get; set; } // FK to Product
        public int Quantity { get; set; }

        // Relationships
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
