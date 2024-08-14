using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IVillageMasterRepository
    {
        VillageMasterEntities GetVillageById(int Id);
        IEnumerable<VillageMasterEntities> GetAllVillages();
        bool CreateVillage(VillageMasterEntities entity);
        bool UpdateVillage(VillageMasterEntities entity);
        bool DeleteVillage(VillageMasterEntities entity);
    }
}
