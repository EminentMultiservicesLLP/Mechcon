using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Ticket.Controllers
{
    public class TicketController : Controller
    {
        public ActionResult TicketRegister()
        {
            return PartialView();
        }
        public ActionResult TicketActivity()
        {
            return PartialView();
        }
        public ActionResult Dashboard()
        {
            return PartialView();
        }
    }
}
