using BISERP.Areas.Masters.Models;
using BISERPCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Masters.Controllers
{
    public class ProductMasterController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(ProductMasterController));

        public ProductMasterController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpGet]
        public async Task<JsonResult> AllProduct(string searchProduct, string searchCode)
        {
            List<ProductMasterModel> Pimodel = new List<ProductMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = url + "/Product/getAllProduct" + Common.Constants.JsonTypeResult;
                var records = await Common.AsyncWebCalls.GetAsync<List<ProductMasterModel>>(client, _url, CancellationToken.None);

                if (records != null && records.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(searchProduct))
                    {
                        records = records.Where(p => p.ProductDesc.ToUpper().StartsWith(searchProduct.ToUpper())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(searchCode))
                    {
                        records = records.Where(p => p.ProductName.ToUpper().StartsWith(searchCode.ToUpper())).ToList();
                    }
                    int total = records.Count;

                    jResult = Json(new { success = true, records, total }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { success = false, Messsage = "No Details found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AllProduct :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Server Error! Please Contact administrator!");
            }
            return jResult;
        }

        [HttpPost]
        public ActionResult SaveProduct(ProductMasterModel Product)
        {
            string _url = "";
            bool _isSuccess = true;
            if (Product.ProductID > 0)
            {
                _url = url + "/Product/updateProduct" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Product, new JsonMediaTypeFormatter()).Result;//.Result.Content.ReadAsAsync<int>().Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    Product = JsonConvert.DeserializeObject<ProductMasterModel>(result.Content.ReadAsStringAsync().Result);
                }

                if (!_isSuccess)
                    return Json(new { success = false, Message = Product.Message });
                else
                    return Json(new { success = true });
            }
            else
            {
                _url = url + "/Product/createProduct" + Common.Constants.JsonTypeResult;
                var result = client.PostAsync(_url, Product, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    _isSuccess = false;
                    Product = JsonConvert.DeserializeObject<ProductMasterModel>(result.Content.ReadAsStringAsync().Result);
                }
            }
            if (!_isSuccess)
                return Json(new { success = false, Message = Product.Message });
            else
                return Json(new { success = true });
        }
    }
}
