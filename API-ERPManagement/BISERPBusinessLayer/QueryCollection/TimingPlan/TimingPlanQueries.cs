using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.TimingPlan
{
    public class TimingPlanQueries
    {
        //Schedule
        public const string GetTaskMaster = "dbsp_TP_GetTaskMaster";
        public const string GetProjectTaskSchedule = "dbsp_TP_GetProjectTaskSchedule";
        public const string SaveProjectTaskRole = "dbsp_TP_SaveProjectTaskRole";
        public const string SavePTSchedule = "dbsp_TP_SaveProjectTaskSchedule";
        public const string SavePTScheduleDetails = "dbsp_TP_SaveProjectTaskScheduleDetails";
        public const string FinalizeTaskScheduleAfterSave = "dbsp_TP_FinalizeTaskScheduleAfterSave";
        public const string GetProjectTaskRole = "dbsp_TP_GetProjectTaskRole";
        public const string GetProjectTaskScheduleDetails = "dbsp_TP_GetProjectTaskScheduleDetails";
        //TaskProgress
        public const string GetTaskProgress = "dbsp_TP_GetTaskProgress";
        public const string SaveTaskProgress = "dbsp_TP_SaveTaskProgress";
        //TaskMonitor
        public const string GetTaskMonitor = "dbsp_TP_GetTaskMonitor";
        //TaskTracking
        public const string GetTaskTracking = "dbsp_TP_GetTaskTracking";
    }
}
