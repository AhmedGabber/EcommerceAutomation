using EcommerceAi.Core.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Infrastructure.Repositories.Interfaces
{
    public interface IShipmentRepository
    {
        Task<List<Shipment>> GetAllAsync();

        Task<Shipment?> GetByIdAsync(Guid id);

        Task<Shipment?> GetByOrderIdAsync(Guid orderId);

        Task AddAsync(Shipment shipment);

        Task UpdateAsync(Shipment shipment);
    }
}
