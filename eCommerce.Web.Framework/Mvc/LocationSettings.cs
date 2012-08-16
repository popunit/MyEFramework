﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;

namespace eCommerce.Web.Framework.Mvc
{
    public static class LocationSettings
    {
        public enum Parameter
        {
            ViewName = 0,
            ControllerName = 1,
            AreaName = 2,
            Theme = 3
        }

        public static int ViewNameIndex { get { return Parameter.ViewName.Index<int>(); } }
        public static int ControllerNameIndex { get { return Parameter.ControllerName.Index<int>(); } }
        public static int AreaNameIndex { get { return Parameter.AreaName.Index<int>(); } }
        public static int ThemeIndex { get { return Parameter.Theme.Index<int>(); } }

        public static readonly string[] AdminLocationFormat = 
        { 
            "~/Administration/Views/{"+ControllerNameIndex+"}/{"+ViewNameIndex+"}.cshtml", 
            "~/Administration/Views/{"+ControllerNameIndex+"}/{"+ViewNameIndex+"}.vbhtml",
            "~/Administration/Views/Shared/{"+ViewNameIndex+"}.cshtml",
            "~/Administration/Views/Shared/{"+ViewNameIndex+"}.vbhtml"
        };
    }
}
