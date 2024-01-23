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
    /// Реализация <see cref="IJewelryReadRepository"/>
    /// </summary>
    public class JewelryReadRepository : IJewelryReadRepository, IRepositoryMarker
    {
        /// <summary>
        /// Контекст для связи с БД
        /// </summary>
        private IDBRead reader;

        public JewelryReadRepository(IDBRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Jewelry>> IJewelryReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Jewelry>()
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Jewelry?> IJewelryReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Jewelry>()
            .ById(id)
            .NotDeletedAt()
            .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Jewelry>> IJewelryReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Jewelry>()
            .ByIds(ids)
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IJewelryReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Jewelry>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
