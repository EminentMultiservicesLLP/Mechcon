using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using BISERP.Areas.Training.Models;
using System.Web.Mvc;
using BISERPCommon;
using Newtonsoft.Json;

namespace BISERP.Areas.Training.Controllers
{
    public class TrainingDaillyUpdatesController : Controller
    {
        static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(TrainingDaillyUpdatesController));

        public async Task<JsonResult> CreateTrainingDaillyUpdates(TrainingDailyUpdateHdrModel items)
        {
            items.InsertedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.InsertedON = DateTime.Now;
            items.InsertedIPAddress = Common.Constants.IpAddress;
            items.InsertedMacID = Common.Constants.MacId;
            items.InsertedMacName = Common.Constants.MacName;

            items.UpdatedBy = Convert.ToInt32(Session["AppUserId"].ToString());
            items.UpdatedOn = DateTime.Now;
            items.UpdatedIPAddress = Common.Constants.IpAddress;
            items.UpdatedMacID = Common.Constants.MacId;
            items.UpdatedMacName = Common.Constants.MacName;

            var result = await
                Common.AsyncWebCalls.PostAsync("/TrainingDaillyUpdates/CreateTrainingDaillyUpdates", items,
                        CancellationToken.None);
            return result.IsSuccessStatusCode ? Json(new { success = true }) : Json(new { success = false, result.Message });
        }


        public async Task<JsonResult> GetTrainee()
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TraineeModel>>("/TrainingDaillyUpdates/GuardTrainee", CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTrainee :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        public async Task<JsonResult> GetTraniningDay(int id)
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TrainingDaysModel>>("/TrainingDaillyUpdates/TraniningDay/" + id, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTraniningDay :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }


        public async Task<JsonResult> GetTraniningTemplatePeriod(int trainingtempdtId, int trainingDay)
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TrainingTemplatePeriodDetails>>("/TrainingDaillyUpdates/TraniningTemplatePeriod/" + trainingtempdtId + "/" + trainingDay, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTraniningTemplatePeriod :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

        public async Task<string> GetTraniningTemplatePeriodRecord(int trainingtempdtId, int trainingDay)
        {
            JsonResult jResult;
            string json = "";
            try
            {

                var records = await Common.AsyncWebCalls.GetAsync<List<TraniningTemplatePeriodRecordModel>>("/TrainingDaillyUpdates/TraniningTemplatePeriodRecord/" + trainingtempdtId + "/" + trainingDay, CancellationToken.None);
                var result = records.GroupBy(g => new { g.TrainingTemplateId, g.DaillyUpdId, g.DaillyUpdDtlId, g.TraineeName, g.Performance })
                                .Select(s =>
                                {
                                    var dict = new Dictionary<int, string>();
                                    foreach (var i in s)
                                    {
                                        dict.Add(i.PeriodId, i.RatingId);
                                    }
                                    return new
                                    {
                                        DaillyUpdId=s.Key.DaillyUpdId,
                                        TraineeName = s.Key.TraineeName,
                                        DaillyUpdDtlId = s.Key.DaillyUpdDtlId,
                                        Performance = s.Key.Performance,
                                        Periods = dict
                                    };
                                });

                json = JsonConvert.SerializeObject(result);
                jResult = new JsonResult
                {
                    Data = JsonConvert.DeserializeObject(json)
                };

                //jResult = Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTraniningTemplatePeriodRecord :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return json;
        }

        public static DataTable ToPivotTable<T, TColumn, TRow, TData>(
            IEnumerable<T> source,
            Func<T, TColumn> columnSelector,
            Expression<Func<T, TRow>> rowSelector,
            Func<IEnumerable<T>, TData> dataSelector)
        {
            DataTable table = new DataTable();
            var rowName = ((MemberExpression)rowSelector.Body).Member.Name;
            table.Columns.Add(new DataColumn(rowName));
            var columns = source.Select(columnSelector).Distinct();

            table.Columns.Add(new DataColumn("DaillyUpdId"));
            table.Columns.Add(new DataColumn("TraineeName"));
            table.Columns.Add(new DataColumn("Performance"));

            foreach (var column in columns)
                table.Columns.Add(new DataColumn(column.ToString()));



            var rows = source.GroupBy(rowSelector.Compile())
                             .Select(rowGroup => new
                             {
                                 Key = rowGroup.Key,
                                 Values = columns.GroupJoin(
                                     rowGroup,
                                     c => c,
                                     r => columnSelector(r),
                                     (c, columnGroup) => dataSelector(columnGroup))
                             });

            foreach (var row in rows)
            {
                var dataRow = table.NewRow();
                var items = row.Values.Cast<object>().ToList();
                items.Insert(0, row.Key);
                dataRow.ItemArray = items.ToArray();
                table.Rows.Add(dataRow);
            }

            return table;
        }

        public async Task<JsonResult> GetTrainingTemplate(int trainingTypeId)
        {
            JsonResult jResult;
            try
            {
                var records = await Common.AsyncWebCalls.GetAsync<List<TrainingModel>>("/TrainingDaillyUpdates/TraniningTemplateSlot/" + trainingTypeId, CancellationToken.None);
                jResult = Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetTrainingTemplate :" + ex.Message + Environment.NewLine + ex.StackTrace);
                jResult = Json("Error");
            }
            return jResult;
        }

    }
}
