using EcommerceAi.Core.Domain_Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Core.Domain_Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Shipment? Shipment { get; set; }

        public ICollection<OrderItem> Items { get; set; }
            = new List<OrderItem>();
    }
}
