using eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework.Mvc.View
{
    /// <summary>
    /// Represent current theme in process
    /// </summary>
    /// <remarks>Consider moving it to work context</remarks>
    public class ThemeContext : IThemeContext
    {
        private readonly WorkContextServiceBase workContext;

        public ThemeContext(
            WorkContextServiceBase workContext
            )
        {
            this.workContext = workContext;
        }

        public string GetCurrentTheme(Core.Enums.WorkType type)
        {
            throw new NotImplementedException();
        }

        public bool SetTheme(Core.Enums.WorkType type)
        {
            throw new NotImplementedException();
        }
    }
}
