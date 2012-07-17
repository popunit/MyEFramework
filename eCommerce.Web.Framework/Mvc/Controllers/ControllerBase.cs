using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            //return base.CreateActionInvoker();
            // use AsyncControllerActionInvoker?
            return new WebControllerActionInvoker();
        }
    }
}
