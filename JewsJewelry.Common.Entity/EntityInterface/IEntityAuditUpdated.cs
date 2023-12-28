using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит обновления сущности
    /// </summary>
    public interface IEntityAuditUpdated
    {
        /// <summary>
        /// Когда изменили
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Кто изменил
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}
