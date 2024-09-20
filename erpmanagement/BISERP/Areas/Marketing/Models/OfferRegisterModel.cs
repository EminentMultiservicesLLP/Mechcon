using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Marketing.Models
{
    public class OfferRegisterModel
    {
        public int OfferRegisterID { get; set; }
        public int? EnquiryID { get; set; }
        public string EnquiryNo { get; set; }
        public string strEnquiryDate { get; set; }
        public string CustomerName { get; set; }
        public DateTime? QRDate { get; set; }
        public string strQRDate { get; set; }
        public string OfferRegisterComment { get; set; }
        public int? ProductID { get; set; }
        public int? StatusID { get; set; }
        public int? IncoTermID { get; set; }
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
        public List<OfferDetailModel> OfferDetails { get; set; }
    }
    public class OfferDetailModel
    {
        public int OfferID { get; set; }
        public int OfferRegisterID { get; set; }
        public string OfferNo { get; set; }
        public DateTime? OfferDate { get; set; }
        public string strOfferDate { get; set; }
        public string OfferRemark { get; set; }
        public double POBaseValue { get; set; } 
        public double GSTAmount { get; set; } 
        public DateTime? CustRespDate { get; set; }
        public string strCustRespDate { get; set; }
        public string CustRemark { get; set; }
        public bool Deactive { get; set; }
    }

}