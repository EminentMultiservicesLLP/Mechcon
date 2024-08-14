using BISERP.Areas.ScanDoc.Models;
using BISERP.Common;
using BISERPCommon;
using BISERPCommon.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.CommonUtilities.Controllers
{
    public class FileUploadDownloadController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static ILogger _logger = Logger.Register(typeof(FileUploadDownloadController));

        public FileUploadDownloadController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        [HttpPost]
        public JsonResult Upload(int NewId, string AreaLocation, string SubAreaLocation)
        {
            string saveAgainstId = ""; //string extraFolderName = "";            
            saveAgainstId = NewId.ToString();
            if (HttpContext.Request.Files.AllKeys.Any())
            {
                for (int i = 0; i <= HttpContext.Request.Files.Count; i++)
                {
                    TryCatch.Run(() =>
                        {
                            var file = HttpContext.Request.Files["files" + i];
                            if (file != null)
                            {
                                _logger.LogInfo("   (((((Upload => Starting file upload for speacialId :" + saveAgainstId + ", AreaLocation:" + AreaLocation);
                                string ScanDocLocation = AreaLocation.ToString()
                                                                        + (string.IsNullOrWhiteSpace(SubAreaLocation) == false ? "\\" + SubAreaLocation : "")
                                                                        + (string.IsNullOrWhiteSpace(saveAgainstId) == false ? "\\" + saveAgainstId : ""); 
                                
                                var fileSavePath = Path.Combine(Server.MapPath("~/Content/UploadFile/") + ScanDocLocation);
                                if (!Directory.Exists(fileSavePath))
                                {
                                    Directory.CreateDirectory(fileSavePath);
                                }
                                fileSavePath = fileSavePath + "\\" + file.FileName;
                                file.SaveAs(fileSavePath);
                                _logger.LogInfo("   File uploaded successfully for speacialId :" + saveAgainstId + ", AreaLocation:" + AreaLocation + " <= Upload)))))");

                                _logger.LogInfo("   (((((File upload DatabaseUpdate for speacialId :" + saveAgainstId + ", AreaLocation:" + AreaLocation);
                                UpdateDatabase(AreaLocation, SubAreaLocation, saveAgainstId, ScanDocLocation + "\\" + file.FileName, file.FileName);
                                _logger.LogInfo("   Successful File upload DatabaseUpdate for speacialId :" + saveAgainstId + ", AreaLocation:" + AreaLocation + "  <= File upload DatabaseUpdate)))))");
                            }
                        }).IfNotNull(ex =>
                        {
                            _logger.LogError("Error :- Issue while loading File, specialId :" + saveAgainstId + ", AreaLocation:" + AreaLocation);
                        });
                }
            }
            return Json(new { success = true });
        }

        public ActionResult Download()
        {
            string[] files = Directory.GetFiles(Server.MapPath("/Files"));
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }
            ViewBag.Files = files;
            return View();
        }

        public FileResult DownloadFile(string fileName)
        {
            var filepath = System.IO.Path.Combine(Server.MapPath("/Files/"), fileName);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
        }


        private void UpdateDatabase(string AreaLocation, string SubAreaLocation, string againstId, string filePath,string FileName)
        {
            ScanDocModel model = new ScanDocModel();
            model.FileId = Convert.ToInt32(againstId);
            model.FilePath = filePath;
            model.FileNames = FileName;
            model.ScanDocType = AreaLocation;
            model.ScanDocSubType = SubAreaLocation;
            model.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            model.InsertedON = System.DateTime.Now;
            model.InsertedIPAddress = BISERP.Common.Constants.IpAddress;
            model.InsertedMacID = BISERP.Common.Constants.MacId;
            model.InsertedMacName = BISERP.Common.Constants.MacName;
            string _url = url + "/scandoc/CreateScandoc" + Common.Constants.JsonTypeResult;
            var result = client.PostAsync(_url, model, new JsonMediaTypeFormatter()).Result;
        }
    }
}
