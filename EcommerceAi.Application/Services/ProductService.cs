using EcommerceAi.Application.Dtos.ProductDtos;
using EcommerceAi.Application.IServices;
using EcommerceAi.Core.Domain_Models;
using EcommerceAi.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price
            }).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            await _repository.AddAsync(product);
        }

        public async Task UpdateAsync(Guid id, CreateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                throw new Exception("Product not found");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;

            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                throw new Exception("Product not found");

            await _repository.DeleteAsync(product);
        }
    }
}
