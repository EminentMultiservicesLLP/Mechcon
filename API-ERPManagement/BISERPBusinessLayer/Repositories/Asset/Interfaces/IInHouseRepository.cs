using BISERPBusinessLayer.Entities.Asset;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IInHouseRepository
    {        
        InHouseEntity CreateInHouseMaintenance(InHouseEntity Entity, DBHelper dbhelper);

        bool UpdateInHouseMaintenance(InHouseEntity Entity, DBHelper dbhelper);
    }
}
