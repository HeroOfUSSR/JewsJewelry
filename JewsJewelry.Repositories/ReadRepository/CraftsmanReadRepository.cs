using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Context.Contracts;
using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Repositories.Contracts.Interface;
using JewsJewelry.Repositories.Marker;
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
        /// Для связи с БД
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
            .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Craftsman?> ICraftsmanReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Craftsman>()
    }
}
