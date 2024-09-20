using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Marketing.Controllers
{
    public class MarketingController : Controller
    {
        public ActionResult EnquiryRegister()
        {
            return PartialView();
        }
        public ActionResult EnquiryAllocation()
        {
            return PartialView();
        }
        public ActionResult OfferRegister()
        {
            return PartialView();
        }
        public ActionResult OrderBook()
        {
            return PartialView();
        }
        public ActionResult WorkOrder()
        {
            return PartialView();
        }
    }
}
