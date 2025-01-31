using System;

namespace MultiVendorEcommerceAPI.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // FK to Order
        public string TrackingNumber { get; set; }
        public string Status { get; set; } // In Transit, Out for Delivery, Delivered
        public DateTime? DeliveryDate { get; set; }
        public string CourierName { get; set; }

        // Relationships
        public Order Order { get; set; }
    }
}
