using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Models
{
    public class JewelryModel
    {
        /// <summary>
        /// Идентификатор изделия
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор мастерской
        /// </summary>
        public MaterialModel Material { get; set; }

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
    }
}
