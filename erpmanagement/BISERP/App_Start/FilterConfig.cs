using System.Web;
using System.Web.Mvc;
using BISERP.Filters;

namespace BISERP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
			filters.Add(new LogonAuthorize());
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ProcessExceptionFilterAttribute());
        }
    }
}