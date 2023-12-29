using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Common.Entity.EntityInterface;
using JewsJewelry.Context.Contracts;
using JewsJewelry.Context.Contracts.Config.Configs;
using JewsJewelry.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace JewsJewelry.Context
{
    public class JewelryContext : DbContext, IJewelryContext, IDBRead, IDBWriter, IUnitOfWork
    {
        public DbSet<Craftsman> Craftsmen { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Jewelry> Jewelries { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Workshop> Workshops { get; set; }

        public JewelryContext(DbContextOptions<JewelryContext> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CraftsmanEntityTypeConfig).Assembly);
        }


        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }

        /// <summary>
        /// Чтение сущностей из БД
        /// </summary>
        IQueryable<TEntity> IDBRead.Read<TEntity>()
            => base.Set<TEntity>() .AsNoTracking() .AsQueryable();

        /// <summary>
        /// Запись сущности в БД
        /// </summary>
        void IDBWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity) .State = EntityState.Added;

        /// <summary>
        /// Обновление сущности в БД
        /// </summary>
        void IDBWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity) .State = EntityState.Modified;

        /// <summary>
        /// Удаление сущности из БД
        /// </summary>
        void IDBWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity) .State = EntityState.Deleted;


    }
}