using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc
{
    /// <summary>
    /// ModelBinder cannot bind interface or abstract class directly, should specify one concrete instance
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/9417888/mvc-3-model-binding-a-sub-type-abstract-class-or-interface
    /// </remarks>
    [DefaultKnownType(typeof(DefaultWebModel))]
    public abstract class WebModelBase
    {
        /// <summary>
        /// Happens before binding model in ModelBinder
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        public virtual void BindingModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        { 
        }
    }

    public class DefaultWebModel : WebModelBase
    { 
        // do nothing
    }
}
