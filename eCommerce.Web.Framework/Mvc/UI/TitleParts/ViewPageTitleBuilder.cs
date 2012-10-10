using eCommerce.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework.Mvc.UI.TitleParts
{
    public partial class ViewPageTitleBuilder : IViewPageTitleBuilder
    {
        private readonly PageSettings pageSettings;
        private readonly List<string> allPartialTitles;

        public ViewPageTitleBuilder(PageSettings pageSettings)
        {
            this.pageSettings = pageSettings;
            this.allPartialTitles = new List<string>();
        }

        public void AppendPartialTitles(params string[] partialTitles)
        {
            if (null != partialTitles)
            {
                var titles = from pt in partialTitles
                             where !String.IsNullOrEmpty(pt)
                             select pt;
                allPartialTitles.AddRange(titles);
            }
        }

        public void PrependPartialTitles(params string[] partialTitles)
        {
            if (null != partialTitles)
            {
                var titles = from pt in partialTitles
                             where !String.IsNullOrEmpty(pt)
                             select pt;
                allPartialTitles.InsertRange(0, partialTitles);
            }
        }

        public string GenerateTitle(bool hasDefaultTitle)
        {
            if (hasDefaultTitle)
            {
                return pageSettings.DefaultTitle + (allPartialTitles.Count() > 0 ? ": " + string.Join("", allPartialTitles) : "");
            }
            else
            {
                return string.Join("", allPartialTitles);
            }
        }
    }
}
