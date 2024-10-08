﻿using System.Web.Mvc;

namespace BISERP.Areas.Asset
{
    public class AssetAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Asset";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Asset_default",
                "Asset/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BISERP.Areas.Asset.Controllers" }
            );
        }
    }
}
