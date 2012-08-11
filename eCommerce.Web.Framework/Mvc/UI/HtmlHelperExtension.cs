using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eCommerce.Web.Framework.Mvc.UI.TitleParts;

namespace eCommerce.Web.Framework.Mvc.UI
{
    public static class HtmlHelperExtension
    {
        public static void AppendPartialTitles(this HtmlHelper html, params string[] parts)
        {
            var headerBuilder = DependencyResolver.Current.GetService<IViewPageHeaderBuilder>();
            headerBuilder.AppendPartialTitles(parts);
        }

        public static void PrependPartialTitles(this HtmlHelper html, params string[] parts)
        {
            var headerBuilder = DependencyResolver.Current.GetService<IViewPageHeaderBuilder>();
            headerBuilder.PrependPartialTitles(parts);
        }

        public static MvcHtmlString Title(this HtmlHelper html, bool hasDefaultTitle = true)
        {
            var headerBuilder = DependencyResolver.Current.GetService<IViewPageHeaderBuilder>();
            return MvcHtmlString.Create(html.Encode(headerBuilder.GenerateTitle(hasDefaultTitle)));
        }
    }
}
