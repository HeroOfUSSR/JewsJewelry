using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewsJewelry.Common.Entity.EntityInterface;

namespace JewsJewelry.Context.Contracts.Models
{
    /// <summary>
    /// Ювелирное изделие
    /// </summary>
    public class Jewelry : BaseAuditEntity
    {
        /// <summary>
        /// ID материала
        /// </summary>
        public Guid MaterialId { get; set; }
        public Material Material { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Стоимость изделия
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Масса изделия
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Описание изделия
        /// </summary>
        public string? Description { get; set; }
        

        public ICollection<Order> Orders { get; set; }

    }
}
