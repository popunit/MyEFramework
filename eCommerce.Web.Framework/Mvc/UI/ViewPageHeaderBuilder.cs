
namespace eCommerce.Web.Framework.Mvc.UI
{
    public class ViewPageHeaderBuilder : IViewPageHeaderBuilder
    {
        private readonly IViewPageTitleBuilder _titleBuilder;
        private readonly IViewPageCssBuilder _cssBuilder;

        public ViewPageHeaderBuilder(
            //PageSettings pageSettings,
            IViewPageTitleBuilder titleBuilder,
            IViewPageCssBuilder cssBuilder)
        {
            this._titleBuilder = titleBuilder;
            this._cssBuilder = cssBuilder;
        }

        public IViewPageTitleBuilder Title
        {
            get { return _titleBuilder; }
        }

        public IViewPageCssBuilder Css
        {
            get { return _cssBuilder; }
        }
    }
}
