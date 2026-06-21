using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Dtos.InventoryDtos
{
    public class InventoryDto
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int AvailableQuantity { get; set; }

        public int ReservedQuantity { get; set; }

        public int OpenStockQuantity { get; set; }
    }
}
