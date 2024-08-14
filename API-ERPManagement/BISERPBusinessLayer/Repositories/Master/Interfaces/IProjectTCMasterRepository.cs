using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IProjectTCMasterRepository
    {
        IEnumerable<ProjectTCMasterEntities> GetAllProjectTC();
        int CreateProjectTC(ProjectTCMasterEntities entity);
        bool UpdateProjectTC(ProjectTCMasterEntities entity);
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int ID);
    }
}
