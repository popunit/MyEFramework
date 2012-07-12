using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Enums;

namespace eCommerce.Web.Framework
{
    public interface IThemeContext
    {
        string GetCurrentTheme(WorkType type);
        bool SetTheme(WorkType type);
    }
}
