using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using BISERPCommon;
using BISERP.Models.UserMangement;
using BISERP.Areas.Asset.Models;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Threading;
using BISERP.Areas.Masters.Models;

namespace BISERP.Areas.Asset.Controllers
{
    public class AssetController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _url = Common.Constants.WebAPIAddress;
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(AssetController));

        public AssetController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_url);
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public ActionResult Asset(int MenuId)
        {
            if (Session["AppUserId"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            MenuUserRightsModel model = new MenuUserRightsModel();
            List<MenuUserRightsModel> menurights = (List<MenuUserRightsModel>)Session["UserMenu"];

            model.Parent = menurights.Select(row => new ParentMenuRights
            {
                MenuId = row.MenuId,
                UserId = row.UserId,
                Access = row.Access,
                MenuName = row.MenuName,
                PageName = row.PageName,
                ParentMenuId = row.ParentMenuId
            }).Where(m => m.ParentMenuId.Equals(7)).ToList();
            if (MenuId > 7)
            {
                model.Child = menurights.Select(row => new ChildMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => m.ParentMenuId.Equals(MenuId)).ToList();
            }
            return View(model);
        }

        public ActionResult Masters(int MenuId)
        {
            MenuUserRightsModel model = new MenuUserRightsModel();
            List<MenuUserRightsModel> menurights = (List<MenuUserRightsModel>)Session["UserMenu"];

            model.Parent = menurights.Select(row => new ParentMenuRights
            {
                MenuId = row.MenuId,
                UserId = row.UserId,
                Access = row.Access,
                MenuName = row.MenuName,
                PageName = row.PageName,
                ParentMenuId = row.ParentMenuId
            }).Where(m => m.ParentMenuId.Equals(7)).ToList();
            if (MenuId > 7)
            {
                model.Child = menurights.Select(row => new ChildMenuRights
                {
                    MenuId = row.MenuId,
                    UserId = row.UserId,
                    Access = row.Access,
                    MenuName = row.MenuName,
                    PageName = row.PageName,
                    ParentMenuId = row.ParentMenuId
                }).Where(m => m.ParentMenuId.Equals(MenuId)).ToList();
            }
            return View(model);
        }

        public ActionResult Floor()
        {
            return PartialView();
        }

        public ActionResult Room()
        {
            return PartialView();
        }

        public ActionResult Technician()
        {
            return PartialView();
        }

        public ActionResult AssetType()
        {
            return PartialView();
        }

        public ActionResult SubType()
        {
            return PartialView();
        }

        public ActionResult MaintainanceType()
        {
            return PartialView();
        }

        public ActionResult CallType()
        {
            return PartialView();
        }

        public ActionResult AMCProvider()
        {
            return PartialView();
        }
        public ActionResult AssetDetails()
        {
            return PartialView();
        }

        public ActionResult AssetSchedule()
        {
            return PartialView();
        }

        public ActionResult AssetLocation()
        {
            return PartialView();
        }
        public ActionResult RegisterRequest()
        {
            return PartialView();
        }
        public ActionResult AcceptRequest()
        {
            return PartialView();
        }
        public ActionResult AssetDeletionRequest()
        {
            return PartialView();
        }
        public ActionResult AssetScheduleCompletion()
        {
            return PartialView();
        }

        public ActionResult InHouseMaintenance()
        {
            return PartialView();
        }

        public ActionResult OutsideMaintenance()
        {
            return PartialView();
        }

        public ActionResult WarrantyMaintenance()
        {
            return PartialView();
        }
        public ActionResult QuickView()
        {
            return PartialView();
        }

        public ActionResult Location()
        {
            return PartialView();
        }
        public ActionResult Vendor()
        {
            return PartialView();
        }

        public async Task<ActionResult> Reports()
        {
            int MenuId = 134;
            List<ReportListModel> model = new List<ReportListModel>();
            string url = this._url + "/user/getuserreports/" + Session["AppUserId"].ToString() + "/" + MenuId + Common.Constants.JsonTypeResult;
            model = await Common.AsyncWebCalls.GetAsync<List<ReportListModel>>(_client, url, CancellationToken.None);

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SaveAsset(List<AssetModel> modellist)
        {
            string strError = "";
            bool isSuccess = false;
            List<AssetModel> assetList = new List<AssetModel>();
            foreach (var item in modellist)
            {
                if(item.AssetCode != null)
                {
                    if(item.AssetCode == "")
                    {
                        strError = "Please Enter AssetCode";
                    }
                }
                else
                {
                    strError = "Please Enter AssetCode";
                }
                if (strError == "")
                {
                    item.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
                    item.InsertedON = DateTime.Now;
                    item.InsertedIPAddress = Common.Constants.IpAddress;
                    item.InsertedMacID = Common.Constants.MacId;
                    item.InsertedMacName = Common.Constants.MacName;
                    var model = item;
                    assetList.Add(model);
                }
            }
            if (strError == "")
            {
                var url = _url + "/asset/createasset" + Common.Constants.JsonTypeResult;
                var result = _client.PostAsync(url, assetList, new JsonMediaTypeFormatter()).Result;
                if (result.StatusCode.ToString() == "BadRequest")
                {
                    isSuccess = false;
                }
                else if (result.StatusCode.ToString() == "Created")
                {
                    isSuccess = true;
                }
                if (!isSuccess)
                    return Json(new { success = false });
                else
                    return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetBranchAsset(int BranchId)
        {
            List<AssetModel> records = new List<AssetModel>();
            JsonResult jResult;
            try
            {
                string _url = this._url + "/asset/getbranchassets/" + BranchId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<AssetModel>>(_client, _url, CancellationToken.None);
                if (records != null)
                {                    
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetBranchAsset :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        [HttpGet]
        public async Task<JsonResult> GetSupplierNameAssetdt(int ItemId)
        {
            List<SupplierMasterModel> records = new List<SupplierMasterModel>();
            JsonResult jResult;
            try
            {
                string _url = this._url + "/asset/GetSupplierNameAssetdt/" + ItemId + Common.Constants.JsonTypeResult;
                records = await Common.AsyncWebCalls.GetAsync<List<SupplierMasterModel>>(_client, _url, CancellationToken.None);
                if (records != null)
                {
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetSupplierNameAssetdt :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
