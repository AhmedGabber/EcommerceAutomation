using EcommerceAi.Core.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(Guid id);

        Task AddAsync(Customer customer);

        Task UpdateAsync(Customer customer);

        Task DeleteAsync(Customer customer);

        Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
    }
}
