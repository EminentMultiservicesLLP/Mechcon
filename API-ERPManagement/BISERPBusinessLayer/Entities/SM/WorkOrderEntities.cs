using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.SM
{
    public class WorkOrderEntities
    {
        public int WorkOrderID { get; set; }
        public string WorkOrderNo { get; set; }
        public int? EnquiryID { get; set; }
        public string EnquiryNo { get; set; }
        public string strEnquiryDate { get; set; }
        public string ClientName { get; set; }
        public int? OrderBookID { get; set; }
        public int? ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectDescription { get; set; }
        public string MaterialOfConstruction { get; set; }
        public string AreaOfInstallation { get; set; }
        public int? BilledTo { get; set; }
        public string BilledToName { get; set; }
        public string ProjectLocation { get; set; }
        public string TechnicalSpecification { get; set; }
        public string ScopeOfSupply { get; set; }
        public string PONo { get; set; }
        public DateTime? PODate { get; set; }
        public string strPODate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string strDeliveryDate { get; set; }
        public string Transport { get; set; }
        public string Packaging { get; set; }
        public string Insurance { get; set; }
        public string Supervision { get; set; }
        public double? LDCharges { get; set; }
        public string ContactAtSite { get; set; }
        public string ContactAtPurchase { get; set; }
        public DateTime? PIAdvanceDate { get; set; }
        public string strPIAdvanceDate { get; set; }
        public DateTime? PIPreDispatchDate { get; set; }
        public string strPIPreDispatchDate { get; set; }
        public DateTime? DispatchDate1 { get; set; }
        public string strDispatchDate1 { get; set; }
        public DateTime? DispatchDate2 { get; set; }
        public string strDispatchDate2 { get; set; }
        public DateTime? DispatchDate3 { get; set; }
        public string strDispatchDate3 { get; set; }
        public DateTime? DispatchDate4 { get; set; }
        public string strDispatchDate4 { get; set; }
        public DateTime? DispatchDate5 { get; set; }
        public string strDispatchDate5 { get; set; }
        public string BriefTechnicalSpecification { get; set; }
        public int? InsertedBy { get; set; }
        public DateTime? InsertedOn { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedIPAddress { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedIPAddress { get; set; }
        public bool Deactive { get; set; }
        public List<WorkOrderOtherDetail> WOOtherDetails { get; set; }
    }
    public class WorkOrderOtherDetail
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
