using System.Web.Mvc;

namespace BISERP.Areas.TimingPlan
{
    public class TimingPlanAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TimingPlan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TimingPlan_default",
                "TimingPlan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
