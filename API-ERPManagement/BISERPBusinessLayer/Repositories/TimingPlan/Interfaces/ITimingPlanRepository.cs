using BISERPBusinessLayer.Entities.TimingPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.TimingPlan.Interfaces
{
    public interface ITimingPlanRepository
    {
        #region Schedule
        IEnumerable<TP_TaskMaster> GetTaskMaster();
        IEnumerable<TP_ProjectTaskSchedule> GetProjectTaskSchedule(int ProjectID);
        TP_PTSchedule SaveProjectTaskSchedule(TP_PTSchedule model);
        IEnumerable<TP_PTSchedule> GetProjectTaskRole(int ProjectID);
        IEnumerable<TP_ProjectTaskScheduleDetails> GetProjectTaskScheduleDetails(int ProjectID);
        #endregion Schedule

        #region TaskProgress
        IEnumerable<TP_ProjectTaskSchedule> GetTaskProgress(int IsCompleted, int LoginId);
        TP_ProjectTaskSchedule SaveTaskProgress(TP_ProjectTaskSchedule model);
        #endregion TaskProgress

        #region TaskMonitor
        IEnumerable<TP_ProjectTaskSchedule> GetTaskMonitor(int ProjectID);
        #endregion TaskMonitor

        #region TaskTracking
        IEnumerable<TP_ProjectTaskSchedule> GetTaskTracking(int ProjectID);
        #endregion TaskTracking
    }
}
