using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.TimingPlan.Models
{
    public class TP_TaskMaster
    {
        public int? TaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool IsActive { get; set; }
    }
    public class TP_PTSchedule
    {
        public int? ProjectID { get; set; }
        public int? ProjectOwnerID { get; set; }
        public string ProjectOwner { get; set; }
        public int? ProjectHeadID { get; set; }
        public string ProjectHead { get; set; }
        public int? ProjectEngineerID { get; set; }
        public string ProjectEngineer { get; set; }
        public List<TP_ProjectTaskSchedule> TP_PTScheduleList { get; set; }
        public int LoginId { get; set; }
        public int? isDeleted { get; set; }
    }
    public class TP_ProjectTaskSchedule
    {
        public int? ScheduleID { get; set; }
        public int? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int? TaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int? AssignedTo { get; set; }
        public string AssignedToName { get; set; }
        public string strStartDate { get; set; }
        public string strEndDate { get; set; }
        public decimal? ProgressPercent { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string Action { get; set; }
        public int? LastActionBy { get; set; }
        public string LastActionByName { get; set; }
        public string strLastActionDate { get; set; }
        public string DeliveryStatus { get; set; }
        public bool IsActive { get; set; }
        public int? ScheduledWeeks { get; set; }
        public int? TakenWeeks { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string strCreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string strUpdatedOn { get; set; }
        public bool State { get; set; }
        public List<TP_ProjectTaskScheduleDetails> PTDetails { get; set; }
    }
    public class TP_ProjectTaskScheduleDetails
    {
        public int? ScheduleID { get; set; }
        public int? DetailID { get; set; }
        public string DateKey { get; set; }
        public string Date { get; set; }
        public bool IsSchedule { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string strUpdatedOn { get; set; }
    }
}