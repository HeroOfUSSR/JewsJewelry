using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context.Contracts.Models
{
    /// <summary>
    /// Предмет
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

        public Guid WorkshopId { get; set; }
        public Workshop Workshop { get; set;}


    }
}
