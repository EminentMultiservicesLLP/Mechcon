using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Asset.Models;
using BISERP.Areas.Branch.Models;
using BISERP.Areas.CommonUtilities.Models;
using BISERPCommon;
using Newtonsoft.Json;

namespace BISERP.Areas.CommonUtilities.Controllers
{
    public class PettyCashController : Controller
    {
        readonly HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(PettyCashController));

        public PettyCashController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        [ActionName("SaveTransaction")]
        public ActionResult SavePettyCashTransaction(List<PettyCashModel> items)
        {
            //string strErrorMsg = "";
            var isSuccess = false;
            PettyCashModel pettycash = new PettyCashModel();
            List<PettyCashModel> modelList = new List<PettyCashModel>();
            foreach (var item in items)
            {
                item.CreatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                item.CreatedDate = DateTime.Now;
                var model = item;
                modelList.Add(model);
            }

            var _url = url + "/PettyCashTransaction/InsupPettyCashTransaction" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, modelList, new JsonMediaTypeFormatter()).Result;//.Content.ReadAsAsync<MaterialIssueModel>().Id;
            if (result.StatusCode.ToString() == "BadRequest")
            {
                JsonConvert.DeserializeObject<List<PettyCashModel>>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "Created")
            {
                isSuccess = true;
                JsonConvert.DeserializeObject<List<PettyCashModel>>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode.ToString() == "OK")
            {
                isSuccess = true;
            }

            if (!isSuccess)
                return Json(new { success = false, Message = "Error In  PettyCash Register" });
            else
                return Json(new { success = true, Message = "PettyCash Register saved successfully." });
        }


        [HttpGet]
        public async Task<JsonResult> GetPettyCash(string FromDate, string Todate, int Type)
        {
            JsonResult jResult;
            List<PettyCashModel> model = new List<PettyCashModel>();
            try
            {

                //string strfromdate = Convert.ToDateTime(FromDate).ToString("dd-MM-yy");
                //string strtodate = Convert.ToDateTime(Todate).ToString("dd-MM-yy");

                if (Type == 1)
                {
                    string _url = url + "/PettyCashTransaction/GetPettyCashDEPOSITE/" + FromDate + "/" + Todate + "/" + Type;
                    model = await Common.AsyncWebCalls.GetAsync<List<PettyCashModel>>(client, _url, CancellationToken.None);

                }
                else
                {
                    string _url = url + "/PettyCashTransaction/GetPettyCashWITHDRAWAL/" + FromDate + "/" + Todate + "/" + Type;
                    model = await Common.AsyncWebCalls.GetAsync<List<PettyCashModel>>(client, _url, CancellationToken.None);

                }

                if (model != null)
                {
                    jResult = Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetPettyCash :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


    }
}
