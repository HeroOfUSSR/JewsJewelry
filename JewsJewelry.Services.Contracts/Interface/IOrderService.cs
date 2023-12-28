using JewsJewelry.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Interface
{
    /// <summary>
    /// Сервис <see cref="OrderModel"/>
    /// </summary>
    public interface IOrderService
    {

        /// <summary>
        /// Список всех <see cref="OrderModel"/>
        /// </summary>
        Task<IEnumerable<OrderModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="OrderModel"/>
        /// </summary>
        Task<OrderModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый заказ
        /// </summary>
        Task<OrderModel> AddAsync(OrderModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует заказ
        /// </summary>
        Task<OrderModel> EditAsync(OrderModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет заказ
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }

}
