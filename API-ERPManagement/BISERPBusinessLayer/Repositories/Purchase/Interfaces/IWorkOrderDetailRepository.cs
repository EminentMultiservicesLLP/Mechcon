using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IWorkOrderDetailRepository
    {
        int CreateWorkOrderDetails(int IndentId, WorkOrderDetailEntities entity, DBHelper dbhelper);
        List<WorkOrderDetailEntities> GetWorkOrderDetailById(int IndentId);
        bool UpdateWorkOrderAuthQty(WorkOrderEntities mainentity, WorkOrderDetailEntities entity, DBHelper dbhelper);
    }
}
