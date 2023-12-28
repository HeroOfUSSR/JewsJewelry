using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Context.Contracts.Models;

namespace JewsJewelry.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий считывания <see cref="Craftsman"/>
    /// </summary>
    public interface ICraftsmanReadRepository
    {
        /// <summary>
        /// Список всех <see cref="Craftsman"/>
        /// </summary>
        Task<IReadOnlyCollection<Craftsman>> GetAllAsync(CancellationToken cancellationToken);
        
        /// <summary>
        /// Список по идентификатору <see cref="Craftsman"/>
        /// </summary>
        Task<Craftsman?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        
        /// <summary>
        /// Список по идентификаторам <see cref="Craftsman"/>
        /// </summary>
        Task<Dictionary<Guid, Craftsman>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
        
        /// <summary>
        /// Проверка на наличие <see cref="Craftsman"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
