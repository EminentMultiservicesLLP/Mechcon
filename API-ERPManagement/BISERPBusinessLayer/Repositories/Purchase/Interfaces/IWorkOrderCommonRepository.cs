using BISERPBusinessLayer.Entities.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IWorkOrderCommonRepository
    {
        WorkOrderEntities CreateWorkOrder(WorkOrderEntities entity);
        bool UpdateWorkOrder(WorkOrderEntities entity);
        List<WODeliveryTermEntities> GetWODeliveryTerms(int Id);
        List<WOPaymentTermEntities> GetWOPaymenterms(int Id);
        List<WOOtherTermEntities> GetWOOtherterms(int Id);
        bool AuthCancelWorkOrder(WorkOrderEntities entity);
    }
}
