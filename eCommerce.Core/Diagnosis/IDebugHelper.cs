﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Diagnosis
{
    public interface IDebugHelper
    {
        bool DebugEnabled { get; }
    }
}
