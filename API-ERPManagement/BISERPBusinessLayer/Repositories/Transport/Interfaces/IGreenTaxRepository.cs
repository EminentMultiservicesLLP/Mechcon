using BISERPBusinessLayer.Entities.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Interfaces
{
    public interface IGreenTaxRepository
    {
        IEnumerable<GreenTaxEntity> GetAllGreenTax();
        IEnumerable<GreenTaxEntity> GreenTaxNotification(int DueDays);
        IEnumerable<GreenTaxEntity> GetAllGreenTax(int VehicleId);
        int CreateGreenTax(GreenTaxEntity Entity);
        bool UpdateGreenTax(GreenTaxEntity Entity);
        IEnumerable<GreenTaxEntity> ActiveGreenTax();
        bool CheckDuplicateGreenTax(int VehicleId);
    }
}
