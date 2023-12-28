using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит сущности с Id
    /// </summary>
    public interface IEntityId
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public Guid Id { get; set; }
    }
}
