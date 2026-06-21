using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Core.Domain_Models.Enums
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
