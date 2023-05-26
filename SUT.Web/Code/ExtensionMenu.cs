using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Sut.Web
{
    public static class ExtensionMenu
    {
        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller,
            string actionActive,
            string controllerActive)
        {
            var li = new TagBuilder("li");
            if (string.Equals(actionActive, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(controllerActive, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("active");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, controller).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuItem(
            this HtmlHelper htmlHelper,
            string text,
            string action,
            string controller,
            string actionActive,
            string controllerActive,
            long parameterId)
        {
            var li = new TagBuilder("li");
            if (string.Equals(actionActive, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(controllerActive, controller, StringComparison.OrdinalIgnoreCase)
                )
            {
                li.AddCssClass("active");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, new { controller = controller, id = parameterId }).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }
    }
}