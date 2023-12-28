using JewsJewelry.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий считывания <see cref="Order"/>
    /// </summary>
    public interface IOrderReadRepository
    {
        /// <summary>
        /// Список всех <see cref="Material"/>
        /// </summary>
        Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="Order"/>
        /// </summary>
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /*/// <summary>
        /// Список по идентификаторам <see cref="Order"/>
        /// </summary>
        Task<Dictionary<Guid, Order>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);*/

        /// <summary>
        /// Проверка на наличие <see cref="Order"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
