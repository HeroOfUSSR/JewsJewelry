using JewsJewelry.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.ModelsRequest
{
    public class OrderRequestModel
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID изделия
        /// </summary>
        public Guid JewelryId { get; set; }

        /// <summary>
        /// ID покупателя
        /// </summary>

        public Guid CustomerId { get; set; }

        /// <summary>
        /// ID мастерской
        /// </summary>
        public Guid WorkshopId { get; set; }

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
        public DateTimeOffset OrderDate { get; set; }

        /// <summary>
        /// Дата закрытия заказа
        /// </summary>
        public DateTimeOffset DoneDate { get; set; }

        
    }
}
