using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Billing;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPBusinessLayer.Repositories.Billing.Interface
{
   public interface IClientBillCancelRepository
    {
       bool CancelGeneratedBill(ClientBillingEntity entity);
       bool CancelRecieptBill(ClientRecieptEntiy entity);
       //IEnumerable<ClientRecieptDtEntity> GetClienRecieptdeatailById(int branchId);
       bool CheckReciept(ClientBillingEntity entity);
       IEnumerable<GRNEntity> GetTaxCredited(int projectId);
       IEnumerable<ClientBillingEntity> GetTaxPaid(int projectId);
    }
}
