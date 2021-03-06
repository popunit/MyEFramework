﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.Core.Infrastructure.NoAOP
{
    public interface ILogger
    {
        void Log(string message);
        void Log(string[] categories, string message);
        void LogException(System.Exception x);
    }
}
