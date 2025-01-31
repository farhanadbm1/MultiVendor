using Microsoft.EntityFrameworkCore;
using MultiVendorEcommerceAPI.Models;

namespace MultiVendorEcommerceAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		// Add DbSet properties for each model/table
		public DbSet<User> Users { get; set; }
		public DbSet<Store> Stores { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Promotion> Promotions { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<Delivery> Deliveries { get; set; }
		public DbSet<Chat> Chats { get; set; }

		// Optional: Configure relationships using Fluent API
		protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Promotion>()
        .Property(p => p.DiscountValue)
        .HasPrecision(10, 2);

    // Configure Chat relationships
    modelBuilder.Entity<Chat>()
        .HasOne(chat => chat.Sender)
        .WithMany() // No navigation property from User to Chats sent
        .HasForeignKey(chat => chat.SenderId)
        .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

    modelBuilder.Entity<Chat>()
        .HasOne(chat => chat.Receiver)
        .WithMany() // No navigation property from User to Chats received
        .HasForeignKey(chat => chat.ReceiverId)
        .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

    // Configure OrderItems and Products relationship
    modelBuilder.Entity<OrderItem>()
        .HasOne(orderItem => orderItem.Product)
        .WithMany() // No navigation property from Product to OrderItems
        .HasForeignKey(orderItem => orderItem.ProductId)
        .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete

    // Configure OrderItems and Orders relationship
    modelBuilder.Entity<OrderItem>()
        .HasOne(orderItem => orderItem.Order)
        .WithMany(order => order.OrderItems) // Assuming Order has a collection of OrderItems
        .HasForeignKey(orderItem => orderItem.OrderId)
        .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete

    // Configure Reviews and Users relationship
    modelBuilder.Entity<Review>()
        .HasOne(review => review.User)
        .WithMany() // No navigation property from User to Reviews
        .HasForeignKey(review => review.UserId)
        .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete

    // Configure ShoppingCarts and Users relationship
    modelBuilder.Entity<ShoppingCart>()
        .HasOne(cart => cart.User)
        .WithMany() // No navigation property from User to ShoppingCarts
        .HasForeignKey(cart => cart.UserId)
        .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete
}
	}
}
