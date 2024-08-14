using System.Web.Mvc;

namespace BISERP.Areas.DailyData
{
    public class DailyDataAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DailyData";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DailyData_default",
                "DailyData/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BISERP.Areas.DailyData.Controllers" }
            );
        }
    }
}
