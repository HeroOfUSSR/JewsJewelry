using JewsJewelry.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Tests
{
    internal static class TestDataExtensions
    {
        public static void BaseAuditParameters<TEntity>(this TEntity entity) where TEntity : BaseAuditEntity
        {
            entity.Id= Guid.NewGuid();
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = $"Created by{Guid.NewGuid():N}";
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = $"Updated by{Guid.NewGuid():N}";
        }
    }
}
