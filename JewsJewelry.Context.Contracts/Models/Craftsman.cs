using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Common.Entity.EntityInterface;

namespace JewsJewelry.Context.Contracts.Models
{
    /// <summary>
    /// Мастер
    /// </summary>
    public class Craftsman : BaseAuditEntity
    {
        /// <summary>
        /// Имя мастера
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия мастера
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Отчество мастера
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Возраст мастера
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// ID мастерской
        /// </summary>
        public Guid WorkshopId { get; set; }
        public Workshop Workshops { get; set; }




    }
}
