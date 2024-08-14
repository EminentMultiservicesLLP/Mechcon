using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Configuration.Controllers
{
    public class ConfigurationController : Controller
    {
        public ActionResult ProjectLogic()
        {
            return PartialView();
        }
        public ActionResult SeriesLogic()
        {
            return PartialView();
        }
        public ActionResult TaxMaster()
        {
            return PartialView();
        }
        public ActionResult StockView()
        {
            return PartialView();
        }
    }
}
