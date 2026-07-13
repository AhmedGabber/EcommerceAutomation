using EcommerceAi.Core.Domain_Models;
using EcommerceAi.Infrastructure.DBContext;
using EcommerceAi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Infrastructure.Repositories.Implimentation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> SearchSimilarAsync(Vector vector)
        {
            return await _context.Products
                .OrderBy(p => p.Embedding!.CosineDistance(vector))
                .Take(5)
                .ToListAsync();
        }


        public async Task<List<Product>> SearchByLikeAsync(List<string> keywords)
        {
            var predicate = PredicateBuilder.New<Product>(false);

            foreach (var keyword in keywords)
            {
                predicate = predicate.Or(p =>
                    EF.Functions.ILike(p.Name, $"%{keyword}%") ||
                    EF.Functions.ILike(p.Description, $"%{keyword}%"));
            }

            return await _context.Products
                .Where(predicate)
                .Take(5)
                .ToListAsync();
        }
    }
}
