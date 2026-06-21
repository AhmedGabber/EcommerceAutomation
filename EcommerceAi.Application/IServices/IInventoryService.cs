using EcommerceAi.Application.Dtos.InventoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.IServices
{
    public interface IInventoryService
    {
        Task<List<InventoryDto>> GetAllAsync();

        Task<InventoryDto?> GetByProductIdAsync(Guid productId);

        Task UpdateAsync(Guid productId, UpdateInventoryDto dto);
    }
}
