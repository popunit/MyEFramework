using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Services.WcfClient.Entities;

namespace eCommerce.Services
{
    public abstract class WorkContextServiceBase : WorkContextBase
    {
        public virtual User CurrentUser { get; set; }
    }
}
