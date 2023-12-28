using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Common.Entity.DBInterface
{
    /// <summary>
    /// Интерфейс для сохранения БД
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Асинхронно сохраняет изменения в бд
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken= default);
    }
}
