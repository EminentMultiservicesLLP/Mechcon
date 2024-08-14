using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface ITechnicianRepository
    {
        IEnumerable<TechnicianEntity> GetAllTechnician();
        int CreateTechnician(TechnicianEntity entity);
        bool UpdateTechnician(TechnicianEntity entity);
        IEnumerable<TechnicianEntity> GetActiveTechnician();
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int technicianId);
    }
}
