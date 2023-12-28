using JewsJewelry.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий считывания <see cref="Jewelry"/>
    /// </summary>
    public interface IJewelryReadRepository
    {
        /// <summary>
        /// Список всех <see cref="Jewelry"/>
        /// </summary>
        Task<IReadOnlyCollection<Jewelry>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="Jewelry"/>
        /// </summary>
        Task<Jewelry?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификаторам <see cref="Jewelry"/>
        /// </summary>
        Task<Dictionary<Guid, Jewelry>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка на наличие <see cref="Jewelry"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
