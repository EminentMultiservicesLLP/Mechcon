using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Training
{
    public class TrainingDaillyUpdatesHdrEntity
    {
        public int DaillyUpdId { get; set; }
        public int TrainingTemplateID { get; set; }
        public int TrainingDay { get; set; }
        public List<TrainingDailyUpdatesDtlEntity> DailyUpdateDtlModel { get; set; }
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

    public class TrainingDailyUpdatesDtlEntity
    {
        public int DaillyUpdId { get; set; }
        public int DaillyUpdDtlId { get; set; }
        public string TraineeName { get; set; }
        public string Performance { get; set; }
        public int TrainingDay { get; set; }
        public List<TrainingDailyUpdatesRatingEntity> DailyUpdatesRatingModel { get; set; }
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

    public class TrainingDailyUpdatesRatingEntity
    {
        public int DaillyUpdDtlId { get; set; }
        public string RatingId
        {
            get;
            set;
        }
        public string TraingTemplateDtlId { get; set; } // Its actually period id(training tempdtl Id)
    }

    public class TrainingTemplateSlotDetails
    {
        public string SlotName { get; set; }
        public int SlotId { get; set; }
    }

    public class TrainingTemplatePeriodDetails
    {
        public string PeriodName { get; set; }
        public int PeriodId { get; set; }
    }

    public class TraineeEntity
    {
        public string TraineeName { get; set; }
        public int TraineeId { get; set; }
    }

    public class TrainingEntity
    {
        public string TrainingName { get; set; }
        public int TrainingId { get; set; }
    }

    public class TrainingDaysEntity
    {
        public int TrainingDay { get; set; }
        public int TrainingDayId { get; set; }
    }

    public class TraniningTemplatePeriodRecordEntity
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
