using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Common.Entity.Repositories;
using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Repositories.Contracts.Interface;
using JewsJewelry.Repositories.Marker;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories.Implementations
{
    /// <summary>
    /// Реализация <see cref="IWorkshopReadRepository"/>
    /// </summary>
    public class WorkshopReadRepository : IWorkshopReadRepository, IRepositoryMarker
    {
        /// <summary>
        /// Контекст для связи с БД
        /// </summary>
        private IDBRead reader;

        public WorkshopReadRepository(IDBRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Workshop>> IWorkshopReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Workshop>()
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Workshop?> IWorkshopReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Workshop>()
            .ById(id)
            .NotDeletedAt()
            .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Workshop>> IWorkshopReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Workshop>()
            .ByIds(ids)
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IWorkshopReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Customer>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
