using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IDriverScheduleRepository
    {
        IEnumerable<DriverScheduleEntity> GetAllDriverSchedule();
        IEnumerable<DriverScheduleEntity> GetAllSchedule();
        IEnumerable<DriverScheduleEntity> GetAllScheduleRpt(int VehicleId, int branchId, DateTime fromdate, DateTime todate);
        IEnumerable<DriverScheduleEntity> GetFuelconsumptionvehiclewiserpt(int branchId, DateTime fromdate, DateTime todate, int vehicletypeId);
        int CreateDriverSchedule(DriverScheduleEntity Entity);
        bool UpdateDriverSchedule(DriverScheduleEntity Entity);
    }
}
