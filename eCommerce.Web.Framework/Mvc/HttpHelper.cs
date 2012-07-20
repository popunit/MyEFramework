using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eCommerce.Core;
using eCommerce.Core.Common;

namespace eCommerce.Web.Framework.Mvc
{
    public class HttpHelper : IHttpHelper
    {
        private readonly HttpContextBase httpContext;

        public HttpHelper(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetUrlReferrer()
        {
            string urlReferrer = string.Empty;
            if (httpContext.HasRequest())
                urlReferrer = httpContext.Request.UrlReferrer.ToSafeString();
            return urlReferrer;
        }

        public string GetCurrentRequestUrl(bool fullUrl)
        {
            throw new NotImplementedException();
        }
    }
}
