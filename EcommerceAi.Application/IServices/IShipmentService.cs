using EcommerceAi.Application.Dtos.ShipmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.IServices
{
    public interface IShipmentService
    {
        Task<List<ShipmentDto>> GetAllAsync();

        Task<ShipmentDto?> GetByIdAsync(Guid id);

        Task<ShipmentDto?> GetByOrderIdAsync(Guid orderId);

        Task CreateAsync(CreateShipmentDto dto);

        Task UpdateStatusAsync(Guid shipmentId, string status);

        Task MarkAsDelayedAsync(Guid shipmentId, string reason);
    }
}
