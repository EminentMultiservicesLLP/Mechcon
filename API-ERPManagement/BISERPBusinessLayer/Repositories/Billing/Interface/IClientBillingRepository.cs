using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Billing;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Repositories.Billing
{
   public interface IClientBillingRepository
    {
       ClientBillingEntity CreateClientBill(ClientBillingEntity entity);
       ClientRecieptEntiy RecieptClientBill(ClientRecieptEntiy entity);
       IEnumerable<ClientBillingEntity> GetClienBillNo(int branchId);
       List<ClientBillingDtEntity> GetClienBilldeatailById(int clientBillId);
       List<PaymentTermsMasterEntities> GetClienPaymentTermById(int clientBillId);
       IEnumerable<ClientRecieptEntiy> GetClienBillRecieptByBillingId(int clientBillId);
       IEnumerable<PayModeEntity> GetAllPaymentModes();
       IEnumerable<ClientRecieptDtEntity> GetClienRecieptdeatailById(int recieptId);
       ClientBillingEntity GetBillMasterBybillId(int clientBillId, int ReportFor);
       IEnumerable<ClientBillingEntity> GetSummary(int clientId, int projectId);
    }
}
