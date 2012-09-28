﻿using eCommerce.Core.Data;
using eCommerce.Wcf.Services.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Wcf.Services.Common
{
    public class DataInfoService : IDataInfoService
    {
        public bool DatabaseIsInstalled()
        {
            return DatabaseSettingHelper.FindDatabaseSettings;
        }
    }
}
