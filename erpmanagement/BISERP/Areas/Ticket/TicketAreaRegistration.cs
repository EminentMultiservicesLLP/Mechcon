using System.Web.Mvc;

namespace BISERP.Areas.Ticket
{
    public class TicketAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Ticket";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Ticket_default",
                "Ticket/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
