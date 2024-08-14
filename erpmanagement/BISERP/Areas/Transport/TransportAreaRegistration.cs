using System.Web.Mvc;

namespace BISERP.Areas.Transport
{
    public class TransportAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Transport";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Transport_default",
                "Transport/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BISERP.Areas.Transport.Controllers" }
            );
        }
    }
}
