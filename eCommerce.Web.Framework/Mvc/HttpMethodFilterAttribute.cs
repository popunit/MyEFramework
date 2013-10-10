using System;

namespace eCommerce.Web.Framework.Mvc
{
    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false, Inherited = false)]
    public class HttpMethodFilterAttribute : Attribute
    {
        public bool DisableGet { get; set; }
        public bool DisablePost { get; set; }
        public bool DisablePut { get; set; }
        public bool DisableDelete { get; set; }

        public HttpMethodFilterAttribute()
        {
            this.DisableGet = false;
            this.DisablePost = false;
            this.DisablePut = false;
            this.DisableDelete = false;
        }
    }
}
