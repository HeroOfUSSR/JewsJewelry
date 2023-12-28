using JewsJewelry.Common.Entity.EntityInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Common.Entity.Repositories
{
    /// <summary>
    /// Вспомогательные методы расширения
    /// </summary>
    public static class DefaultExtensions
    {
        /// <summary>
        /// По Айдишнику
        /// </summary>
        public static IQueryable<TEntity> ById<TEntity>(this IQueryable<TEntity> query, Guid id) where TEntity : class, IEntityId
            => query.Where(x => x.Id == id);

        /// <summary>
        /// По Айдишникам
        /// </summary>
        public static IQueryable<TEntity> ByIds<TEntity>(this IQueryable<TEntity> query, IEnumerable<Guid> ids) where TEntity : class, IEntityId
        {
            var temp = ids.Count();
            switch (temp)
            {
                case 0:
                    return query.Where(x => false);
                case 1:
                    return query.ById(ids.First());
                default:
                    return query.Where(x => ids.Contains(x.Id));
            }
        }

        public static IQueryable<TEntity> NotDeletedAt<TEntity>(this IQueryable<TEntity> query) where TEntity : class, IEntityAuditDeleted
            => query.Where(x => x.DeletedAt == null);

        public static Task<IReadOnlyCollection<TEntity>> ToReadOnlyCollectionAsync<TEntity>(this IQueryable<TEntity> query,
            CancellationToken cancellationToken)
            => query.ToListAsync(cancellationToken).ContinueWith(x => new ReadOnlyCollection<TEntity>(x.Result) as IReadOnlyCollection<TEntity>, cancellationToken);
        
    }
}
