using JewsJewelry.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий считывания <see cref="Material"/>
    /// </summary>
    public interface IMaterialReadRepository
    {
        /// <summary>
        /// Список всех <see cref="Material"/>
        /// </summary>
        Task<IReadOnlyCollection<Material>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="Material"/>
        /// </summary>
        Task<Material?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификаторам <see cref="Material"/>
        /// </summary>
        Task<Dictionary<Guid, Material>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка на наличие <see cref="Material"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
