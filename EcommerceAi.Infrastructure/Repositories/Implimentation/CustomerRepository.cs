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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Customers
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(
                    x => x.PhoneNumber == phoneNumber);
        }
    }
}
