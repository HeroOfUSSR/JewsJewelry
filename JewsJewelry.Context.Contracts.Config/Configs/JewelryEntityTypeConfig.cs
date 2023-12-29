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
    public class JewelryEntityTypeConfig : IEntityTypeConfiguration<Jewelry>
    {
        void IEntityTypeConfiguration<Jewelry>.Configure(EntityTypeBuilder<Jewelry> builder)
        {
            builder.ToTable("Jewelry");
            builder.HasKey(j => j.Id);
            builder.PropertyAuditConfigutaion();
            builder.Property(j => j.Id).IsRequired();
            builder.Property(j => j.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(j => j.Name).IsUnique().HasDatabaseName($"{nameof(Jewelry)} {nameof(Jewelry.Name)}").HasFilter($"{nameof(Jewelry.DeletedAt)} is null");
            builder.Property(j => j.Cost).IsRequired();
            builder.Property(j => j.Weight).IsRequired();
            builder.Property(j => j.MaterialId).IsRequired();
            builder.HasMany(j => j.Orders).WithOne(j => j.Jewelries).HasForeignKey(j => j.JewelryId);

        }
    }
}
