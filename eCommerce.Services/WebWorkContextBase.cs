using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core;
using eCommerce.Services.WcfClient.Entities;

namespace eCommerce.Services
{
    public abstract class WebWorkContextBase : WorkContextBase
    {
        public virtual User CurrentUser 
        {
            get
            {
                return GetData<User>();
            }
            set
            {
                SetData<User>(value);
            }
        }
    }
}
