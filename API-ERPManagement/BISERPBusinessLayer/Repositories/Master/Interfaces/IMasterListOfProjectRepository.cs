using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Interfaces
{
    public interface IMasterListOfProjectRepository
    {
        List<MasterListOfProjectEntity> GetMasterListOfProject(int financialYearID, int pending);
        List<ProjectCostingSummaryEntity> GetProjectCostingSummary(int financialYearID);
    }
}
