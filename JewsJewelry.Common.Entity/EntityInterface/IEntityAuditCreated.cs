using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит создания сущности
    /// </summary>
    public interface IEntityAuditCreated
    {
        /// <summary>
        /// Когда создано
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Кем создано
        /// </summary>
        public string CreatedBy { get; set; }
    }
}
