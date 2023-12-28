using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Models
{
     public class MaterialModel
    {
        /// <summary>
        /// Идентификатор материала
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
