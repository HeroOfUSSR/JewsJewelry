using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context.Contracts.Models
{
    /// <summary>
    /// Ювелирное изделие
    /// </summary>
    internal class Jewelries : BaseAuditEntity
    {
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
        
        public Guid MaterialId { get; set; }
        public Materials Material { get; set; }

        public ICollection<Orders> Orders { get; set; }

    }
}
