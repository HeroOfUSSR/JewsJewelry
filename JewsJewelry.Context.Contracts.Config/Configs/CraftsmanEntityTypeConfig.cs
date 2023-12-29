using JewsJewelry.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context.Contracts.Config.Configs
{
    public class CraftsmanEntityTypeConfig : IEntityTypeConfiguration<Craftsman>
    {
        void IEntityTypeConfiguration<Craftsman>.Configure(EntityTypeBuilder<Craftsman> builder)
        {
            builder.ToTable("Craftsmen");
            builder.HasKey(c => c.Id);
            builder.PropertyAuditConfigutaion();
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Surname).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Patronymic).HasMaxLength(100).IsRequired();
            builder.HasIndex(c => c.PhoneNumber).IsUnique().HasDatabaseName($"{nameof(Craftsman)} {nameof(Craftsman.PhoneNumber)}").HasFilter($"{nameof(Craftsman.DeletedAt)} is null");
            builder.Property(c => c.Age).IsRequired();
            builder.Property(c => c.WorkshopId).IsRequired();
        }
    }
}
