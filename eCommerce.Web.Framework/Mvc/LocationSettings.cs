
namespace eCommerce.Web.Framework.Mvc
{
    public static class LocationSettings
    {
        //public enum Parameter
        //{
        //    ViewName = 0,
        //    ControllerName = 1,
        //    AreaName = 2,
        //    Theme = 3
        //}

        //public static int ViewNameIndex { get { return Parameter.ViewName.Index<int>(); } }
        //public static int ControllerNameIndex { get { return Parameter.ControllerName.Index<int>(); } }
        //public static int AreaNameIndex { get { return Parameter.AreaName.Index<int>(); } }
        //public static int ThemeIndex { get { return Parameter.Theme.Index<int>(); } }

        public static readonly string[] AdminLocationFormat = 
        { 
            "~/Administration/Views/{1}/{0}.cshtml", 
            "~/Administration/Views/{1}/{0}.vbhtml",
            "~/Administration/Views/Shared/{0}.cshtml",
            "~/Administration/Views/Shared/{0}.vbhtml"
        };
    }
}
