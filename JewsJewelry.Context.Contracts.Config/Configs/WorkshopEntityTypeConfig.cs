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
    public class WorkshopEntityTypeConfig : IEntityTypeConfiguration<Workshop>
    {
        void IEntityTypeConfiguration<Workshop>.Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.ToTable("Workshop");
            builder.HasKey(w => w.Id);
            builder.PropertyAuditConfigutaion();
            builder.Property(w => w.Id).IsRequired();
            builder.Property(w => w.Name).HasMaxLength(100).IsRequired();
            builder.Property(w => w.Address).IsRequired();
            builder.HasIndex(w => w.Address).IsUnique().HasDatabaseName($"{nameof(Workshop)} {nameof(Workshop.Address)}").HasFilter($"{nameof(Workshop.DeletedAt)} is null");
            builder.Property(w => w.Speciality).IsRequired();
            builder.Property(w => w.Workplaces).IsRequired();
            builder.HasMany(w => w.Orders).WithOne(w => w.Workshop).HasForeignKey(w => w.WorkshopId);
            builder.HasMany(w => w.Craftsman).WithOne(w => w.Workshops).HasForeignKey(w => w.WorkshopId);
        }
    }
}
