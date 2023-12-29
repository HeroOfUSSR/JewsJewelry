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
    public class CustomerEntityTypeConfig : IEntityTypeConfiguration<Customer>
    {
        void IEntityTypeConfiguration<Customer>.Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(c => c.Id);
            builder.PropertyAuditConfigutaion();
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Surname).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Patronymic).HasMaxLength(100).IsRequired();
            builder.HasIndex(c => c.PhoneNumber).IsUnique().HasDatabaseName($"{nameof(Customer)} {nameof(Customer.PhoneNumber)}").HasFilter($"{nameof(Customer.DeletedAt)} is null");
            builder.Property(c => c.Email).HasMaxLength(100).IsRequired();
            builder.HasIndex(c => c.Email).IsUnique().HasDatabaseName($"{nameof(Customer)}_{nameof(Customer.Email)}").HasFilter($"{nameof(Customer.DeletedAt)} is null"); 
            builder.HasMany(c => c.Orders).WithOne(c => c.Customer).HasForeignKey(c => c.CustomerId);
        }
    }
}
