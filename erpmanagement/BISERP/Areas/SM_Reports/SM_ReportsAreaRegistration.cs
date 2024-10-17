using System.Web.Mvc;

namespace BISERP.Areas.SM_Reports
{
    public class SM_ReportsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SM_Reports";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SM_Reports_default",
                "SM_Reports/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
