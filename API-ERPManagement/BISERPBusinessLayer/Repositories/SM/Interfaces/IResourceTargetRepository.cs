using BISERPBusinessLayer.Entities.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Interfaces
{
    public interface IResourceTargetRepository
    {
        IEnumerable<FinancialYearEntities> GetFinancialYear();
        ResourceTargetEntities SaveResourceTargetDetail(ResourceTargetEntities model);
        IEnumerable<ResourceTargetDetailsEntities> GetResourceTargetDetail(int financialYearID);
    }
}
