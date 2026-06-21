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
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;

        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> GetAllAsync()
        {
            return await _context.Inventories
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<Inventory?> GetByProductIdAsync(Guid productId)
        {
            return await _context.Inventories
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);

            await _context.SaveChangesAsync();
        }
    }
}
