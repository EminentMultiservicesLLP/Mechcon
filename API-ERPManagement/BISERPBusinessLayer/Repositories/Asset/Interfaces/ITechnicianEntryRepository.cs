using BISERPBusinessLayer.Entities.Asset;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface ITechnicianEntryRepository
    {
        TechnicianEntryEntity GetTechnicianEntry(int InHouseId);
        int CreateTechnicianEntry(InHouseEntity MainEntity, TechnicianEntryEntity Entity, DBHelper dbhelper);
        bool UpdateTechnicianEntry(InHouseEntity MainEntity, TechnicianEntryEntity Entity, DBHelper dbhelper);
    }
}
