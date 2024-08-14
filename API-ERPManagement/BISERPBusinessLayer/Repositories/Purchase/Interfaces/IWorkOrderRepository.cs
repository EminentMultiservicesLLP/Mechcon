using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IWorkOrderRepository
    {
        WorkOrderEntities CreateWorkOrder(WorkOrderEntities entity, DBHelper dbhelper);
        bool UpdateWorkOrder(WorkOrderEntities entity, DBHelper dbhelper);
        IEnumerable<WorkOrderEntities> GetWorkOrder(int AuthorizationStatusId);
        WorkOrderEntities GetWorkOrderById(int Id);
        bool AuthCancelWorkOrder(WorkOrderEntities entity, DBHelper dbhelper);
        IEnumerable<WorkOrderEntities> WOforReport();
    }
}
