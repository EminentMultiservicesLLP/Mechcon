using BISERP.Areas.Store.Models.Store;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Miscellaneous.Models;

namespace BISERP.Areas.Miscellaneous.Controllers
{
    public class OpeningBalanceController : Controller
    {
      HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(OpeningBalanceController));

        public OpeningBalanceController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public ActionResult SaveOpeningBalance(List<OpeningBalanceModel> items)
        {
            string _url = "";
            List<OpeningBalanceModel> modelList = new List<OpeningBalanceModel>();
            foreach (var item in items)
            {
                OpeningBalanceModel model = new OpeningBalanceModel();
                item.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                item.InsertedON = System.DateTime.Now;
                item.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
                item.InsertedMacID = BISERP.Common.Constants.MacId;
                item.InsertedMacName = BISERP.Common.Constants.MacName;
                model = item;
                modelList.Add(model);
            }
            //if (items.ID > 0)
            //{
            //    _url = url + "/items/updateitemmaster" + Common.Constants.JsonTypeResult;
            //    client.PutAsync(_url, items, new JsonMediaTypeFormatter());//.Result.Content.ReadAsAsync<int>().Result;
            //}
            //else
            //{
            _url = url + "/openbal/Createopenbal" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, modelList, new JsonMediaTypeFormatter()).Result.Content.ReadAsAsync<OpeningBalanceModel>().Id;
            //}
            return Json(new { success = true });
        }


    }
}
