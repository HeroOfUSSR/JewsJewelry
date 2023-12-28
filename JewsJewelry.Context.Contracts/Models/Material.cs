using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context.Contracts.Models
{
    /// <summary>
    /// Материал
    /// </summary>
    public class Material : BaseAuditEntity
    {
        /// <summary>
        /// Название материала
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Цвет материала
        /// </summary>
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// Проба металла
        /// </summary>
        public int Sample { get; set; }

        /// <summary>
        /// Количество материала (В граммах)
        /// </summary>
        public int Amount { get; set; }

        public ICollection<Jewelry> Jewelries { get; set; }





    }
}
