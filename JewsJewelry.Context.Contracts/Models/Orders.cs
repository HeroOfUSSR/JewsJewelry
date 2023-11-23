using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Context.Contracts.Models
{
    internal class Orders
    {
        public string IdOrder { get; set; } 
        public DateTimeOffset WhenOrdered { get; set; }

        public string WhoOrdered { get; set; }


    }
}
