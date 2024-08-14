using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BISERP
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(MvcApplication));
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            BundleTable.EnableOptimizations = false;
        }
        protected void Application_Error(object sender, EventArgs e)
        {          
            var exception = Server.GetLastError() as HttpException;
            if (exception != null && exception.GetHttpCode() == 404)
            {
                //Redirect to a specific view
                Response.Clear();
                Server.ClearError();
                Response.Redirect("~/Error/ViewNotFound");
            }
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
        //    RouteData rd = RouteTable.Routes.GetRouteData(context);

        //    if (rd != null)
        //    {
        //        string controllerName = rd.GetRequiredString("controller");
        //        string actionName = rd.GetRequiredString("action");
        //        var message = string.Format("controller :{0} action :{1}", controllerName, actionName);
        //        Logger.LogInfo(message);
        //    }

        //}
        //protected void Application_Endrequest()
        //{
        //    Logger.LogInfo("Request Application_Endrequest");
        //}

    }
}