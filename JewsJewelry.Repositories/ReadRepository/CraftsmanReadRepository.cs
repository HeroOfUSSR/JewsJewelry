using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Common.Entity.Repositories;
using JewsJewelry.Context.Contracts;
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
    /// Реализация <see cref="ICraftsmanReadRepository"/>
    /// </summary>
    public class CraftsmanReadRepository : ICraftsmanReadRepository, IRepositoryMarker
    {
        /// <summary>
        /// Контекст для связи с БД
        /// </summary>
        private IDBRead reader;

        public CraftsmanReadRepository(IDBRead reader) 
        { 
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Craftsman>> ICraftsmanReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Craftsman>()
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Surname)
            .ThenBy(x => x.Patronymic)
            .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Craftsman?> ICraftsmanReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Craftsman>()
            .ById(id)
            .NotDeletedAt()
            .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Craftsman>> ICraftsmanReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Craftsman>()
            .ByIds(ids)
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Surname)
            .ThenBy(x => x.Patronymic)
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> ICraftsmanReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Craftsman>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
