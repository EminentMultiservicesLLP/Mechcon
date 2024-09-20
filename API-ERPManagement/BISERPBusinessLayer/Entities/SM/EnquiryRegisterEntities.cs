using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.SM
{
    public class EnquiryRegisterEntities
    {
        public int EnquiryID { get; set; }

        public string EnquiryNo { get; set; }

        public DateTime? EnquiryDate { get; set; }
        public string strEnquiryDate { get; set; }

        public int? SourceID { get; set; }
        public string Source { get; set; }

        public int? ProductID { get; set; }
        public string Product { get; set; }

        public int? LocationID { get; set; }
        public string Location { get; set; }

        public string CustomerMailMsg { get; set; }

        public int ClientID { get; set; }
        public string ClientName { get; set; }

        public string Place { get; set; }

        public string ContactPerson { get; set; }

        public string ContactNo { get; set; }

        public string EmailID { get; set; }

        public int? StatusID { get; set; }
        public string Status { get; set; }

        public int? TypeID { get; set; }
        public string Type { get; set; }

        public int? SectorID { get; set; }
        public string Sector { get; set; }

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

    public class SourceEntities
    {
        public int SourceID { get; set; }
        public string Source { get; set; }
    }
    public class ProductEntities
    {
        public int ProductID { get; set; }
        public string Product { get; set; }
    }
    public class LocationEntities
    {
        public int LocationID { get; set; }
        public string Location { get; set; }
    }
    public class TypeEntities
    {
        public int TypeID { get; set; }
        public string Type { get; set; }
    }
    public class SectorEntities
    {
        public int SectorID { get; set; }
        public string Sector { get; set; }
    }
    public class ZoneEntities
    {
        public int ZoneID { get; set; }
        public string Zone { get; set; }
    }
    public class StatusEntities
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
