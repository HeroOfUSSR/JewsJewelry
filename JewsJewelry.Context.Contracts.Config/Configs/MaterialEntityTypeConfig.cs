using JewsJewelry.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JewsJewelry.Context.Contracts.Config.Configs
{
    public class MaterialEntityTypeConfig : IEntityTypeConfiguration<Material>
    {
        void IEntityTypeConfiguration<Material>.Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Material");
            builder.HasKey(m => m.Id);
            builder.PropertyAuditConfigutaion();
            builder.Property(m => m.Id).IsRequired();
            builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(m => m.Name).IsUnique().HasDatabaseName($"{nameof(Material)}_{nameof(Material.Name)}").HasFilter($"{nameof(Material.DeletedAt)} is null");
            builder.Property(m => m.Color).HasMaxLength(100).IsRequired();
            builder.HasIndex(m => m.Color).IsUnique().HasDatabaseName($"{nameof(Material)}_{nameof(Material.Color)}").HasFilter($"{nameof(Material.DeletedAt)} is null");
            builder.Property(m => m.Sample).IsRequired();
            builder.Property(m => m.Amount).IsRequired();
            builder.HasMany(m => m.Jewelries).WithOne(m => m.Material).HasForeignKey(m => m.MaterialId);
        }
    }
}
