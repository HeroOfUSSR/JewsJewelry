using JewsJewelry.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Exceptions
{
    public class ValidsException : DefaultException
    {
        public IEnumerable<InvalidateItemModel> Errors { get; }

        public ValidsException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
