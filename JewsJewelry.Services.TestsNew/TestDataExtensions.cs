using JewsJewelry.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.TestsNew
{
    public static class TestDataExtensions
    {
        public static void BaseAuditParameters<TEntity>(this TEntity entity) where TEntity : BaseAuditEntity
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedBy = $"Created by{Guid.NewGuid():N}";
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedBy = $"Updated by{Guid.NewGuid():N}";
        }
    }
}
