using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IFuelDetailsRepository
    {
        IEnumerable<FuelDetailsEntity> GetAllFuelDetails(int ScheduleId);
        IEnumerable<FuelDetailsEntity> GetAllFuelSchedule(int ScheduleId);
        int CreateFuelDetails(FuelDetailsEntity Entity);
      //  int UpdateFuelDetails(FuelDetailsEntity Entity);
    }
}
