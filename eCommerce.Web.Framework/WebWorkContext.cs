using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Data;
using eCommerce.Services;

namespace eCommerce.Web.Framework
{
    public class WebWorkContext : WorkContextServiceBase
    {
        protected override T GetData<T>(string name)
        {
            throw new NotImplementedException();
        }

        protected override bool SetData<T>(string name, T value)
        {
            throw new NotImplementedException();
        }
    }
}
