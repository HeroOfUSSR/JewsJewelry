﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Common.Entity.EntityInterface;

namespace JewsJewelry.Context.Contracts.Models
{
    /// <summary>
    /// Заказчик
    /// </summary>
    public class Customer : BaseAuditEntity
    {
        /// <summary>
        /// Имя заказчика
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия заказчика
        /// </summary>
        public string Surname { get; set; } = string.Empty;
        
        /// <summary>
        /// Отчество заказчика
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;
        
        /// <summary>
        /// Телефон заказчика
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// Электронная почта заказчика
        /// </summary>
        public string Email { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; }


    }
}
