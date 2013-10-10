using eCommerce.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.ModelBinder
{
    public class WebModelBinderProvider : IModelBinderProvider
    {
        // null: for all types, count == 0: for none of types, count > 0: for specific types
        private List<Type> _supportedModelTypes;

        /// <summary>
        /// According to the model type, select proper model binder type
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public IModelBinder GetBinder(Type modelType)
        {
            Type t = typeof(WebModelBinderProvider);
            IModelBinder result = null;
            Type modelBinderType = null;

            if (null == _supportedModelTypes)
            {
                var currentTypes = Assembly.GetAssembly(t)
                    .GetTypes()
                    .Where(type => !String.IsNullOrEmpty(type.Namespace) &&
                        type.IsInheritFrom(typeof(IModelBinder)));
                foreach (var type in currentTypes)
                {
                    var attrList = type.GetCustomAttributes<EnabledAttribute>(false);
                    if (null != attrList && attrList.Count() != 0)
                    {
                        modelBinderType = type;
                        var modelTypes = attrList.First().ModelTypes;
                        if (null != modelTypes)
                        {
                            _supportedModelTypes = new List<Type>();
                            _supportedModelTypes.AddRange(modelTypes);
                            break;
                        }
                    }
                }
            }

            if (null != _supportedModelTypes && _supportedModelTypes.Contains(modelType))
                //result = (IModelBinder)Activator.CreateInstance(modelBinderType);
                result = (IModelBinder)EmitHelper.FastGetInstance(modelBinderType)();

            return result;
        }
    }
}
