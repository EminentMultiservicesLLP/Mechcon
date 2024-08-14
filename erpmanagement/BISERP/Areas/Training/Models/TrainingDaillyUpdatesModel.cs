using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BISERP.Areas.Training.Models
{

    public class TrainingDailyUpdateHdrModel : HttpResponseMessage
    {

        public int DaillyUpdId { get; set; }
        public int TrainingTemplateID { get; set; }
        public int TrainingDay { get; set; }
        public List<TrainingDailyUpdateDtlModel> DailyUpdateDtlModel { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }
        public string Message { get; set; }

    }


    public class TrainingDailyUpdateDtlModel : HttpResponseMessage
    {

        public int DaillyUpdId { get; set; }
        public int DaillyUpdDtlId { get; set; }
        public string TraineeName { get; set; }
        public string Performance { get; set; }
        public int TrainingDay { get; set; }
        public List<TrainingDailyUpdatesRatingModel> DailyUpdatesRatingModel { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }
        public string Message { get; set; }

    }


    public class TrainingDailyUpdatesRatingModel : HttpResponseMessage
    {
        public int DaillyUpdDtlId { get; set; }
        public string RatingId { get; set; }
        public string TraingTemplateDtlId { get; set; } // Its actually period id
    }

    public class TraineeModel : HttpResponseMessage
    {
        public string TraineeName { get; set; }
        public int TraineeId { get; set; }
    }

    public class TrainingModel : HttpResponseMessage
    {
        public string TrainingName { get; set; }
        public int TrainingId { get; set; }
    }
    public class TrainingTemplateSlotDetails : HttpResponseMessage
    {
        public string SlotName { get; set; }
        public int SlotId { get; set; }
    }
    public class TrainingDaysModel : HttpResponseMessage
    {
        public int TrainingDay { get; set; }
        public int TrainingDayId { get; set; }
    }

    public class TrainingTemplatePeriodDetails
    {
        public string PeriodName { get; set; }
        public int PeriodId { get; set; }
    }


    public class TraniningTemplatePeriodRecordModel : HttpResponseMessage
    {
        public int TrainingTemplateId { get; set; }
        public int DaillyUpdId { get; set; }
        public int DaillyUpdDtlId { get; set; }
        public string TraineeName { get; set; }
        public string Performance { get; set; }
        public string RatingId { get; set; }
        public int PeriodId { get; set; }

    }

}