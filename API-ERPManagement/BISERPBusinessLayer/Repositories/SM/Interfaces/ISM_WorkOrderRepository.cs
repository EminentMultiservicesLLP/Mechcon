using BISERPBusinessLayer.Entities.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Interfaces
{
    public interface ISM_WorkOrderRepository
    {
        IEnumerable<WorkOrderEntities> GetEnqForWorkOrder(int UserID);
    }
}
