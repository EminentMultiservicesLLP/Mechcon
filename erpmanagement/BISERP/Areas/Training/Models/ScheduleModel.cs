using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Training.Models
{
    public class ScheduleModel
    {
        public int ScheduleId { get; set; }

        [Display(Name = "Schedule Date")]
        public DateTime ScheduleDate { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Display(Name="Batch")]
        public int BatchId { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [Display(Name = "Subject Topic")]
        public int SubjectTopicId { get; set; }

        [Display(Name = "Trainer")]
        public int TrainerId { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }
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
        public string strScheduleDate { get; set; }
        public string strStartTime { get; set; }
        public string strEndTime { get; set; }
        public string BatchName { get; set; }
        public string Subject { get; set; }
        public string TopicName { get; set; }
        public string Trainer { get; set; }

        public string Message { get; set; }
    }
}