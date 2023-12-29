using JewsJewelry.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context.Contracts.Config.Configs
{
    public class OrderEntityTypeConfig : IEntityTypeConfiguration<Order>
    {
        void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);
            builder.PropertyAuditConfigutaion();
            builder.Property(o => o.Id).IsRequired();
            builder.Property(o => o.Name).HasMaxLength(50).IsRequired();
            builder.Property(o => o.OrderDate).IsRequired();
            builder.HasIndex(o => o.OrderDate).IsUnique().HasDatabaseName($"{nameof(Order)} {nameof(Order.OrderDate)}").HasFilter($"{nameof(Order.DeletedAt)} is null");
            builder.Property(o => o.DoneDate).IsRequired();
            builder.Property(o => o.JewelryId).IsRequired();
            builder.Property(o => o.CustomerId).IsRequired();
            builder.Property(o => o.WorkshopId).IsRequired();
        }
    }
}
