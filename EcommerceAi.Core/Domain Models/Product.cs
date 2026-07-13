using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Pgvector;
using System.Threading.Tasks;


namespace EcommerceAi.Core.Domain_Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Pgvector.Vector? Embedding { get; set; }
    }
}
