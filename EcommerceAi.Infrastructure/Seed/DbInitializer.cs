using EcommerceAi.Core.Domain_Models;
using EcommerceAi.Core.Domain_Models.Enums;
using EcommerceAi.Infrastructure.DBContext;
using EcommerceAi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Pgvector;

namespace EcommerceAi.Infrastructure.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        // =========================
        // PRODUCTS
        // =========================
        if (!await context.Products.AnyAsync())
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "iPhone 15 Pro",
                    Description = "Apple mobile phone",
                    Price = 1200,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                },

                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Samsung S24 Ultra",
                    Description = "Samsung flagship phone",
                    Price = 1100,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                },

                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Dell XPS 15",
                    Description = "Dell laptop",
                    Price = 2000,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                },

                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "MacBook Pro M3",
                    Description = "Apple laptop",
                    Price = 2500,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                }
            };

            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();
        }

        // =========================
        // INVENTORY
        // =========================
        if (!await context.Inventories.AnyAsync())
        {
            var products = await context.Products.ToListAsync();

            var inventories = products.Select(product => new Inventory
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                AvailableQuantity = 100,
                ReservedQuantity = 10,
                OpenStockQuantity = 90,
                LastUpdated = DateTime.UtcNow
            }).ToList();

            await context.Inventories.AddRangeAsync(inventories);

            await context.SaveChangesAsync();
        }

        // =========================
        // CUSTOMERS
        // =========================
        if (!await context.Customers.AnyAsync())
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid(),
                    FullName = "Ahmed Abdelhamid",
                    Email = "ahmed@test.com",
                    PhoneNumber = "01000000001"
                },

                new Customer
                {
                    Id = Guid.NewGuid(),
                    FullName = "Mohamed Ali",
                    Email = "mohamed@test.com",
                    PhoneNumber = "01000000002"
                },

                new Customer
                {
                    Id = Guid.NewGuid(),
                    FullName = "Sara Hassan",
                    Email = "sara@test.com",
                    PhoneNumber = "01000000003"
                }
            };

            await context.Customers.AddRangeAsync(customers);

            await context.SaveChangesAsync();
        }

        // =========================
        // ORDERS + ORDER ITEMS
        // =========================
        if (!await context.Orders.AnyAsync())
        {
            var customer = await context.Customers.FirstAsync();

            var products = await context.Products.ToListAsync();

            var firstProduct = products[0];
            var secondProduct = products[1];

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                Status = OrderStatus.Pending,
                TotalAmount = firstProduct.Price + secondProduct.Price,
                CreatedAt = DateTime.UtcNow
            };

            await context.Orders.AddAsync(order);

            await context.SaveChangesAsync();

            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = firstProduct.Id,
                    Quantity = 1,
                    UnitPrice = firstProduct.Price
                },

                new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = secondProduct.Id,
                    Quantity = 1,
                    UnitPrice = secondProduct.Price
                }
            };

            await context.OrderItems.AddRangeAsync(orderItems);

            await context.SaveChangesAsync();
        }

        if (!await context.Shipments.AnyAsync())
        {
            var order = await context.Orders.FirstOrDefaultAsync();

            if (order != null)
            {
                await context.Shipments.AddAsync(new Shipment
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    TrackingNumber = $"TRK-{Random.Shared.Next(100000, 999999)}",
                    Status = ShipmentStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                    ExpectedDeliveryDate = DateTime.UtcNow.AddDays(5),
                    IsDelayed = false
                });

                await context.SaveChangesAsync();
            }
        }
    }
}