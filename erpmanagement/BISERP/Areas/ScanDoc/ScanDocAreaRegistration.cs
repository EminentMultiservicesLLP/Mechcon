using System.Web.Mvc;

namespace BISERP.Areas.ScanDoc
{
    public class ScanDocAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ScanDoc";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ScanDoc_default",
                "ScanDoc/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BISERP.Areas.ScanDoc.Controllers" }
            );
        }
    }
}
