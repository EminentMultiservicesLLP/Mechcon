using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BISERPCommon;
using BISERP.Area.Branch.Models;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace BISERP.Areas.Branch.Controllers
{
    public class MaterialReturnGuardController : Controller
    {
         HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(MaterialReturnGuardController));

        public MaterialReturnGuardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpPost]
        public ActionResult SaveMaterialIssueGuard(MaterialReturnGuardModel mig)
        {
            string strErrorMsg = "";
            mig.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            mig.InsertedON = System.DateTime.Now;
            mig.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            mig.InsertedMacID = BISERP.Common.Constants.MacId;
            mig.InsertedMacName = BISERP.Common.Constants.MacName;

            string _url = url + "/mreturnguard/createReturnguard" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, mig, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                strErrorMsg = "Error In Material Return Guard";
                mig = JsonConvert.DeserializeObject<MaterialReturnGuardModel>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                strErrorMsg = "";
                mig = JsonConvert.DeserializeObject<MaterialReturnGuardModel>(result.Content.ReadAsStringAsync().Result);
            }

            if (strErrorMsg != "")
                return Json(new { success = false, responseText = strErrorMsg }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = true, responseText = mig.ReturnNo });
        }

    }
}
