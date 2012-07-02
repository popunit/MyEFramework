using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework.Mvc
{
    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false, Inherited = false)]
    public class EnabledAttribute : Attribute
    {
        public Type[] ModelTypes
        {
            get;
            protected set;
        }

        public EnabledAttribute(params Type[] forModelTypes)
        {
            this.ModelTypes = forModelTypes;
        }
    }
}
