using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework.Mvc.UI.TitleParts
{
    /// <summary>
    /// Builder for header part in view page
    /// </summary>
    public interface IViewPageHeaderBuilder
    {
        #region Title part
        void AppendPartialTitles(params string[] partialTitles);
        void PrependPartialTitles(params string[] partialTitles);
        string GenerateTitle(bool hasDefaultTitle);
        #endregion
    }
}
