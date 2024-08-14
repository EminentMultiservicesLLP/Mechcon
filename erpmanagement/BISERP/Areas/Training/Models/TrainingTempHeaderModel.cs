using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Org.BouncyCastle.Asn1.Cms;

namespace BISERP.Areas.Training.Models
{
    public class TrainingTempHeaderModel: HttpResponseMessage
    {
        public int TrainingTemplateId { get; set; }
        public string TrainingName { get; set; }
        public string TrainingCode { get; set; }
        public int BatchId { get; set; }
        public string BatchDay { get; set; }
        public int BatchDays { get; set; }
        public int SlotPerDay { get; set; }

        public List<TrainingCategoryModel> TrainingCategories { get; set; }
        public List<TrainerModel> TrainingTrainer { get; set; }
        //public List<TrainerModel> TrainingTrainer { get; set; }

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
        public int TrainerId { get; set; }
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