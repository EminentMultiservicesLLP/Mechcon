using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.SM
{
    class MarketingQueries
    {
        //ResourceTarget
        public const string GetFinancialYear = "sp_SM_GetFinancialYear";
        public const string SaveResourceTargetDetail = "sp_SM_SaveResourceTargetDetail";
        public const string GetResourceTargetDetail = "sp_SM_GetResourceTargetDetail";


        //EnquiryRegister
        public const string GetSources = "sp_SM_GetSources";
        public const string GetProducts = "sp_SM_GetProducts";
        public const string GetLocations = "sp_SM_GetLocations";
        public const string GetTypes = "sp_SM_GetTypes";
        public const string GetSectors = "sp_SM_GetSectors";
        public const string GetZones = "sp_SM_GetZones";
        public const string GetEnqStatus = "sp_SM_GetEnqStatus";
        public const string GetStatus = "sp_SM_GetStatus";
        public const string SaveEnquiry = "sp_SM_SaveEnquiryRegister";
        public const string GetEnquiry = "sp_SM_GetEnquiry";
        public const string GetEnquiryDetails = "sp_SM_GetEnquiryDetails";
        public const string GetEnqRegFollowUp = "sp_SM_GetEnqRegFollowUp";

        //EnquiryAllocation
        public const string GetEnqForAllocation = "sp_SM_GetEnqForAllocation";
        public const string SaveAllocation = "sp_SM_SaveEnquiryAllocation";
        public const string GetAllocation = "sp_SM_GetEnquiryAllocation";

        //OfferRegister
        public const string GetEnqForOffer = "sp_SM_GetEnqForOffer";
        public const string GetOfferID = "sp_SM_GetOfferID";
        public const string SaveOffer = "sp_SM_SaveOfferRegister";
        public const string GetOffer = "sp_SM_GetOfferRegister";
        public const string GetOfferDetails = "sp_SM_GetOfferDetails";

        //OrderBook
        public const string GetEnqForOrderBook = "sp_SM_GetEnqForOrderBook";
        public const string GetConsignee = "sp_SM_GetConsignee";
        public const string GetFinalOffer = "sp_SM_GetFinalOffer";
        public const string SaveOrderBook = "sp_SM_SaveOrderBook";
        public const string GetOrderBook = "sp_SM_GetOrderBook";
        public const string GetOrderBookProjectTC = "sp_SM_GetOrderBookProjectTC";
        public const string GetOrderBookPaymentTerms = "sp_SM_GetOrderBookPaymentTerms";
        public const string GetOrderBookDeliveryTerms = "sp_SM_GetOrderBookDeliveryTerms";
        public const string GetOrderBookOtherTerms = "sp_SM_GetOrderBookOtherTerms";
        public const string GetOrderBookBasisTerms = "sp_SM_GetOrderBookBasisTerms";
        public const string GetIncoTerm = "sp_SM_GetIncoTerms";
        public const string GetOBOtherDetails = "sp_SM_GetOBOtherDetails";

        //WorkOrder
        public const string GetEnqForWorkOrder = "sp_SM_GetEnqForWorkOrder";
        public const string SaveWorkOrder = "sp_SM_SaveWorkOrder";
        public const string GetWorkOrder = "sp_SM_GetWorkOrder";
        public const string GetWorkOrderDetails = "sp_SM_GetWorkOrderDetails";
        public const string GetWOOtherDetails = "sp_SM_GetWOOtherDetails";

        //Reports
        public const string GetEnquiryRegisterRpt = "sp_SM_GetEnquiryRegisterRpt";
        public const string GetOrderBookRpt = "sp_SM_GetOrderBookRpt";
        public const string GetResourceWiseMonthlyStatusRpt = "sp_SM_GetResourceWiseMonthlyStatusRpt";
        public const string GetSectorWiseSalesRpt = "sp_SM_GetSectorWiseSalesRpt";
        public const string GetLocationWiseSalesRpt = "sp_SM_GetLocationWiseSalesRpt";
        public const string GetProductWiseSalesRpt = "sp_SM_GetProductWiseSalesRpt";
        public const string GetWorkOrderReport = "sp_SM_GetWrokOrderReport";

    }
}
