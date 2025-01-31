using System;

namespace MultiVendorEcommerceAPI.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int SenderId { get; set; } // FK to User
        public int ReceiverId { get; set; } // FK to User
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
