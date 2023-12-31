﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Common.Entity.EntityInterface;

namespace JewsJewelry.Context.Contracts.Models
{
    /// <summary>
    /// Мастерская
    /// </summary>
    public class Workshop : BaseAuditEntity
    {
        /// <summary>
        /// Название мастерской
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Адрес мастерской
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Направленность мастерской
        /// </summary>
        public string Speciality { get; set; } = string.Empty;

        /// <summary>
        /// Количество рабочих мест
        /// </summary>
        public int Workplaces { get; set; }

        public ICollection<Craftsman> Craftsman { get; set;}

        public ICollection<Order> Orders { get; set; }

    }
}
