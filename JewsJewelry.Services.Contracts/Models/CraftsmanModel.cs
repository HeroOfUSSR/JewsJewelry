using JewsJewelry.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Models
{
    public class CraftsmanModel
    {
        /// <summary>
        /// Идентификатор мастера
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор мастерской
        /// </summary>
        public WorkshopModel Workshop { get; set; }

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
    }
}
