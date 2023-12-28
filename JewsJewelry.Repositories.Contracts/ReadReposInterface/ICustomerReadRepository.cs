using JewsJewelry.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий считывания <see cref="Customer"/>
    /// </summary>
    public interface ICustomerReadRepository
    {
        /// <summary>
        /// Список всех <see cref="Customer"/>
        /// </summary>
        Task<IReadOnlyCollection<Customer>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="Customer"/>
        /// </summary>
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификаторам <see cref="Customer"/>
        /// </summary>
        Task<Dictionary<Guid, Customer>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка на наличие <see cref="Customer"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
