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
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TrackingNumber)
                .HasMaxLength(100);

            builder.Property(x => x.DelayReason)
                .HasMaxLength(500);

            builder.HasOne(x => x.Order)
                .WithOne(x => x.Shipment)
                .HasForeignKey<Shipment>(x => x.OrderId);

            builder.HasIndex(x => x.TrackingNumber)
                .IsUnique();
        }
    }
}
