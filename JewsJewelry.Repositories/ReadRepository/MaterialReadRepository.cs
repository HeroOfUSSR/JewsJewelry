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
    /// Реализация <see cref="IMaterialReadRepository"/>
    /// </summary>
    public class MaterialReadRepository : IMaterialReadRepository, IRepositoryMarker
    {
        /// <summary>
        /// Контекст для связи с БД
        /// </summary>
        private IDBRead reader;

        public MaterialReadRepository(IDBRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Material>> IMaterialReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Material>()
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Material?> IMaterialReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Material>()
            .ById(id)
            .NotDeletedAt()
            .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Material>> IMaterialReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Material>()
            .ByIds(ids)
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IMaterialReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Customer>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
