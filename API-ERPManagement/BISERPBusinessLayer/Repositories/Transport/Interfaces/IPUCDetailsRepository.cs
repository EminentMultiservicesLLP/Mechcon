using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IPUCDetailsRepository
    {
        IEnumerable<PUCDetailsEntity> GetAllPUCDetails();
        IEnumerable<PUCDetailsEntity> PUCDetailsNotification(int DueDays);
        IEnumerable<PUCDetailsEntity> GetAllPUCDetails(int VehicleId);
        int CreatePUCDetails(PUCDetailsEntity Entity);
        bool UpdatePUCDetails(PUCDetailsEntity Entity);
        IEnumerable<PUCDetailsEntity> ActivePUCDetails();
        bool CheckDuplicatePUCDetails(int VehicleId);
    }
}
