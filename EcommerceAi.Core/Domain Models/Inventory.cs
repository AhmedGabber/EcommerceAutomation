using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Core.Domain_Models
{
    public class Inventory
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int AvailableQuantity { get; set; }

        public int ReservedQuantity { get; set; }

        public int OpenStockQuantity { get; set; }

        public DateTime LastUpdated { get; set; }
            = DateTime.UtcNow;
    }
}
