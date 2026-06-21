using EcommerceAi.Core.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Infrastructure.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<List<Inventory>> GetAllAsync();

        Task<Inventory?> GetByProductIdAsync(Guid productId);

        Task UpdateAsync(Inventory inventory);
    }
}
