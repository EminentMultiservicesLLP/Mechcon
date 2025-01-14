using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.SM_Reports.Models
{
    public class EnquiryRegisterReportModel
    {
        public string EnquiryNo { get; set; }
        public string strEnquiryDate { get; set; }
        public int EnquiryYear { get; set; }
        public string ClientName { get; set; }
        public string Location { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string Sector { get; set; }
        public string Product { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public decimal POBaseValue { get; set; }
        public string strOfferDate { get; set; }
        public string OfferRemark { get; set; }
        public string Status { get; set; }
        public string AllocatedTo { get; set; }
    }

    public class OrderBookReportModel
    {
        public string EnquiryNo { get; set; }
        public string strEnquiryDate { get; set; }
        public int EnquiryYear { get; set; }
        public string ClientName { get; set; }
        public string Location { get; set; }
        public string Source { get; set; }
        public string Product { get; set; }
        public string Type { get; set; }
        public string Sector { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string Month { get; set; }
        public string strOrderBookDate { get; set; }
        public string strPODate { get; set; }
        public string PONo { get; set; }
        public decimal POBaseValue { get; set; }
        public string strPIAdvSubmitDate { get; set; }
        public string strABGSubmitDate { get; set; }
        public string strPIABGAdvSubmitDate { get; set; }
        public string ProjectCode { get; set; }
        public string Status { get; set; }
        public string AllocatedTo { get; set; }
    }

    public class MonthlyTargetReportModel
    {
        public string Resource { get; set; }
        public string Month { get; set; }
        public decimal TargetValue { get; set; }
        public decimal OfferValue { get; set; }
        public decimal WonValue { get; set; }
    }

    public class SectorWiseSalesReportModel
    {
        public string Sector { get; set; }
        public decimal WonValue { get; set; }
        public decimal Percentage { get; set; }
    }

    public class LocationWiseSalesReportModel
    {
        public string Location { get; set; }
        public decimal WonValue { get; set; }
        public decimal Percentage { get; set; }
    }

    public class ProductWiseSalesReportModel
    {
        public string Product { get; set; }
        public decimal WonValue { get; set; }
        public decimal Percentage { get; set; }
    }
   public class WorkOrderRptModel
    {
        public int OrderBookID { get; set; }
        public string OrderBookNo { get; set; }
        public int? EnquiryID { get; set; }
        public int? OfferID { get; set; }
        public string EnquiryNo { get; set; }
        public string strEnquiryDate { get; set; }
        public string ClientName { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string PONo { get; set; }
        public DateTime? PODate { get; set; }
        public string strPODate { get; set; }
        public double? POBaseValue { get; set; }
        public decimal? BudgetValue { get; set; }
        public decimal? MaterialValue { get; set; }
        public decimal? ConversionValue { get; set; }
        public decimal? TransValue { get; set; }
        public decimal? ECValue { get; set; }
        public int? ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectDescription { get; set; }
        public string MaterialOfConstruction { get; set; }
        public string AreaOfInstallation { get; set; }
        public string TechnicalSpecification { get; set; }
        public string ScopeOfSupply { get; set; }
        public string Transport { get; set; }
        public string Packaging { get; set; }
        public string Insurance { get; set; }
        public string Supervision { get; set; }
        public double? LDCharges { get; set; }
        public string ContactAtSite { get; set; }
        public string ContactAtPurchase { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string strDeliveryDate { get; set; }      
        public string InsertedBy { get; set; }
        public string InsertedOn { get; set; }
        public int? Quantity { get; set; }
        public string InstAndComm { get; set; }
        public string GuaranteeType { get; set; }
        public decimal? SBG { get; set; }
        public decimal? ABG { get; set; }
        public decimal? PBG { get; set; }
        public string AdditionalContact { get; set; }
        public string BillAddress { get; set; }
        public string GSTIN { get; set; }
        public List<WORptPaymentTermDetails> PaymentTermList { get; set; }
        public List<WORptDeliveryTerm> DeliveryTermList { get; set; }
        public List<WORptOtherTerm> OtherTermList { get; set; }
        public List<WORptBasisTerm> BasisTermList { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyGST { get; set; }
        public string companyCIN { get; set; }
        public string companyEmail { get; set; }
    }
    public class WORptPaymentTermDetails
    {
        public bool State { get; set; }
        public int PayTermID { get; set; }
        public string PaymentTermCode { get; set; }
        public string PaymentTermDesc { get; set; }
    } 
    
    public class WORptDeliveryTerm
    {
        public bool State { get; set; }
        public int DelTermID { get; set; }
        public string DeliveryTermDesc { get; set; }
        public string DeliveryTermCode { get; set; }
    }
    public class WORptOtherTerm
    {
        public bool State { get; set; }
        public int OtherTermID { get; set; }
        public string OthersTermCode { get; set; }
        public string OthersTermDesc { get; set; }
    }
    public class WORptBasisTerm
    {
        public bool State { get; set; }
        public int BasisId { get; set; }
        public string BasisCode { get; set; }
        public string BasisDesc { get; set; }
    }

    public class FunctionalReportListModel
    {
        public int ReportId { get; set; }
        public  string Name { get; set;}
        public bool Deactive { get; set;  }
    }
    public class ZoneWiseSalesReportModel
    {
        public string Zone { get; set; }
        public decimal WonValue { get; set; }
        public decimal Percentage { get; set; }
    }

    public class PersonWiseSalesReportModel
    {
        public string AllocatedTo { get; set; }
        public int Count { get; set; }
        public decimal WonValue { get; set; }
        public decimal Percentage { get; set; }
    }

    public class StatusWiseSalesReportModel    {
        public string Status { get; set; }
        public int Count { get; set; }
        public decimal WonValue { get; set; }
        public decimal Percentage { get; set; }
    }
}