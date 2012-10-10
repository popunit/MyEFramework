using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework
{
    /// <summary>
    /// Builder for header part in view page
    /// </summary>
    public interface IViewPageHeaderBuilder
    {
        IViewPageTitleBuilder Title { get; }
        IViewPageCssBuilder Css { get; }
    }

    public interface IViewPageTitleBuilder
    {
        void AppendPartialTitles(params string[] partialTitles);
        void PrependPartialTitles(params string[] partialTitles);
        string GenerateTitle(bool hasDefaultTitle);
    }

    public interface IViewPageCssBuilder
    {
        void AppendPartialCss(params string[] partialCss);
        void PrependPartialCss(params string[] partialCss);
        string GenerateCss();
    }
}
