using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface ICityStateCountryMasterRepository
    {
        int SaveCity(CityMasterEntities model);
        int SaveState(StateMasterEntities model);
    }
}
