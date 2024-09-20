using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.SM
{
    public class OrderBookEntities
    {
        public int OrderBookID { get; set; }
        public int? EnquiryID { get; set; }
        public int? OfferID { get; set; }
        public string EnquiryNo { get; set; }
        public string strEnquiryDate { get; set; }
        public string ClientName { get; set; }
        public string PONo { get; set; }
        public DateTime? PODate { get; set; }
        public string strPODate { get; set; }
        public double? POBaseValue { get; set; }
        public DateTime? PIAdvSubmitDate { get; set; }
        public string strPIAdvSubmitDate { get; set; }
        public DateTime? ABGSubmitDate { get; set; }
        public string strABGSubmitDate { get; set; }
        public DateTime? PIABGAdvSubmitDate { get; set; }
        public string strPIABGAdvSubmitDate { get; set; }
        public int? InsertedBy { get; set; }
        public DateTime? InsertedON { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedIPAddress { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedIPAddress { get; set; }
        public bool Deactive { get; set; }
        public int ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public int IncoTermID { get; set; }
        public string IncoTermCode { get; set; }
        public List<ProjectTCDetails> ProjectTCList { get; set; }
        public List<PaymentTermDetails> PaymentTermList { get; set; }
        public List<DeliveryTermDetails> DeliveryTermList { get; set; }
    }
    public class ProjectTCDetails
    {
        public bool State { get; set; }
        public int ProjectTCID { get; set; }
        public string ProjectTCCode { get; set; }
        public string ProjectTCDesc { get; set; }
    }
    public class PaymentTermDetails
    {
        public bool State { get; set; }
        public int PayTermID { get; set; }
        public string PaymentTermCode { get; set; }
        public string PaymentTermDesc { get; set; }
    }
    public class DeliveryTermDetails
    {
        public bool State { get; set; }
        public int DelTermID { get; set; }
        public string DeliveryTermCode { get; set; }
        public string DeliveryTermDesc { get; set; }
    }
    public class IncoTermEntities
    {
        public int IncoTermID { get; set; }
        public string IncoTermCode { get; set; }
        public string IncoTermName { get; set; }
        public bool Deactive { get; set; }
    }
}
