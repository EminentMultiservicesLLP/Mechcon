﻿using System.Web.Mvc;

namespace BISERP.Areas.Training
{
    public class TrainingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Training";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Training_default",
                "Training/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BISERP.Areas.Training.Controllers" }
            );
        }
    }
}
