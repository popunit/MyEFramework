using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;
using eCommerce.Core.Configuration;

namespace eCommerce.Web.Framework.Mvc.UI
{
    public class ViewPageHeaderBuilder : IViewPageHeaderBuilder
    {
        private IViewPageTitleBuilder titleBuilder;
        private IViewPageCssBuilder cssBuilder;

        public ViewPageHeaderBuilder(
            //PageSettings pageSettings,
            IViewPageTitleBuilder titleBuilder,
            IViewPageCssBuilder cssBuilder)
        {
            this.titleBuilder = titleBuilder;
            this.cssBuilder = cssBuilder;
        }

        public IViewPageTitleBuilder Title
        {
            get { return titleBuilder; }
        }

        public IViewPageCssBuilder Css
        {
            get { return cssBuilder; }
        }
    }
}
