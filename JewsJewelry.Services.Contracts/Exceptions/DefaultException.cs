using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Exceptions
{
    public abstract class DefaultException : Exception
    {
        protected DefaultException() { }

        protected DefaultException(string message) : base(message) { }
    }
}
