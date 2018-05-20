using Microsoft.AspNetCore.Mvc;
using Plain.Library.ConvertType;

namespace System.Web.Mvc
{
    public static class ControllerExtension
    {
        public static string GetParamFromRoute(this Controller controller, string value, string defaultValue)
        {
            if (controller.RouteData.Values[value] != null)
                return ConvertHelper.To<String>(controller.RouteData.Values[value]);
            return defaultValue;
        }

        public static string GetParamFromRequest(this Controller controller, string value, string defaultValue)
        {
            if (controller.Request.HttpContext.Items[value] != null)
                return ConvertHelper.To<String>(controller.Request.HttpContext.Items[value]);
            return defaultValue;
        }
    }
}
