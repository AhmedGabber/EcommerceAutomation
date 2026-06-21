using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Dtos.ShipmentDtos
{
    public class CreateShipmentDto
    {
        public Guid OrderId { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }
    }
}
