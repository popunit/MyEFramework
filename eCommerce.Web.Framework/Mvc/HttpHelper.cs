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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeQueryString"></param>
        /// <returns></returns>
        /// <remarks>TO-DO: to support SSL</remarks>
        public string GetCurrentRequestUrl(bool includeQueryString)
        {
            string url = string.Empty;
            if (httpContext.HasRequest())
            {
                if (includeQueryString)
                {
                    url = httpContext.Request.Url.ToString();
                }
                else
                {
                    url = httpContext.Request.Url.GetLeftPart(UriPartial.Path);
                }
                url.ToLowerInvariant();
            }

            return url;
        }


        public HttpCookie GetCookie(string name)
        {
            if (httpContext.HasRequest())
                return httpContext.Request.Cookies[name];
            else
                return null;
        }

        public void SetCookie(string name, string value)
        {
            if (httpContext.HasRequest())
            {
                var cookie = new HttpCookie(name)
                {
                    HttpOnly = true,
                    Value = value,
                    Expires = string.IsNullOrWhiteSpace(value) ? 
                                DateTime.Now.AddMonths(-1) : 
                                DateTime.Now.AddMonths(3)
                };

                httpContext.Response.Cookies.Remove(name);
                httpContext.Response.Cookies.Add(cookie);
            }
        }
    }
}
