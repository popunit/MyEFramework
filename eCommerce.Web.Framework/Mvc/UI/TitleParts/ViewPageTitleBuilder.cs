using eCommerce.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Web.Framework.Mvc.UI.TitleParts
{
    public partial class ViewPageTitleBuilder : IViewPageTitleBuilder
    {
        private readonly PageSettings _pageSettings;
        private readonly List<string> _allPartialTitles;

        public ViewPageTitleBuilder(PageSettings pageSettings)
        {
            this._pageSettings = pageSettings;
            this._allPartialTitles = new List<string>();
        }

        public void AppendPartialTitles(params string[] partialTitles)
        {
            if (null != partialTitles)
            {
                var titles = from pt in partialTitles
                             where !String.IsNullOrEmpty(pt)
                             select pt;
                _allPartialTitles.AddRange(titles);
            }
        }

        public void PrependPartialTitles(params string[] partialTitles)
        {
            if (null != partialTitles)
            {
                var titles = from pt in partialTitles
                             where !String.IsNullOrEmpty(pt)
                             select pt;
                _allPartialTitles.InsertRange(0, partialTitles);
            }
        }

        public string GenerateTitle(bool hasDefaultTitle)
        {
            if (hasDefaultTitle)
            {
                return _pageSettings.DefaultTitle + (_allPartialTitles.Any() ? ": " + string.Join("", _allPartialTitles) : "");
            }
            else
            {
                return string.Join("", _allPartialTitles);
            }
        }
    }
}
