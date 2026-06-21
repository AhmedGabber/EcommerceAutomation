using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Dtos.ShipmentDtos
{
    public class ShipmentDto
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string TrackingNumber { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime? ExpectedDeliveryDate { get; set; }

        public bool IsDelayed { get; set; }

        public string? DelayReason { get; set; }
    }
}
