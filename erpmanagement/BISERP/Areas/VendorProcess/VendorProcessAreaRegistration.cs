using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.VendorProcess
{
    public class VendorProcessAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "VendorProcess";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "VendorProcess_default",
                "VendorProcess/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BISERP.Areas.VendorProcess.Controllers" }
            );
        }

    }
}