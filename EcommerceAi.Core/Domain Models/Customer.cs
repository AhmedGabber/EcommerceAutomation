using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Core.Domain_Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        // Navigation Property
        public ICollection<Order> Orders { get; set; }
            = new List<Order>();
    }
}
