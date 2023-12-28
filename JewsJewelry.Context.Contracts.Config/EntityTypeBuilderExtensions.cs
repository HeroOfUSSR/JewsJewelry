using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JewsJewelry.Context.Contracts.Models;
using System.Security.Cryptography.X509Certificates;

namespace JewsJewelry.Context.Contracts.Config

{
    static internal class EntityTypeBuilderExtensions
    {
        public static void PropertyAuditConfigutaion<T>(this EntityTypeBuilder<T> builder) where T : BaseAuditEntity
        {

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(250);
            builder.Property(x => x.UpdatedAt).IsRequired();
            builder.Property(x => x.UpdatedBy).IsRequired().HasMaxLength(250);

        }
    }
}