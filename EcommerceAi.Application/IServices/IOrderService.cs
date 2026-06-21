using EcommerceAi.Application.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.IServices
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();

        Task<OrderDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateOrderDto dto);

        Task UpdateStatusAsync(Guid id, string status);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(Guid id, UpdateOrderDto dto);
        Task<OrderDto?> GetLatestOrderAsync(Guid customerId);
    }
}
