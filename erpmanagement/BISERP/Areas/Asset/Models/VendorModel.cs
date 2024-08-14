using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Asset.Models
{
    public class VendorModel
    {
        public int VendorId { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Society { get; set; }
        public string Street { get; set; }
        public string landmark { get; set; }
        public int Village { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string Pin { get; set; }
        public string ContactPerson { get; set; }
        public string ContactDesignation { get; set; }
        public string Fax { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }
        public string Message { get; set; }
    }
}