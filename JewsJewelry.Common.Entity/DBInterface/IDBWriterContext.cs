using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Common.Entity.DBInterface
{
    /// <summary>
    /// Контекст для работы с записями в БД
    /// </summary>
    public interface IDBWriterContext
    {
        /// <summary>
        /// <inheritdoc cref="IDBWriter"/>
        /// </summary>
        IDBWriter Writer { get; }

        /// <summary>
        /// <inheritdoc cref="IUnitOfWork"/>
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// <inheritdoc cref="IDateTimeProvider"/>
        /// </summary>
        IDateTimeProvider TimeProvider { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        string User { get; }
    }
}
