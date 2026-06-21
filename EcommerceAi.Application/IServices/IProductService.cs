using EcommerceAi.Application.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.IServices
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();

        Task<ProductDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateProductDto dto);

        Task UpdateAsync(Guid id, CreateProductDto dto);

        Task DeleteAsync(Guid id);
    }
}
