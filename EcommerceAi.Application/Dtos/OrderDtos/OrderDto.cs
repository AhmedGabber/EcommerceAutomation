using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Dtos.OrderDtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public List<OrderItemDto> Items { get; set; }
            = new();
    }
}
