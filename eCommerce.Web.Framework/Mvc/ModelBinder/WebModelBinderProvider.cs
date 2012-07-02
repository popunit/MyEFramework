using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eCommerce.Core.Common;

namespace eCommerce.Web.Framework.Mvc.ModelBinder
{
    public class WebModelBinderProvider : IModelBinderProvider
    {
        // null: for all types, count == 0: for none of types, count > 0: for specific types
        private List<Type> supportedModelTypes;

        public IModelBinder GetBinder(Type modelType)
        {
            Type t = typeof(WebModelBinderProvider);
            IModelBinder result = null;
            Type modelBinderType = null;

            if (null == supportedModelTypes)
            {
                var currentTypes = Assembly.GetAssembly(t)
                    .GetTypes()
                    .Where(type => !String.IsNullOrEmpty(type.Namespace) &&
                        type.IsInherit(typeof(IModelBinder)));
                foreach (var type in currentTypes)
                {
                    var attrList = type.GetCustomAttributes<EnabledAttribute>(false);
                    if (null != attrList && attrList.Count() != 0)
                    {
                        modelBinderType = type;
                        var modelTypes = attrList.First().ModelTypes;
                        if (null != modelTypes)
                        {
                            supportedModelTypes = new List<Type>();
                            supportedModelTypes.AddRange(modelTypes);
                            break;
                        }
                    }
                }
            }

            if (null != supportedModelTypes && supportedModelTypes.Contains(modelType))
                result = (IModelBinder)Activator.CreateInstance(modelBinderType);

            return result;
        }
    }
}
