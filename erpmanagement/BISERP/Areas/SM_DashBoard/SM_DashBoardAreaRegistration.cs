using System.Web.Mvc;

namespace BISERP.Areas.SM_DashBoard
{
    public class SM_DashBoardAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SM_DashBoard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SM_DashBoard_default",
                "SM_DashBoard/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
