using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IInsuranceRepository
    {
        IEnumerable<InsuranceEntity> GetAllInsurance();

        IEnumerable<InsuranceEntity> InsuranceNotification(int DueDays);
        IEnumerable<InsuranceEntity> GetAllInsurance(int VehicleId);
        int CreateInsurance(InsuranceEntity Entity);
        bool UpdateInsurance(InsuranceEntity Entity);
        IEnumerable<InsuranceEntity> ActiveInsurance();
        bool CheckDuplicateInsurance(int VehicleId);
    }
}
