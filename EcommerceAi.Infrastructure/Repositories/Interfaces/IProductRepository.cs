using EcommerceAi.Core.Domain_Models;
using Pgvector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(Guid id);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);

        Task<List<Product>> SearchByLikeAsync(List<string> keywords);

        Task<List<Product>> SearchSimilarAsync(Vector vector);
    }
}
