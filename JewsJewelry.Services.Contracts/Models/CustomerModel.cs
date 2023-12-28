using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Models
{
    public class CustomerModel
    {
        /// <summary>
        /// Идентификатор заказчика
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
