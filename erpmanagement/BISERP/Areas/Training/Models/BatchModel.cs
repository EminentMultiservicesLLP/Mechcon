using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Training.Models
{
    public class BatchModel
    {        
        public int BatchId { get; set; }

        [Display(Name="Centre")]
        public int CentreId { get; set; }

        [Display(Name = "Batch Type")]
        public int BatchTypeId { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }


        [Display(Name = "noOfSlot")]
        public int NoOfSlot { get; set; }

        [Display(Name = "NoOfDays")]
        public int NoOfDays { get; set; }

        [Display(Name = "Accomodation Qty")]
        public float BatchQty { get; set; }

        [Display(Name = "Training Type")]
        public int TrainingTypeId { get; set; }

        [Display(Name = "Grade")]
        public int GradeId { get; set; }
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
        public string Centre { get; set; }
        public string BatchType { get; set; }
        public string TrainingType { get; set; }
        public string strStartDate { get; set; }
        public string strEndDate { get; set; }
        public string Grade { get; set; }
        public string BatchName { get; set; }
        public string Message { get; set; }
    }
}