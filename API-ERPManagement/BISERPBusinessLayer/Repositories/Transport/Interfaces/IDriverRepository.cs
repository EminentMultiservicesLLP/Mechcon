using BISERPBusinessLayer.Entities.Transport;
using System.Collections.Generic;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IDriverRepository
    {
        IEnumerable<DriverEntity> GetAllDriver();
        IEnumerable<DriverEntity> GetAllDriverSchedule(int branchId);

    }
}
