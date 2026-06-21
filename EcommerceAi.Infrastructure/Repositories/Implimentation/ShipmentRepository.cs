using EcommerceAi.Core.Domain_Models;
using EcommerceAi.Infrastructure.DBContext;
using EcommerceAi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Infrastructure.Repositories.Implimentation
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly AppDbContext _context;

        public ShipmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shipment>> GetAllAsync()
        {
            return await _context.Shipments
                .Include(x => x.Order)
                .ToListAsync();
        }

        public async Task<Shipment?> GetByIdAsync(Guid id)
        {
            return await _context.Shipments
                .Include(x => x.Order)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Shipment?> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.Shipments
                .Include(x => x.Order)
                .FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task AddAsync(Shipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shipment shipment)
        {
            _context.Shipments.Update(shipment);

            await _context.SaveChangesAsync();
        }
    }
}
