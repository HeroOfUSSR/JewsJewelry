using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Common.Entity.EntityInterface;


namespace JewsJewelry.Common.Entity.DBInterface
{
    /// <summary>
    /// Интерфейс для записи в БД
    /// </summary>
    public interface IDBWriter
    {
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
        
        /// <summary>
        /// Обновление записи
        /// </summary>
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Удаление записи
        /// </summary>
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;

    }
}
