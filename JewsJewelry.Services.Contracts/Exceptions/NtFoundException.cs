﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Exceptions
{
    public class NtFoundException : DefaultException
    {
        public NtFoundException(string message)
            : base(message)
        { }
    }
}
