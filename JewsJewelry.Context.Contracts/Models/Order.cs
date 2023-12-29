using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Common.Entity.EntityInterface;

namespace JewsJewelry.Context.Contracts.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order : BaseAuditEntity
    {
        /// <summary>
        /// ID изделия
        /// </summary>
        public Guid JewelryId { get; set; }
        public Jewelry Jewelries { get; set; }

        /// <summary>
        /// ID покупателя
        /// </summary>
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        /// <summary>
        /// ID мастерской
        /// </summary>
        public Guid WorkshopId { get; set; }
        public Workshop Workshop { get; set; }

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
