using JewsJewelry.Common.Entity.EntityInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Common.Entity.DBInterface
{
    /// <summary>
    /// Интерфейс для чтения БД
    /// </summary>
    public interface IDBRead
    {
        /// <summary>
        /// Надо для выполнения запросов!!!
        /// </summary>
        IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity;
    }
}
