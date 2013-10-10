using eCommerce.Core.Common;
using System;
using System.Linq;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.ModelBinder
{
    [Enabled(typeof(WebModelBase))]
    public class WebModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            var modelBase = model as WebModelBase;
            if (modelBase != null) 
                modelBase.BindingModel(controllerContext, bindingContext);
            return model;
        }

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            if (modelType == typeof(WebModelBase))
            {
                var attribute = typeof(WebModelBase).GetCustomAttributes(false).Where(t => t.GetType() == typeof(DefaultKnownTypeAttribute)).FirstOrDefault() as DefaultKnownTypeAttribute;
                Type instantiationType;
                if (null != attribute)
                    instantiationType = attribute.DefaultSubType;
                else
                    throw new Exception(string.Format("No set default sub type for base type {0}!", typeof(WebModelBase).FullName));
                //var obj = Activator.CreateInstance(instantiationType);
                var obj = EmitHelper.FastGetInstance(instantiationType)();
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, instantiationType);
                bindingContext.ModelMetadata.Model = obj;
                return obj;
            }
            return base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Be careful to use</remarks>
    public class WebDIModelBinder : WebModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return System.Web.Mvc.DependencyResolver.Current.GetService(modelType)??
                base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}
