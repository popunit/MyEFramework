using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Web.Framework.Mvc
{
    /// <summary>
    /// In MVC, there is an attribute named "ChildActionOnly", that means we only
    /// call the action by inline html extension functions (@Html.Action or @Html.RenderAction),
    /// the attribute can help us avoid calling the action by urls. The action will render or return string
    /// as part of current view, not entire page. But if there is a situation we only want to render as entire
    /// page and don't want to render as partial, that means we must call the action via urls, not html extensions.
    /// The attribute is designed for this purpose
    /// </summary>
    /// <remarks>Check IsChildAction value from controllerContext to confirm if current action is child</remarks>
    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false, Inherited = false)]
    public class NotAllowChildActionAttribute : Attribute
    {
    }
}
