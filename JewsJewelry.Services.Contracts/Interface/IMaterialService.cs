using JewsJewelry.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Interface
{
    /// <summary>
    /// Сервис <see cref="MaterialModel"/>
    /// </summary>
    public interface IMaterialService
    {

        /// <summary>
        /// Список всех <see cref="MaterialModel"/>
        /// </summary>
        Task<IEnumerable<MaterialModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Список по идентификатору <see cref="MaterialModel"/>
        /// </summary>
        Task<MaterialModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый материал
        /// </summary>
        Task<MaterialModel> AddAsync(MaterialModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует материал
        /// </summary>
        Task<MaterialModel> EditAsync(MaterialModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет материал
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
