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
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset MadeAt { get; set; }
        

    }
}
