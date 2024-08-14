using System.Web.Mvc;

namespace BISERP.Areas.DashBoards
{
    public class DashBoardsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DashBoards";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DashBoards_default",
                "DashBoards/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "BISERP.Areas.DashBoards.Controllers" }
            );
        }
    }
}
