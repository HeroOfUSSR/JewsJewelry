using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context.Contracts.Models
{
    internal class Orders
    {
        /// <summary>
        /// Наименование заказа
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание заказа
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Дата закрытия заказа
        /// </summary>
        DateTimeOffset DoneDate { get; set; }

        public Guid JewelryId { get; set; }
        public Jewelries Jewelries { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public Guid WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        


    }
}
