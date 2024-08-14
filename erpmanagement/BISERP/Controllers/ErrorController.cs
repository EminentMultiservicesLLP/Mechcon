using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Controllers
{
    public class ErrorController : Controller
    {

       
        public ActionResult ViewNotFound()
        {
            return PartialView();
        }

    }
}
