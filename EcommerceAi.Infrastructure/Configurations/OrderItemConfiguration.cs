using EcommerceAi.Core.Domain_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAi.Infrastructure.Configurations
{
    public class OrderItemConfiguration
    : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UnitPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Order)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);
        }
    }
}
