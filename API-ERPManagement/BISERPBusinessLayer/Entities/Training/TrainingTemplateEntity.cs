using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Training
{
    public class TrainingTemplateEntity
    {
        public int Id { get; set; }
        public int SlotId { get; set; }
        public int CategoryId { get; set; }
        public int TrainerId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
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
        public string TrainingName { get; set; }
        public string TrainingCode { get; set; }

     
    }

    public class TrainingTempHeaderModel
    {
        public int TrainingTemplateId { get; set; }
        public string TrainingName { get; set; }
        public string TrainingCode { get; set; }

        public int BatchId { get; set; }
        public int BatchDays { get; set; }
        public int BatchPerDay { get; set; }
        public List<TrainingTempDaywiseModel> TrainingTempDaywiseModel { get; set; }
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
        //extra 
        public string BatchName { get; set; }
        public string TrainingTypeCode { get; set; }
        public int TrainingTypeId { get; set; }

    }

    public class TrainingTempDaywiseModel
    {
        public int Id { get; set; }
        public int TrainingTemplateId { get; set; }
        public int TrainingDay { get; set; }
        public int CategoryId { get; set; }
        public int TrainerId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
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

        public string strFromTime { get; set; }
        public string strToTime { get; set; }


    }
}
