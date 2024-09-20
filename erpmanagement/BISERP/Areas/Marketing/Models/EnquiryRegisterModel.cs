using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BISERP.Areas.Marketing.Models
{
    public class EnquiryRegisterModel
    {
        [Display(Name = "Enquiry ID")]
        public int EnquiryID { get; set; }

        [Display(Name = "Enquiry Number")]
        public string EnquiryNo { get; set; }

        [Display(Name = "Enquiry Date")]
        public DateTime? EnquiryDate { get; set; }
        public string strEnquiryDate { get; set; }

        [Display(Name = "Source")]
        public int? SourceID { get; set; }
        public string Source { get; set; }

        [Display(Name = "Product")]
        public int? ProductID { get; set; }
        public string Product { get; set; }

        [Display(Name = "Location")]
        public int? LocationID { get; set; }
        public string Location { get; set; }

        [Display(Name = "Customer Message")]
        public string CustomerMailMsg { get; set; }

        [Display(Name = "Client")]
        public int ClientID { get; set; }
        public string ClientName { get; set; }

        [Display(Name = "Place")]
        public string Place { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNo { get; set; }

        [Display(Name = "Email Address")]
        public string EmailID { get; set; }

        [Display(Name = "Status")]
        public int? StatusID { get; set; }
        public string Status { get; set; }

        [Display(Name = "Type")]
        public int? TypeID { get; set; }
        public string Type { get; set; }

        [Display(Name = "Sector")]
        public int? SectorID { get; set; }
        public string Sector { get; set; }

        [Display(Name = "Zone")]
        public int? ZoneID { get; set; }
        public string Zone { get; set; }
        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedMacID { get; set; }

        public string UpdatedMacName { get; set; }

        public string UpdatedIPAddress { get; set; }

        public int? InsertedBy { get; set; }

        public DateTime? InsertedON { get; set; }

        public string InsertedMacID { get; set; }

        public string InsertedMacName { get; set; }

        public string InsertedIPAddress { get; set; }

        public bool? Deactive { get; set; }

        public int OrderBookID { get; set; }
        public string AllocatedToName { get; set; }
    }
    public class SourceModel
    {
        public int SourceID { get; set; }
        public string Source { get; set; }
    }
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string Product { get; set; }
    }
    public class LocationModel
    {
        public int LocationID { get; set; }
        public string Location { get; set; }
    }
    public class TypeModel
    {
        public int TypeID { get; set; }
        public string Type { get; set; }
    }
    public class SectorModel
    {
        public int SectorID { get; set; }
        public string Sector { get; set; }
    }
    public class ZoneModel
    {
        public int ZoneID { get; set; }
        public string Zone { get; set; }
    }
    public class StatusModel
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
    public class EnqRegFollowUpDetails
    {
        public int FollowUpID { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string strFollowUpDate { get; set; }
        public int? EnquiryID { get; set; }
        public string EnquiryNo { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
        public string CustomerMailMsg { get; set; }
    }
}