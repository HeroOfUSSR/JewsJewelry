using JewsJewelry.Common.Entity.DBInterface;
using JewsJewelry.Common.Entity.EntityInterface;
using JewsJewelry.Repositories.Contracts.WriteReposInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Repositories
{
    /// <summary>
    /// Класс для работы с БД, реализующий <see cref="IRepositoryWriter"/>
    /// </summary>
    public abstract class BasedWriteRepository<T> : IRepositoryWriter<T> where T : class, IEntity
    {
        /// <summary>
        /// <inheritdoc cref="IDBWriterContext"/>
        /// </summary>
        private readonly IDBWriterContext _writerContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BasedWriteRepository{T}"/>
        /// </summary>
        /// <param name="writerContext"></param>
        protected BasedWriteRepository(IDBWriterContext writerContext)
        {
            this._writerContext= writerContext;
        }

        /// <summary>
        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        /// </summary>
        public virtual void Add([NotNull] T entity)
        {
            if (entity is IEntityId entityId && entityId.Id == Guid.Empty)
            {
                entityId.Id= Guid.NewGuid();
            }
            AuditCreate(entity);
            AuditUpdate(entity);
            _writerContext.Writer.Add(entity);
        }

        /// <summary>
        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        /// </summary>
        public void Update([NotNull] T entity)
        {
            AuditUpdate(entity);
            _writerContext.Writer.Update(entity);
        }

        /// <summary>
        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        /// </summary>
        public void Delete([NotNull] T entity)
        {
            AuditUpdate(entity);
            AuditDelete(entity);

            if (entity is IEntityAuditDeleted)
            {
                _writerContext.Writer.Update(entity);
            }
            else
            {
                _writerContext.Writer.Delete(entity);
            }
        }

        private void AuditDelete(T entity)
        {
            if (entity is IEntityAuditDeleted auditDeleted)
            {
                auditDeleted.DeletedAt = _writerContext.TimeProvider.CurrentTime;
                auditDeleted.DeletedBy = _writerContext.User;
            }
        }

        private void AuditUpdate(T entity)
        {
            if (entity is IEntityAuditUpdated auditUpdated)
            {
                auditUpdated.UpdatedAt = _writerContext.TimeProvider.CurrentTime;
                auditUpdated.UpdatedBy = _writerContext.User;
            }
        }

        private void AuditCreate(T entity)
        {
            if (entity is IEntityAuditCreated auditCreated)
            {
                auditCreated.CreatedAt = _writerContext.TimeProvider.CurrentTime;
                auditCreated.CreatedBy = _writerContext.User;
            }
        }
    }
}
