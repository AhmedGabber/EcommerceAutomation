using EcommerceAi.Application.Dtos.ShipmentDtos;
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
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IOrderRepository _orderRepository;

        public ShipmentService(
            IShipmentRepository shipmentRepository,
            IOrderRepository orderRepository)
        {
            _shipmentRepository = shipmentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<List<ShipmentDto>> GetAllAsync()
        {
            var shipments = await _shipmentRepository.GetAllAsync();

            return shipments.Select(MapShipment).ToList();
        }

        public async Task<ShipmentDto?> GetByIdAsync(Guid id)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(id);

            return shipment == null
                ? null
                : MapShipment(shipment);
        }

        public async Task<ShipmentDto?> GetByOrderIdAsync(Guid orderId)
        {
            var shipment = await _shipmentRepository.GetByOrderIdAsync(orderId);

            return shipment == null
                ? null
                : MapShipment(shipment);
        }

        public async Task CreateAsync(CreateShipmentDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(dto.OrderId);

            if (order == null)
                throw new Exception("Order not found");

            var existingShipment =
                await _shipmentRepository.GetByOrderIdAsync(dto.OrderId);

            if (existingShipment != null)
                throw new Exception("Shipment already exists");

            var shipment = new Shipment
            {
                Id = Guid.NewGuid(),
                OrderId = dto.OrderId,
                TrackingNumber = GenerateTrackingNumber(),
                Status = ShipmentStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
                IsDelayed = false
            };

            await _shipmentRepository.AddAsync(shipment);
        }

        public async Task UpdateStatusAsync(
            Guid shipmentId,
            string status)
        {
            var shipment =
                await _shipmentRepository.GetByIdAsync(shipmentId);

            if (shipment == null)
                throw new Exception("Shipment not found");

            shipment.Status =
                Enum.Parse<ShipmentStatus>(status, true);

            if (shipment.Status == ShipmentStatus.InTransit)
            {
                shipment.Order.Status = OrderStatus.Shipped;

                await _orderRepository.UpdateAsync(
                    shipment.Order);
            }

            if (shipment.Status == ShipmentStatus.Delivered)
            {
                shipment.DeliveredDate = DateTime.UtcNow;

                shipment.Order.Status =
                    OrderStatus.Delivered;

                await _orderRepository.UpdateAsync(
                    shipment.Order);
            }

            await _shipmentRepository.UpdateAsync(shipment);
        }

        public async Task MarkAsDelayedAsync(
            Guid shipmentId,
            string reason)
        {
            var shipment =
                await _shipmentRepository.GetByIdAsync(shipmentId);

            if (shipment == null)
                throw new Exception("Shipment not found");

            shipment.Status = ShipmentStatus.Delayed;

            shipment.IsDelayed = true;

            shipment.DelayReason = reason;

            await _shipmentRepository.UpdateAsync(shipment);
        }

        private static ShipmentDto MapShipment(
            Shipment shipment)
        {
            return new ShipmentDto
            {
                Id = shipment.Id,
                OrderId = shipment.OrderId,
                TrackingNumber = shipment.TrackingNumber,
                Status = shipment.Status.ToString(),
                ExpectedDeliveryDate =
                    shipment.ExpectedDeliveryDate,
                IsDelayed = shipment.IsDelayed,
                DelayReason = shipment.DelayReason
            };
        }

        private static string GenerateTrackingNumber()
        {
            return $"TRK-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }
}
