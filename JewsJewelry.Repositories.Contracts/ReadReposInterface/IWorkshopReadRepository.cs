using JewsJewelry.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий считывания <see cref="Workshop"/>
    /// </summary>
    public interface IWorkshopReadRepository
    {
        /// <summary>
        /// Список всех <see cref="Workshop"/>
        /// </summary>
        Task<IReadOnlyCollection<Workshop>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="Workshop"/>
        /// </summary>
        Task<Workshop?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификаторам <see cref="Workshop"/>
        /// </summary>
        Task<Dictionary<Guid, Workshop>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка на наличие <see cref="Workshop"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
