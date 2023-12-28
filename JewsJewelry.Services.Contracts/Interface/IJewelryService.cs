using JewsJewelry.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Interface
{
    /// <summary>
    /// Сервис <see cref="JewelryModel"/>
    /// </summary>
    public interface IJewelryService
    {

        /// <summary>
        /// Список всех <see cref="JewelryModel"/>
        /// </summary>
        Task<IEnumerable<JewelryModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="JewelryModel"/>
        /// </summary>
        Task<JewelryModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новое изделие
        /// </summary>
        Task<JewelryModel> AddAsync(JewelryModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует изделие
        /// </summary>
        Task<JewelryModel> EditAsync(JewelryModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет изделие
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
