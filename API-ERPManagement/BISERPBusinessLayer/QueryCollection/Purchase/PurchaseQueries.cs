using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.Purchase
{
    public class PurchaseQueries
    {
        public const string GetPurchaseIndentDetilsById = "sp_GetAllPurchaseIndentDetailById";
        public const string GetPurchaseIndentDetailByIndentId = "sp_GetAllPurchaseIndentDetailsByIndentId";
        public const string InsertPurchaseIndentDetails = "sp_Ins_INV_PurchaseIndentDt";
        //public const string UpdateOthersMasterById = "sp_InsUpd_INV_MST_OtherTerms";

        public const string GetAllPurchaseIndent = "sp_GetAllPurchaseIndent";
        public const string GetAllPurchaseIndentforVerification = "sp_GetPIforVerification";
        public const string GetAllPurchaseIndentforAuthorization = "sp_GetPIforAuthorization";
        public const string GetAuthorizedPIndent = "sp_GetAuthorizedPIndent";
        public const string GetPurchaseIndentById = "sp_GetAllPurchaseIndentById";
        public const string InsertPurchaseIndent = "sp_Ins_INV_PurchaseIndent";
        public const string UpdatePurchaseIndent = "sp_UpdPurchaseIndent";

        public const string InsertPurchaseIndentDtl = "sp_Ins_INV_PurchaseIndentDt";
        //public const string UpdateOthersMasterById = "sp_InsUpd_INV_MST_OtherTerms";
        public const string GetPurchaseIndentForReport = "sp_GetPurchaseIndentForRpt";

        public const string AuthCancelPurchaseIndent = "sp_AuthCancelPurchaseIndent";
        public const string UpdPurchaseIndentAuthQty = "sp_UpdPurchaseIndentAuthQty";
        public const string UpdPurchaseIndentQty = "sp_UpdPurchaseIndentQty";

        public const string InsertPurchaseOrder = "sp_Ins_INV_POMaster";
        public const string UpdatePurchaseOrder = "sp_UpdPOMaster";
        public const string InsertPurchaseOrderDetails = "sp_Ins_INV_PODetails";
        public const string InsertPOTaxDetails = "sp_Ins_INV_POTaxDetail";
        public const string UpdatePurchaseOrderDetails = "sp_INV_MST_UpdatePODetails";

        public const string InsertPODeliveryTerm = "sp_Ins_INV_PODeliveryTerms";
        public const string InsertPOOtherTerm = "sp_Ins_INV_POOtherTerms";
        public const string InsertPOPaymentTerm = "sp_Ins_INV_POPayTerms";

        public const string GetPODeliveryTermByPOID = "sp_GetPODeliveryTermByPOID";
        public const string GetPOPaymentTermByPOID = "sp_GetPOPaymentTermByPOID";
        public const string GetPOOtherTermByPOID = "sp_GetPOOtherTermByPOID";

        public const string GetAllPurchaseOrder = "sp_GetAllPurchaseOrder";  
        public const string GetAllPurchaseOrderById = "sp_GetPOById";
        public const string GetPODetailsByPOId = "sp_GetPODetailsByPOId";
        public const string BeforePOAuth = "sp_BeforePOAuth";
        public const string AuthCancelPurchaseOrder = "sp_AuthCancelPurchaseOrder";
        public const string PurchaseOrderAmendment = "sp_PurchaseOrderAmendment";
        public const string PurchaseOrderForReport = "sp_PurchaseOrderForRpt";


        public const string DeleteIndentItem = "sp_DelIndentDetail";
        public const string GetPoTaxByPoid = "dbsp_GetPOTaxByPOID";
        public const string RptPurchaseOrder = "dbsp_GetPOTaxByPOID";
        public const string InsertPoBasis = "sp_Ins_INV_POBasis";
        public const string InsertPoInspection = "sp_Ins_INV_POInspection";
        public const string GetPoBasisByPoid = "sp_GetPOBasisByPOID";
        public const string GetPoInspectionByPoid = "sp_GetPOInspectionByPOID";

        public const string GetUnAuthorizationPo = "sp_GetAllUnAuthorizedPurchaseOrder";
        public const string PurchaseOrderForGrn = "sp_GetPurchaseOrderForGrn";

        public const string GetPendintMaterialRequest = "DBSP_GetPendintMaterialRequest";
        public const string SavePoAmendment = "dbsp_SavePoAmendment";
        public const string AmmendPoDetail = "dbsp_AmmendPoDetail";
        public const string GetPoForAmmendmet = "dbsp_GetPoForAmmendmet";
        public const string SaveIndentTemplate = "dbsp_SaveIndentTemplate";
        public const string SaveIndentTemplateDetails = "dbsp_SaveIndentTemplateDetails";
        public const string GetAllIndentTemplate = "dbsp_GetAllIndentTemplate";
        public const string GetIndentTemplateforId = "dbsp_GetIndentTemplateforId";
        public const string PurchaseOrderClose = "dbsp_PurchaseOrderClose";
        public const string grnVSpoitemcomparison = "dbsp_grnVSpoitemcomparison";
        public const string GetPIRemarkLibrary = "dbsp_GetPIRemarkLibrary";

        /////////////////////////////////RFQ////////////////////////////////////

        public const string InsertRequestForQuote = "sp_Ins_INV_RequestForQuote";
        public const string RequestForQuoteIndentDtl = "sp_Ins_INV_RequestForQuoteDt";
        public const string UpdateRequestForQuote = "sp_UpdRequestForQuote";
        public const string InsertRFQDeliveryTerms = "sp_Ins_INV_RFQDeliveryTerms";
        public const string InsertRFQPaymentTerms = "sp_Ins_INV_RFQPayTerms";
        public const string GetAllRequestForQuote = "sp_GetAllRequestForQuote";
        public const string GetRequestForQuoteById = "sp_GetRequestForQuoteById";
        public const string GetRequestForQuoteDetailById = "sp_GetRequestForQuoteDetailById";
        public const string GetRFQDeliveryTermByID = "sp_GetRFQDeliveryTermByID";
        public const string GetRFQPaymentTermByID = "sp_GetRFQPaymentTermByID";
        public const string AuthCancelRequestForQuote = "sp_AuthCancelRequestForQuote";
        public const string UpdateRequestForQuoteAuthQty = "sp_UpdateRequestForQuoteAuthQty";
        public const string GetAuthorizedRequestForQuote = "sp_GetAuthorizedRequestForQuote";
        public const string GetRFQforReport = "sp_GetRequestForQuoteForRpt";
        public const string AutoRFQGeneration = "sp_AutoRFQGeneration";

        /////////////////////////////////WO////////////////////////////////////

        public const string InsertWorkOrder = "sp_Ins_INV_WorkOrder";
        public const string WorkOrderIndentDtl = "sp_Ins_INV_WorkOrderDt";
        public const string UpdateWorkOrder = "sp_UpdWorkOrder";
        public const string InsertWODeliveryTerms = "sp_Ins_INV_WODeliveryTerms";
        public const string InsertWOPaymentTerms = "sp_Ins_INV_WOPayTerms";
        public const string InsertWOOtherTerms = "sp_Ins_INV_WOOtherTerms";
        public const string GetWorkOrder = "sp_GetWorkOrder";
        public const string GetWorkOrderById = "sp_GetWorkOrderById";
        public const string GetWorkOrderDetailById = "sp_GetWorkOrderDetailById";
        public const string GetWODeliveryTermByID = "sp_GetWODeliveryTermByID";
        public const string GetWOPaymentTermByID = "sp_GetWOPaymentTermByID";
        public const string GetWOOtherTermByID = "sp_GetWOOtherTermByID";
        public const string AuthCancelWorkOrder = "sp_AuthCancelWorkOrder";
        public const string UpdateWorkOrderAuthQty = "sp_UpdateWorkOrderAuthQty";
        public const string GetWOforReport = "sp_GetWorkOrderForRpt";
    }
}
