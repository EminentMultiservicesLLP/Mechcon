using BISERP.Areas.Training.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BISERPCommon;
using System.Threading.Tasks;
using System.Threading;
using WebGrease.Css.Ast.Selectors;

namespace BISERP.Areas.Training.Controllers
{
    public class TrainingTemplateController : Controller
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(TrainingTemplateController));
      
       

        [HttpGet]
        public async Task<ActionResult> GetTemplatedaysSlot(string slotDtl, string day, string TrainingCode,
            string TrainingName, int TrainingTemplateId)
        {
            TrainingTempHeaderModel model = new TrainingTempHeaderModel();
            string[] li = slotDtl.Split(';');
            model.BatchId = int.Parse(li[0]);
            model.BatchDays = int.Parse(li[1]);
            model.SlotPerDay = int.Parse(li[2]);
            model.BatchDay = day;
            model.TrainingCode = TrainingCode;
            model.TrainingName = TrainingName;
            List<TrainingCategoryModel> categories = null;
            List<TrainerModel> trainers = null;
                
            await Task.WhenAll(new Task[]
            {
                Task.Run(() => categories =
                    Common.AsyncWebCalls.GetAsync<List<TrainingCategoryModel>>(
                        "/TrainingCategory/getactiveTrainingCategory", CancellationToken.None).Result),

                Task.Run(() => trainers =
                    Common.AsyncWebCalls.GetAsync<List<TrainerModel>>("/trainer/getactiveTrainer",
                        CancellationToken.None).Result)
            });

            model.TrainingCategories = categories;
            model.TrainingTrainer = trainers;
            model.TrainingTemplateId = TrainingTemplateId;
            if (TrainingTemplateId != 0)
            {
                List<TrainingTempDaywiseModel> records;
                String url = "/trainingtemplate/GetTrainingTempDaywise/" +  TrainingTemplateId +"/"+ day;
                records =
                    await Common.AsyncWebCalls.GetAsync<List<TrainingTempDaywiseModel>>(url, CancellationToken.None);
                model.TrainingTempDaywiseModel = records;
            }
            return PartialView("~/Areas/Training/Views/Training/TrainingDayPartial.cshtml", model);
        }



        [HttpPost]
        public async Task<JsonResult> SaveTrainingTemplate(TrainingTempHeaderModel items)
        {
           
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedON = DateTime.Now;
            items.InsertedIPAddress = Common.Constants.IpAddress;
            items.InsertedMacID = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;
            
            {
                items = await Common.AsyncWebCalls.PostAsync("/trainingtemplate/CreateTrainingTemplate", items, CancellationToken.None);
                return items.IsSuccessStatusCode ? Json(new { success = true, TrainingTemplateId = items.TrainingTemplateId }) : Json(new { success = false , });
            }

        }


        [HttpGet]
        public async Task<JsonResult> GetTrainingTempDaywise(int TrainingTemplateId )
        {
            JsonResult jResult;
            //if (slotDtl )
            //string[] li = slotDtl.Split(';');
            //int SlotId = int.Parse(li[0]);
            try
            {
                String url = "/trainingtemplate/GetTrainingTempDaywise/" + TrainingTemplateId;
                var  records = await Common.AsyncWebCalls.GetAsync<List<TrainingTempDaywiseModel>>(url, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTrainingTemplateHdr :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }


        [HttpGet]
        public async Task<JsonResult> GetTrainingTemplateHdr()
        {
            JsonResult jResult;
            try
            {
                String url = "/trainingtemplate/gettrainingtemplatehdr";
                var records = await Common.AsyncWebCalls.GetAsync<List<TrainingTempHeaderModel>>(url, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTrainingTemplateHdr :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }
    }
}
