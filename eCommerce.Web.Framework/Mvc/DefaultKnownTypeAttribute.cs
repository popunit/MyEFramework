using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface,
        AllowMultiple = false, Inherited = false)]
    public class DefaultKnownTypeAttribute : Attribute
    {
        public Type DefaultSubType
        {
            get;
            protected set;
        }

        public DefaultKnownTypeAttribute(Type defaultSubType)
        {
            this.DefaultSubType = defaultSubType;
        }
    }
}
