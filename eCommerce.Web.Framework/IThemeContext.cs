using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Enums;

namespace eCommerce.Web.Framework
{
    /// <summary>
    /// Interface for theme context
    /// </summary>
    public interface IThemeContext
    {
        string GetCurrentTheme(WorkType type);
        bool SetTheme(string themeName, WorkType type = WorkType.Desktop);
    }
}
