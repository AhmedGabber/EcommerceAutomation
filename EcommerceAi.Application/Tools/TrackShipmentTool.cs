using EcommerceAi.Application.AI;
using EcommerceAi.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Tools
{
    public class TrackShipmentTool : IAIServiceTool
    {
        private readonly IShipmentService _shipmentService;

        public TrackShipmentTool(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        public string Name => "TrackShipment";

        public async Task<object> ExecuteAsync(Dictionary<string, object>? args)
        {
            var orderId = Guid.Parse(args["orderId"].ToString());

            return await _shipmentService.GetByOrderIdAsync(orderId);
        }
    }
}
