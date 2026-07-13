using EcommerceAi.Application.Dtos.ProductDtos;
using EcommerceAi.Application.IServices;
using EcommerceAi.Core.Domain_Models;
using EcommerceAi.Infrastructure.Repositories.Interfaces;
using Pgvector;
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
        private readonly IEmbeddingService _embeddingService;
        private readonly IOllamaService _ollamaService;

        public ProductService(IProductRepository repository, IEmbeddingService embeddingService,IOllamaService ollamaService )
        {
            _repository = repository;
            _embeddingService = embeddingService;
            _ollamaService = ollamaService;
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
            var text =
                    $"""
                    Product Name: {dto.Name}

                    Description: {dto.Description}

                    Price: {dto.Price}
                    """;

            var embedding =
            await _embeddingService.GenerateEmbeddingAsync(text);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Embedding = new Pgvector.Vector(embedding)
            };

            await _repository.AddAsync(product);
        }

        public async Task UpdateAsync(Guid id, CreateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                throw new Exception("Product not found");

            var text =
        $"""
                    Product Name: {dto.Name}

                    Description: {dto.Description}

                    Price: {dto.Price}
                    """;

            var embedding =
            await _embeddingService.GenerateEmbeddingAsync(text);

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Embedding = new Pgvector.Vector(embedding);


            await _repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                throw new Exception("Product not found");

            await _repository.DeleteAsync(product);
        }

        public async Task<List<ProductDto>> SearchProductsAsync(string message)
        {
            // 1
            var keywords =
                await _ollamaService
                    .ExtractKeywordsAsync(message);

            // 2
            var products =
                await _repository
                    .SearchByLikeAsync(keywords);

            // 3
            if (products.Any())
            {
                return Map(products);
            }

            // 4
            var embedding =
                await _embeddingService
                    .GenerateEmbeddingAsync(message);

            // 5
            var vector =
                new Vector(embedding);

            // 6
            var ragProducts =
                await _repository
                    .SearchSimilarAsync(vector);

            return Map(ragProducts);
        }

        private static List<ProductDto> Map(List<Product> products)
        {
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();
        }
    }
}
