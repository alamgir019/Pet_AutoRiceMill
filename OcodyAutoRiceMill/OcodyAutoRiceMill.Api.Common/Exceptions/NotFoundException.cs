﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OcodyAutoRiceMill.Api.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
