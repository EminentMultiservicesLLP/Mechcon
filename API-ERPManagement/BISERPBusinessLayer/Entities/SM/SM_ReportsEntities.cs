using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.SM
{
    public class EnquiryRegisterReportEntities
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

    public class OrderBookReportEntities
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

    public class MonthlyTargetReportEntities
    {
        public string Resource { get; set; }
        public string Month { get; set; }
        public decimal TargetValue { get; set; }
        public decimal OfferValue { get; set; }
        public decimal WonValue { get; set; }
    }

    public class SectorWiseSalesReportEntities
    {
        public string Sector { get; set; }
        public decimal WonValue { get; set; }
        public decimal Percentage { get; set; }
    }

    public class LocationWiseSalesReportEntities
    {
        public string Location { get; set; }
        public decimal WonValue { get; set; }
        public decimal Percentage { get; set; }
    }

    public class ProductWiseSalesReportEntities
    {
        public string Product { get; set; }
        public decimal WonValue { get; set; }
        public decimal Percentage { get; set; }
    }
}
