using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит удаления сущности
    /// </summary>
    public interface IEntityAuditDeleted
    {
        /// <summary>
        /// Когда удалили
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// Кто удалил
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}
