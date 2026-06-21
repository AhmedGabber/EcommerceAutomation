using EcommerceAi.Application.Dtos.CustomerDtos;
using EcommerceAi.Application.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();

        Task<CustomerDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateCustomerDto dto);

        Task UpdateAsync(Guid id, UpdateCustomerDto dto);

        Task DeleteAsync(Guid id);

        Task<List<OrderDto>> GetCustomerOrdersAsync(Guid customerId);

        Task<CustomerDto?> GetByPhoneNumberAsync(string phoneNumber);
    }
}
