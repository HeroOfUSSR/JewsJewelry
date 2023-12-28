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
            builder.ToTable("Craftsmen")
            builder
        }
    }
}
