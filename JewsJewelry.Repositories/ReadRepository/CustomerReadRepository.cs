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
    /// Реализация <see cref="ICustomerReadRepository"/>
    /// </summary>
    public class CustomerReadRepository : ICustomerReadRepository, IRepositoryMarker
    {
        /// <summary>
        /// Контекст для связи с БД
        /// </summary>
        private IDBRead reader;

        public CustomerReadRepository(IDBRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Customer>> ICustomerReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Customer>()
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Surname)
            .ThenBy(x => x.Patronymic)
            .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Customer?> ICustomerReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Customer>()
            .ById(id)
            .NotDeletedAt()
            .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Customer>> ICustomerReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Customer>()
            .ByIds(ids)
            .NotDeletedAt()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Surname)
            .ThenBy(x => x.Patronymic)
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> ICustomerReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Customer>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
