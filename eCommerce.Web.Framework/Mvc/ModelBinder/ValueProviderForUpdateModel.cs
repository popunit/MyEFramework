using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using eCommerce.Core.Common;

namespace eCommerce.Web.Framework.Mvc.ModelBinder
{
    public enum MvcDefaultScopeForUpdateModel
    {
        RequestForm,
        RouteDataValues,
        QueryString,
        RequestFiles,
    }

    public static class BuiltInValueProvider
    {
        public static IValueProvider CreateDefaultValueProvider(
            this Controller controller, 
            MvcDefaultScopeForUpdateModel scope = MvcDefaultScopeForUpdateModel.RequestForm)
        {
            switch (scope)
            {
                case MvcDefaultScopeForUpdateModel.RequestForm:
                    return new FormValueProvider(controller.ControllerContext);
                case MvcDefaultScopeForUpdateModel.RouteDataValues:
                    return new RouteDataValueProvider(controller.ControllerContext);
                case MvcDefaultScopeForUpdateModel.QueryString:
                    return new QueryStringValueProvider(controller.ControllerContext);
                case MvcDefaultScopeForUpdateModel.RequestFiles:
                    return new HttpFileCollectionValueProvider(controller.ControllerContext);
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// DateTime value provider for update model
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class DateTimeValueProvider<TModel> : IValueProvider
    {
        private TModel model;
        private PropertyInfo propertyInfo;
        private string cultureString;
        public DateTimeValueProvider(TModel model, string cultureString)
        {
            this.model = model;
            this.cultureString = cultureString;
        }

        public bool ContainsPrefix(string prefix)
        {
            var prop = typeof(TModel).GetProperty(prefix,
                                            BindingFlags.Public
                                            | BindingFlags.Instance
                                            | BindingFlags.IgnoreCase);        
            if (null != prop)
            {
                if (typeof(DateTime).IsAssignableFrom(prop.PropertyType)) // get datatime type properties
                {
                    propertyInfo = prop;
                    return true;
                }
            }

            return false;
        }

        public ValueProviderResult GetValue(string key)
        {
            if (ContainsPrefix(key))
            {
                var dt = (DateTime)propertyInfo.GetValue(model);
                return new ValueProviderResult(dt, null, new CultureInfo(cultureString));
            }

            return null;
        }
    }
}
