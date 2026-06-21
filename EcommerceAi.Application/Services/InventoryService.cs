using EcommerceAi.Application.Dtos.InventoryDtos;
using EcommerceAi.Application.IServices;
using EcommerceAi.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryDto>> GetAllAsync()
        {
            var inventories = await _repository.GetAllAsync();

            return inventories.Select(x => new InventoryDto
            {
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                AvailableQuantity = x.AvailableQuantity,
                ReservedQuantity = x.ReservedQuantity,
                OpenStockQuantity = x.OpenStockQuantity
            }).ToList();
        }

        public async Task<InventoryDto?> GetByProductIdAsync(Guid productId)
        {
            var inventory = await _repository.GetByProductIdAsync(productId);

            if (inventory == null)
                return null;

            return new InventoryDto
            {
                ProductId = inventory.ProductId,
                ProductName = inventory.Product.Name,
                AvailableQuantity = inventory.AvailableQuantity,
                ReservedQuantity = inventory.ReservedQuantity,
                OpenStockQuantity = inventory.OpenStockQuantity
            };
        }

        public async Task UpdateAsync(Guid productId, UpdateInventoryDto dto)
        {
            var inventory = await _repository.GetByProductIdAsync(productId);

            if (inventory == null)
                throw new Exception("Inventory not found");

            inventory.AvailableQuantity = dto.AvailableQuantity;
            inventory.ReservedQuantity = dto.ReservedQuantity;
            inventory.OpenStockQuantity = dto.OpenStockQuantity;

            await _repository.UpdateAsync(inventory);
        }
    }
}
