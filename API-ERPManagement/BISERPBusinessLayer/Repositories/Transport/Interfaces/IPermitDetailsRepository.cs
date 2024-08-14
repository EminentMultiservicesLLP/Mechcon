using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IPermitDetailsRepository
    {
        IEnumerable<PermitDetailsEntity> GetAllPermitDetails();
        IEnumerable<PermitDetailsEntity> PermitDetailsNotification(int DueDays);
        IEnumerable<PermitDetailsEntity> GetAllPermitDetails(int VehicleId);
        int CreatePermitDetails(PermitDetailsEntity Entity);
        bool UpdatePermitDetails(PermitDetailsEntity Entity);
        IEnumerable<PermitDetailsEntity> ActivePermitDetails();
        bool CheckDuplicatePermitDetails(int VehicleId);
    }
}
