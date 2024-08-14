using BISERPBusinessLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface IScheduleRepository
    {
        IEnumerable<ScheduleEntity> GetAllSchedules();
        int CreateSchedule(ScheduleEntity Entity);
        bool UpdateSchedule(ScheduleEntity Entity);
    }
}
