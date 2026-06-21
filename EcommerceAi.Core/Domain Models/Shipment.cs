using EcommerceAi.Core.Domain_Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Core.Domain_Models
{
    public class Shipment
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string TrackingNumber { get; set; } = null!;

        public ShipmentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        public bool IsDelayed { get; set; }

        public string? DelayReason { get; set; }

        public Order Order { get; set; } = null!;
    }
}
