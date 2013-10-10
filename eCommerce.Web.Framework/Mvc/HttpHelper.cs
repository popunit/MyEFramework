using eCommerce.Core;
using eCommerce.Core.Common;
using System;
using System.Web;

namespace eCommerce.Web.Framework.Mvc
{
    public class HttpHelper : IHttpHelper
    {
        private readonly HttpContextBase _httpContext;

        public HttpHelper(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        public string GetUrlReferrer()
        {
            string urlReferrer = string.Empty;
            if (_httpContext.HasRequest())
                urlReferrer = _httpContext.Request.UrlReferrer.ToSafeString();
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
            if (_httpContext.HasRequest() && null != _httpContext.Request.Url)
            {
                if (includeQueryString)
                {
                    url = _httpContext.Request.Url.ToString();
                }
                else
                {
                    url = _httpContext.Request.Url.GetLeftPart(UriPartial.Path);
                }
                url = url.ToLowerInvariant();
            }

            return url;
        }


        public HttpCookie GetCookie(string name)
        {
            if (_httpContext.HasRequest())
                return _httpContext.Request.Cookies[name];
            else
                return null;
        }

        public void SetCookie(string name, string value)
        {
            if (_httpContext.HasRequest())
            {
                var cookie = new HttpCookie(name)
                {
                    HttpOnly = true,
                    Value = value,
                    Expires = string.IsNullOrWhiteSpace(value) ? 
                                DateTime.Now.AddMonths(-1) : 
                                DateTime.Now.AddMonths(3)
                };

                _httpContext.Response.Cookies.Remove(name);
                _httpContext.Response.Cookies.Add(cookie);
            }
        }
    }
}
