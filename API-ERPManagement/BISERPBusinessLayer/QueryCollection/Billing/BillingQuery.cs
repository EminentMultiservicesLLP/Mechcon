using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.Billing
{
    class BillingQuery
    {
        public const string CreateClientBillMst = "dbsp_Inv_InsUpd_ClientBill";
        public const string CreateClientBillDt = "dbsp_Inv_InsUpd_ClientBillDt";
        public const string GetClienBillNo = "dbsp_GetClienBillNo";
        public const string GetClienBilldeatailById = "dbsp_GetClienBilldetailById";
        public const string GetAllPaymentModes = "dbsp_GetGetAllPaymentModes";
        public const string RecieptClientBillMst = "dbsp_Inv_InsUpd_ClientBillReciept";
        public const string RecieptClientBillDtl = "dbsp_Inv_InsUpd_ClientBillRecieptDtl";
        public const string GetClienBillRecieptByBillingId = "dbsp_GetClienBillRecieptByBillingId";
        public const string CancelGeneratedBill = "dbsp_CancelClientGeneratedBill";
        public const string CancelRecieptBill = "dbsp_CancelClientRecieptBill";
        public const string GetClienRecieptdeatailById = "dbsp_GetClienRecieptdeatailById";
        public const string GetPoBySupplierId = "dbsp_GetPoBySupplierId";
        public const string GetGRNbyPOId = "dbsp_GetGRNbyPOId";
        public const string GetSummarizedBill = "dbsp_GetSummarizedBill";
        public const string CancelSupplierBill = "dbsp_CancelSupplierBill";
        public const string GetAllGRNForSupplier = "dbsp_GetAllGRNForSupplier";
        public const string CreateSupplierBillMst = "dbsp_InsUpd_SupplierBilling";
        public const string CreateSupplierBillDt = "dbsp_InsUpd_SupplierBillingDtl";
        public const string GetGRNbyVendorId = "dbsp_GetGRNbyVendorId";
        public const string CreateVendorBillMst = "dbsp_InsUpd_VendorBilling";
        public const string CreateVendorBillDt = "dbsp_InsUpd_VendorBillingDtl";
        public const string InsertClientPaymentTerm = "dbsp_Ins_InsertClientPaymentTerm";
        public const string GetClienPaymentTermById = "dbsp_GetClientPaymentTermById";
        public const string GetBillMasterBybillId = "dbsp_GetBillMasterBybillId";
        public const string GetVendorSummarizedBill = "dbsp_GetVendorSummarizedBill";
        public const string CancelVendorBill = "dbsp_CancelVendorBill";
        public const string GetSummary = "dbsp_GetSummary";
        public const string CheckReciept = "dbsp_CheckReciept";
        public const string GetTaxCredited = "dbsp_GetTaxCredited";
        public const string GetTaxPaid = "dbsp_GetTaxPaid";
        public const string getGRNbySupplierId = "dbsp_getGRNbySupplierId";
    }
}
