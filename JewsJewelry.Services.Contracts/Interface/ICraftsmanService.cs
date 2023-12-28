using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Services.Contracts.Models;

namespace JewsJewelry.Services.Contracts.Interface
{
    /// <summary>
    /// Сервис <see cref="CraftsmanModel">
    /// </summary>
    public interface ICraftsmanService
    {
        /// <summary>
        /// Список всех <see cref="CraftsmanModel"/>
        /// </summary>
        Task<IEnumerable<CraftsmanModel>> GetAllAsync(CancellationToken cancellationToken);
        
        /// <summary>
        /// Список по идентификатору <see cref="CraftsmanModel"/>
        /// </summary>
        Task<CraftsmanModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового мастера
        /// </summary>
        Task<CraftsmanModel> AddAsync(CraftsmanModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует мастера
        /// </summary>
        Task<CraftsmanModel> EditAsync(CraftsmanModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Убивает мастера :c
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
