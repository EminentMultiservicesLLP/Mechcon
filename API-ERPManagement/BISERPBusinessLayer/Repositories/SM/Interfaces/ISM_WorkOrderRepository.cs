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
        IEnumerable<EnquiryRegisterEntities> GetEnqForWorkOrder(int UserID);
        WorkOrderEntities SaveWorkOrder(WorkOrderEntities model);
        IEnumerable<WorkOrderEntities> GetWorkOrder(int UserID);
        WorkOrderEntities GetWorkOrderDetails(int WorkOrderID);
        IEnumerable<WorkOrderOtherDetail> GetWOOtherDetails(int OfferRegisterID);
    }
}
