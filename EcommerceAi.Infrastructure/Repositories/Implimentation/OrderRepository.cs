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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }
        public async Task<Order?> GetLatestOrderByCustomerAsync(Guid customerId)
        {
            return await _context.Orders
                .Include(x => x.Items)
                .Include(x => x.Customer)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(
                    x => x.CustomerId == customerId);
        }
    }
}
