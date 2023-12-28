using JewsJewelry.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Interface
{
    /// <summary>
    /// Сервис <see cref="WorkshopModel"/>
    /// </summary>
    public interface IWorkshopService
    {

        /// <summary>
        /// Список всех <see cref="WorkshopModel"/>
        /// </summary>
        Task<IEnumerable<WorkshopModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="WorkshopModel"/>
        /// </summary>
        Task<WorkshopModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый заказ
        /// </summary>
        Task<WorkshopModel> AddAsync(WorkshopModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует заказ
        /// </summary>
        Task<WorkshopModel> EditAsync(WorkshopModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет заказ
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
