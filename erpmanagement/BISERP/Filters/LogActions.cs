using BISERPCommon;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BISERP.Filters
{
    public class CustomeExceptionHandlerFilter : FilterAttribute, IExceptionFilter
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(CustomeExceptionHandlerFilter));
        public void OnException(ExceptionContext filterContext)
        {
            Logger.LogError("Exception: "+ filterContext.Exception.Message + Environment.NewLine + filterContext.Exception.StackTrace);
        }
    }

    public class LogActionFilter : ActionFilterAttribute
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(LogActionFilter));
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }
        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = string.Format("{0} controller :{1} action :{2}", methodName, controllerName, actionName);
            Logger.LogInfo(message);
        }
    }
}