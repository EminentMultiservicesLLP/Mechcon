using BISERPBusinessLayer.Entities.Billing;
using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Billing.Interface
{
   public interface ISupplierBillingRepository
    {
       SupplierBillingEntity CreateSupplierBill(SupplierBillingEntity entity);
       IEnumerable<SupplierBillingEntity> GetPoBySupplierId(int supplierId, int vendorid);
       IEnumerable<SupplierBillingdtEntity> GetGRNbyPOId(int POId, int supplier, int vendor);
       IEnumerable<SupplierBillingdtEntity> GetAllGRNForSupplier();
       IEnumerable<SupplierBillingdtEntity> GetSummarizedBill(int GRNId);
       bool CancelSupplierBill(SupplierBillingdtEntity entity);
       VendorBillingEntity CreateVendorBill(VendorBillingEntity entity);
       IEnumerable<VendorBillingDtlEntity> GetGRNbyVendorId(int vendorId);
       IEnumerable<VendorBillingDtlEntity> GetVendorSummarizedBill(int GRNId);
       bool CancelVendorBill(VendorBillingDtlEntity entity);
       bool UpdateFileNamegrnBill(SupplierBillingdtEntity entity);
       IEnumerable<SupplierBillingdtEntity> getGRNbySupplierId(int supId);
    }
}
