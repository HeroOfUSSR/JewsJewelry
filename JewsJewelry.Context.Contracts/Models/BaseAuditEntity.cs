﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Common.Entity.EntityInterface;


namespace JewsJewelry.Context.Contracts.Models
{
    /// <summary>
    /// Базовый класс с аудитом
    /// </summary>
    public abstract class BaseAuditEntity : IEntity, IEntityId, 
        IEntityAuditCreated, 
        IEntityAuditUpdated, 
        IEntityAuditDeleted
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Когда создан
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Кем создан
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Когда изменён
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set;}

        /// <summary>
        /// Кем изменён
        /// </summary>
        public string UpdatedBy { get; set;} = string.Empty;

        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }
       
    }
}
