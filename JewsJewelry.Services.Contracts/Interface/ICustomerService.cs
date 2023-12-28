using JewsJewelry.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Services.Contracts.Models;

namespace JewsJewelry.Services.Contracts.Interface
{
    /// <summary>
    /// Сервис <see cref="CustomerModel"/>
    /// </summary>
    public interface ICustomerService
    {

        /// <summary>
        /// Список всех <see cref="CustomerModel"/>
        /// </summary>
        Task<IEnumerable<CustomerModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="CustomerModel"/>
        /// </summary>
        Task<CustomerModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового заказчика
        /// </summary>
        Task<CustomerModel> AddAsync(CustomerModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует заказчика
        /// </summary>
        Task<CustomerModel> EditAsync(CustomerModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет заказчика
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
