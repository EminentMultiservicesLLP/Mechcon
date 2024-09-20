using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Miscellaneous.Models;
using BISERP.Models.UserMangement;
using BISERPCommon;
//using BISERP.Areas.DailyData.Models;
using System.Threading;
using BISERP.Filters;

namespace BISERP.Areas.Masters.ControllerS
{
    [LogActionFilter]
    [CustomeExceptionHandlerFilter]
    public class MastersController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MastersController));

        public MastersController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        public ActionResult UnitMaster()
        {
            return PartialView();
        }

        public ActionResult ItemTypeMaster()
        {
            return PartialView();
        }

        public ActionResult ItemPackSizeMaster()
        {
            return PartialView();
        }
        public ActionResult SupplierMaster()
        {
            return PartialView();
        }
        public ActionResult DeliveryTermMaster()
        {
            return PartialView();
        }

        public ActionResult PaymentTermMaster()
        {
            return PartialView();
        }

        public ActionResult OtherTermMaster()
        {
            return PartialView();
        }

        public ActionResult ItemMaster()
        {
            return PartialView();
        }
        public ActionResult StoreMaster()
        {

            return PartialView();
        }

        public ActionResult StorewiseItemTypeMappingMaster()
        {
            return PartialView();
        }
        public ActionResult Vendormaster()
        {
            return PartialView();
        }
        public ActionResult ClientMaster()
        {
            return PartialView();
        }
        public ActionResult InspectionMaster()
        {
            return PartialView();
        }
        public ActionResult BasisMaster()
        {
            return PartialView();
        }
        public ActionResult BudgetHeads()
        {
            return PartialView();
        }
        public ActionResult MechconMaster()
        {
            return PartialView();
        }
        public ActionResult CityStateCountryMaster()
        {
            return PartialView();
        }
        public ActionResult ProjectTC()
        {
            return PartialView();
        }
        public ActionResult StoreMasterApproval()
        {
            return PartialView();
        }
        public ActionResult SupplierMasterApproval()
        {
            return PartialView();
        }
        public ActionResult StoreConsumption()
        {
            return PartialView();
        }
        public ActionResult DesignApproval()
        {
            return PartialView();
        }
        public ActionResult ElectricalDesignApproval()
        {
            return PartialView();
        }
        public ActionResult MarketingApproval()
        {
            return PartialView();
        }
        public ActionResult ProjectsApproval()
        {
            return PartialView();
        }
        public ActionResult ClearanceNote()
        {
            return PartialView();
        }
        public ActionResult PackingList()
        {
            return PartialView();
        }
        public ActionResult ProjectStatus()
        {
            return PartialView();
        }
    }
}
