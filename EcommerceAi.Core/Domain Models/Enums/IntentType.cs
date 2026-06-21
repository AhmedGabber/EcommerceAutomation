using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Core.Domain_Models.Enums
{
    public enum IntentType
    {
        Unknown,
        GetProducts,
        TrackShipment,
        CreateOrder,
        CancelOrder
    }
}
