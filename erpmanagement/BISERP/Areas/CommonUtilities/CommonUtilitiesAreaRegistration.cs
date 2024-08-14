using System.Web.Mvc;

namespace BISERP.Areas.CommonUtilities
{
    public class CommonUtilitiesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CommonUtilities";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CommonUtilities_default",
                "CommonUtilities/{controller}/{action}/{id}",
                namespaces: new[] { "BISERP.Areas.CommonUtilities.Controllers" }
            );
        }
    }
}
