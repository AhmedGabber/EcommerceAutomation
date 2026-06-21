using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }

        public List<CreateOrderItemDto> Items { get; set; }
            = new();
    }
}
