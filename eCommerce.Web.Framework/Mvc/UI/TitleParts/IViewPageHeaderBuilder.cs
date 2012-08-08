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
        void AppendPartialTitles(params string[] partialTitle);
        void PrependPartialTitles(params string[] partialTitle);
        string GenerateTitle(bool hasDefaultTitle);
        #endregion
    }
}
