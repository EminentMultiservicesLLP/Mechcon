using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.Store
{
    public class StoreQuery
    {
        public const string GetAllMaterialIndents = "sp_GetAllMaterialIndents";
        public const string GetStoreMaterialIndents = "sp_GetMaterialIndentsStore";
        public const string GetMaterialIndentsById = "sp_GetMaterialIndentsById";
        public const string InsertMaterialIndent = "sp_addINV_MaterialIndent";
        public const string UpdateMaterialIndent = "sp_UpdMaterialIndent";

        public const string GetAllMaterialIssueActive = "sp_GetAllMaterialIssueActive";
        public const string GetMaterialIssueById = "sp_GetMatertialIssueItemDetails";
        public const string GetVendorMaterialIssueById = "sp_GetVendorMatertialIssueById";
        public const string GetVendorMIDetailsById = "sp_GetVendorMIDetailsById";
        public const string GetAllVendorMaterialIssueAuth = "sp_GetAllVendorMaterialIssueAuth";
        //public const string InsertMaterialIssue = "sp_addINV_MaterialIssue";

        public const string GetMaterialIssueonAuthentication = "sp_GetMaterialIssueList";
        public const string GetMaterialIssueDetailsAuthentication = "sp_GetMaterialIssueDtlForAuth";
        public const string UpdateMaterialIssueAuthentication = "sp_UpdateINV_MaterialIssueDt";
        public const string AuthCancelMaterialIssueAuthentication = "sp_AuthCancelMatrialIssue";
        public const string rptMaterialIssueDetails = "sp_rptMaterialIssueDetails";
        public const string rptPendingMaterialIssue = "sp_rptPendingMaterialAcceptance";
        public const string rptCancelledMaterialAcceptance = "sp_rptCancelledMaterialAcceptance";
        public const string rptMaterialIssueItemwise = "sp_MaterialIssueItemwise";

        public const string GetMaterialIndentDetilsById = "sp_GetMaterialIndentDetilsById";
        public const string GetMaterialIndentDetailByIndent_Id = "sp_GetAllMaterialIndentDetailsByIndentId";
        public const string InsertMaterialIndentDetails = "sp_Ins_INV_PurchaseIndentDt";
        public const string InsertMaterialIndentDtl = "sp_Ins_INV_MaterialIndentDt";
        public const string UpdateMaterialIndentDtl = "sp_UpdMaterialIndentQty";
        public const string GetAllpendingMaterialIndents = "sp_GetAllpendingMaterialIndents";
        public const string rptMaterialIndentDetails = "sp_rptMaterialIndentDetails";
        public const string rptMaterialReturnDetails = "dbsp_rptMaterialReturnDetails";

        public const string AuthCancelPMaterialIndent = "sp_AuthCancelMaterialIndent";
        public const string AuthCancelVendorMI = "sp_AuthCancelVendorMI";
        public const string UpdVendorMIAuthQty = "sp_UpdVendorMIAuthQty";
        public const string VerifyCancelMaterialIndent = "sp_VerfyCancelMaterialIndent";

        public const string UpdMaterialIndentAuthQty = "sp_UpdMaterialIndentAuthQty";
        public const string UpdMaterialIndentVerifyQty = "sp_UpdMaterialIndentVerifyQty";
        public const string UpdpenMaterialIndent = "sp_UpdpenMaterialIndentVerifyQty";

        public const string DeleteIndentItem = "sp_DelIndentDetailItem";
        public const string GetAllAuthMaterialIssue = "sp_GetAuthMaterialIssue";

        public const string GetMaterialIssueDtlsById = "sp_GetMaterialIssueDtlsById";

        public const string GetAllAuthMaterialIndents = "sp_GetAllAuthMaterialIndents";
        public const string GetAuthMaterialIndentsForIssue = "sp_GetAuthMaterialIndentsForIssue";
        public const string GetAuthUnitMaterialIndents = "sp_GetAuthMaterialIndents";
        public const string GetAllAuthMaterialIndentsPending = "sp_GetPendingIndentsForPurchaseIndentDirect";


        public const string GetMaterialIndentForIssue = "sp_GetAllMaterialIndentDetailsForIssue";
        public const string GetMaterialIndentForAuth = "sp_GetAllMIDetailsForAuth";
        public const string GetAuthMIItemDetails = "sp_GetMIndentItemDetails";
        public const string InsertMaterialIssue = "sp_Ins_INV_MaterialIssue";
        public const string InsertMaterialIssueDetails = "sp_Ins_INV_MaterialIssueDt";
        public const string TransferMaterialIssueDetails = "sp_TransferMaterialIssueDt";
        public const string GetAllMaterialIssue = "sp_GetAllMaterialIssue";
        public const string GetStoreMaterialIssue = "sp_GetMaterialIssue";
        public const string StoreMaterialIssue = "sp_GetStoreMaterialIssue";
        public const string GetMaterialIssue = "sp_rptMaterialIssue";
       
        public const string AuthCancelMaterialIssue = "sp_AuthCancelMaterialIssue";
        public const string UpdMaterialIssueAuthQty = "sp_UpdMaterialIssueAuthQty";
        public const string AcceptMaterialIssue = "sp_MaterialIssueAcceptance";
        public const string GetpendingMIndentItemDetails = "sp_GetpendingMIndentItemDetails";
        public const string sp_GetallMaterialIssuerpt = "sp_GetallMaterialIssuerpt";
        public const string GetAllMaterialIssueFileDownload = "sp_GetAllMaterialIssueFileDownload";

        public const string GetIssuenoforReturn = "sp_GetIssuenoforReturn";

        public const string InsertDiscrepancy = "sp_InsDiscrepancy";
        public const string InsertDiscrepancyDt = "sp_InsDiscrepancyDt";
        public const string UpdDiscrepancyAuthQty = "sp_UpdDiscrepancyAuthQty";
        public const string GetDiscrepancyDtlsById = "sp_GetDiscrepancyDtlsById";
        public const string GetAllDiscrepancy = "sp_GetAllDiscrepancy";

        public const string InsertAdjustmentVoucher = "sp_InsAdjustmentVoucher";
        public const string InsertAdjustmentVoucherDt = "sp_InsAdjustmentVoucherDt";
        public const string GetAllAdjustmentVoucherRpt = "sp_TRA_GetAllAdjustmentVoucherRpt";

        public const string GetAllGRN = "sp_GetGRNMaster";
        public const string GetAllGRNDetails = "sp_GetGRNDetails"; 
        public const string InsertGRN = "sp_InsGRNMaster";
        public const string UpdateGRN = "sp_UpdGRNMaster";
        public const string InsertGRNDetail = "sp_InsGRNDetails";
        public const string InsertGRNTaxDetail = "sp_Ins_INVGRNTaxDetail";
        public const string GetAllGrnNo = "sp_GetAllGrnNo";

        public const string GRNSummaryReport = "sp_rptGRNSummary";
        public const string GRNDetailReport = "sp_rptGRNDetails";
        public const string rptGRNCancelled = "sp_rptGRNCancelled";
        public const string rptGRNItemWise = "sp_rptGRNItemWise";
        public const string GRNforReport = "sp_GRNforRpt";

        public const string GetGRNVendor = "sp_GetGRNVendor";
        public const string GetGRNVendorDetails = "sp_GetGRNVendorDetails";

        public const string InsertGRNVendor = "sp_InsGRNVendor";
        public const string InsertGRNVendorDetail = "sp_InsGRNVendorDetails";
        public const string InsertGRNTaxVendorDetail = "sp_Ins_INVGRNTaxVendorDetail";
        public const string InsertGRNVendorItemDetails = "sp_InsGRNVendorItemDetails";
        public const string AuthCancelGRNVendor = "sp_AuthCancelGRNVendor";
        public const string AuthCancelGRNVendorDetails = "sp_AuthGRNVendorDetails";

        public const string GetMaterialReturnIssueDtlsById = "sp_GetMaterialIssueDetailforReturn";
        public const string GetMaterialReturnIssueDt = "sp_GetMaterialDetailforReturn";

        public const string AuthCancelGRN = "sp_AuthCancelGRN";
        public const string AuthCancelGRNDetails = "sp_UpdINV_GRNDetailsforAuth";

        public const string InsMaterialReturnMaster = "sp_InsMaterialReturnMaster";
        public const string InsMaterialReturnDetail = "sp_InsMaterialReturnDetail";
        public const string GetAllMaterialReturn = "sp_GetAllMaterialReturn";
        public const string GetMaterialReturnById = "sp_GetMaterialReturnById";
        //public const string GetMaterialReturnDTById = "sp_GetMaterialReturnDTById";
        public const string GetMaterialReturnDTById = "sp_GetMaterialReturnDetilsById";
        public const string UpdMaterialRrturnAuth = "sp_UpdMaterialReturnAuth";
        public const string AllMaterialReturn = "dbsp_AllMaterialReturn";
        public const string GetAllMaterialIssueNO = "sp_GetAllMaterialIssueNO";

        public const string GetGrnNoforPurReturnStore = "sp_GetGrnNoforPurReturnStore";
        public const string GetGrnDtlForPurchaseReturn = "sp_GetGrnDtlForPurchaseReturn";
        public const string InsPurchaseReturnMaster = "sp_UpdatePurchaseReturnMaster";
        public const string InsPurchaseReturnDetail = "sp_UpdatePurchaseReturnDetail";
        public const string GetPurchaseReturn = "sp_GetPurchaseReturn";
        public const string GetPurchaseReturnDt = "sp_GetPurchaseReturnDt";
        public const string UpdPurchaseReturnAuth = "sp_UpdPurchaseReturnAuth";

        public const string UpdateOpeningBalance = "sp_UpdateOpeningBalance";
        public const string Ins_INV_ItemLimits = "sp_Ins_INV_ItemLimits";
        public const string GetItemLimits = "sp_GetItemLimits";

        public const string GetStockDetails = "sp_GetStockDetails";
        public const string StoreStockSummary = "sp_rptStoreStockSummary";
        public const string StoreStockDetails = "sp_rptStoreStockDetails";

        public const string GetStoreWisestockDataByID = "sp_GetStockRegister";
        public const string GetStoreItemWisestockDataByID = "sp_GetAllStockBalance";

        public const string StorestockReport = "sp_rptStoreStockRegister";
        public const string StockConsumptionReport = "sp_rptItemConsumption";
        public const string ExpiryRegisterReport = "sp_rptExpiryRegister";
        public const string StockEvaluationReport = "sp_GetItemValuationReport";
        public const string GetINV_ItemforConsume = "sp_GetINV_ItemforConsume";
        public const string InsINVStoreConsumptionMaster = "sp_InsINVStoreConsumptionMaster";
        public const string InsINV_StoreConsumptionDetail = "sp_InsINV_StoreConsumptionDetail";
        public const string GetAllConsumptionNO = "sp_GetAllConsumptionNO";
        public const string GetAllConsumptiondt = "sp_GetAllConsumptiondt";
        public const string InsINV_StoreConsumptioncancel = "sp_InsINV_StoreConsumptioncancel";

        public const string GetPendentIndentDetails = "sp_GetAllpendingMaterialIndents";

      //  public const string UpdMaterialIndentAuthQty = "sp_UpdMaterialIndentAuthQty";

        public const string CheckForValidation = "sp_CheckValidMaterialIndent";

        public const string InsINVStockDisposeMaster = "sp_InsINVStockDisposeMaster";
        public const string InsINV_StockDisposeDetail = "sp_InsINV_StockDisposeDetail";
        public const string GetAllStockDisposeRpt = "sp_TRA_GetAllStockDisposeRpt";

        public const string GetAllSupplierBill = "sp_GetAllSupplierBill";
        public const string GetAllSupplierBillDetails = "sp_GetAllSupplierBillDetails";

        public const string GetallSupplierListGrn = "sp_GetallSupplierListGrn";
        public const string GetallSupplierListGrnDT = "sp_GetallSupplierListGrnDT";

        public const string InsertSupplierBillPayment = "dbsp_Ins_INV_SupplierBillPayment";

        public const string GetNotificationQuantity = "sp_GetNotificationQuantity";
        public const string InsertPMaterialIndent = "dbsp_InsPurchaseMaterialIndent";
        public const string InsertRequestStatus = "dbsp_InsRequestStatus";
        public const string UpdateMIndentRequestStatus = "dbsp_UpdMIndentRequestStatus";
        public const string UpdateMIssueRequestStatus = "dbsp_UpdMIssueRequestStatus";
        public const string UpdateMIssueAuthRequestStatus = "dbsp_UpdMIssueAuthRequestStatus";
        public const string UpdatePMIndentRequestStatus = "dbsp_UpdPMIndentRequestStatus";
        public const string UpdatePIAuthorizeRequestStatus = "dbsp_UpdPIAuthorizeRequestStatus";
        public const string UpdatePORequestStatus = "dbsp_UpdPORequestStatus";
        public const string UpdatePOAuthRequestStatus = "dbsp_UpdPOAuthRequestStatus";
        public const string UpdateGRNRequestStatus = "dbsp_UpdGRNRequestStatus";
        public const string UpdateGRNAuthRequestStatus = "dbsp_UpdGRNAuthRequestStatus";
        public const string UpdateMRRequestStatus = "dbsp_UpdMRRequestStatus";
        public const string UpdateMRAuthRequestStatus = "dbsp_UpdMRAuthRequestStatus";
        public const string RequestStatusReport = "dbsp_IndentflowReport";

        public const string rptMaterialIssueDetailsAllBranch = "sp_rptMaterialIssueDetailsAllBranch";

        /*********** Dashboard queries for Store **********/
        public const string GetDSBDStockValuationSummaryAllBranchWise = "dbsp_DSBD_GetStockValuationSummaryBranchWise";
        public const string GetDSBDStockQtySummaryAllBranchWise = "dbsp_DSBD_GetStockQtySummaryBranchWise";
        public const string GetDSBDGuardIssueSummaryAllBranchWise = "dbsp_DSBD_GetGuardIssueSummaryBranchWise";

        public const string InsertVendorMaterialIssue = "sp_InsVendorMaterialIssue";
        public const string InsertVendorMaterialIssueDetails = "sp_InsVendorMaterialIssueDt";
        public const string GetVendorMaterialIssue = "sp_GetVendorMaterialIssue";
        public const string GetAuthVendorMaterialForGRNVendor = "dbsp_GetAuthVendorMaterialForGRNVendor";
        public const string RptPendingGrnItemWise = "dbsp_rptPendingGrnItemWise";
        public const string GetMasterReport = "sp_GetMasterReport";

        public const string GetGRNCounts = "[dbsp_DSBD_GetgrnCount]";
        public const string GetMIndentCount = "[dbsp_DSBD_GetMaterialIndentCount]";
        public const string GetMIssueCount = "[dbsp_DSBD_GetMaterialIssueCount]";
        public const string GetVIssueCount = "[dbsp_DSBD_GetVendorIssueCount]";
        public const string GetMReturnCount = "[dbsp_DSBD_GetMaterialReturnCount]";
        public const string GetStockDisposeCount = "[dbsp_DSBD_GetStockDisposeCount]";
        public const string GetStockAdjustmentCount = "[dbsp_DSBD_GetStockAdjustmentCount]";
        public const string GetOpeningBalanceCount = "[dbsp_DSBD_GetOpeningBalanceCount]";
        public const string SendMailStatusOfDashBoard = "[dbsp_SendMailStatusOfDashBoard]";

        public const string GetProjectBudgetConclusion = "[dbsp_GetProjectBudgetConclusion]";
    }
}
        