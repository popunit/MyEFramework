using System;

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
