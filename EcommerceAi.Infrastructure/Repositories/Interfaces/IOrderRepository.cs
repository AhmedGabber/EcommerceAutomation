using EcommerceAi.Core.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(Guid id);

        Task AddAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(Order order);

        Task<Order?> GetLatestOrderByCustomerAsync(Guid customerId);
    }
}
