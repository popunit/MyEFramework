using System;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc;

namespace eCommerce.Web.Framework.Mvc.ModelBinder
{
    public enum MvcDefaultScopeForUpdateModel
    {
        RequestForm,
        RouteDataValues,
        QueryString,
        RequestFiles,
    }

    public static class BuiltInValueProviderForUpdateModel
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
    /// <remarks>Cannot be registered for entire application</remarks>
    public class DateTimeValueProvider<TModel> : IValueProvider
    {
        private readonly TModel _model;
        private PropertyInfo _propertyInfo;
        private readonly string _cultureString;
        public DateTimeValueProvider(TModel model, string cultureString)
        {
            this._model = model;
            this._cultureString = cultureString;
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
                    _propertyInfo = prop;
                    return true;
                }
            }

            return false;
        }

        public ValueProviderResult GetValue(string key)
        {
            if (ContainsPrefix(key))
            {
                var dt = (DateTime)_propertyInfo.GetValue(_model);
                return new ValueProviderResult(dt, null, new CultureInfo(_cultureString));
            }

            return null;
        }
    }
}
