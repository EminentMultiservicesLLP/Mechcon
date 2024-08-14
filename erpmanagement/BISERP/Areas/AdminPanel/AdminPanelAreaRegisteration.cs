using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Billing
{
    public class AdminPanelAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminPanel_default",
                "AdminPanel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BISERP.Areas.AdminPanel.Controllers" }
            );
        }
    }
}