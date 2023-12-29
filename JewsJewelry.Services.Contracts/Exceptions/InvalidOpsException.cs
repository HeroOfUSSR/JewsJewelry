using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Exceptions
{
    public class InvalidOpsException : DefaultException
    {

        public InvalidOpsException(string message)
            : base(message)
        { }
    }
}
