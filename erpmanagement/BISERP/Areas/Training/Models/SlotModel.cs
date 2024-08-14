using System;
using System.Net.Http;

namespace BISERP.Areas.Training.Models
{
    public class SlotModel : HttpResponseMessage
    {
        public int SlotId { get; set; }
        public string SlotCode { get; set; }
        public string SlotName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string NoOfDays { get; set; }
        public string NoOfSlot { get; set; }
        public string Remarks { get; set; }
        public int TrainingTypeId{ get; set; }  
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
        public string strFromDate { get; set; }
        public string strToDate { get; set; }
        public string TraniningSlotCode { get; set; }
        public string SlotDate { get; set; }

    }
}