using EcommerceAi.Application.Dtos.OrderDtos;
using EcommerceAi.Application.IServices;
using EcommerceAi.Core.Domain_Models;
using EcommerceAi.Core.Domain_Models.Enums;
using EcommerceAi.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CustomerName = order.Customer.FullName,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,

                Items = order.Items.Select(item => new OrderItemDto
                {
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto?> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.Customer.FullName,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,

                Items = order.Items.Select(item => new OrderItemDto
                {
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
        }

        public async Task CreateAsync(CreateOrderDto dto)
        {
            decimal total = 0;

            var orderItems = new List<OrderItem>();

            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new Exception("Product not found");

                total += product.Price * item.Quantity;

                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                TotalAmount = total,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                Items = orderItems
            };

            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateStatusAsync(Guid id, string status)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception("Order not found");

            order.Status = Enum.Parse<OrderStatus>(status);

            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception("Order not found");

            await _orderRepository.DeleteAsync(order);
        }

        public async Task UpdateAsync(Guid id, UpdateOrderDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception("Order not found");

            order.Items.Clear();

            decimal total = 0;

            foreach (var item in dto.Items)
            {
                var product = await _productRepository
                    .GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new Exception("Product not found");

                total += product.Price * item.Quantity;

                order.Items.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            order.TotalAmount = total;

            await _orderRepository.UpdateAsync(order);
        }

        public async Task<OrderDto?> GetLatestOrderAsync(Guid customerId)
        {
            var order =
                await _orderRepository
                    .GetLatestOrderByCustomerAsync(
                        customerId);

            if (order == null)
                return null;

            return MapOrder(order);
        }

        private static OrderDto MapOrder(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.Customer.FullName,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,

                Items = order.Items.Select(item => new OrderItemDto
                {
                    ProductName = item.Product?.Name ?? string.Empty,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
        }
    }
}
