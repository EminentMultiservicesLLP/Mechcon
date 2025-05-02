using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Marketing.Models
{
    public class EnquiryAllocationModel
    {
        public int AllocationID { get; set; }
        public int? EnquiryID { get; set; }
        public string EnquiryNo { get; set; }
        public bool FeasibilityStudy { get; set; }
        public int? AllocatedTo { get; set; }
        public string AllocatedToName { get; set; }
        public DateTime? AllocationDate { get; set; }
        public string strAllocationDate { get; set; }
        public DateTime? QRDate { get; set; }
        public string strQRDate { get; set; }
        public string Comment { get; set; }
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
    }
    public class AllocationUserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}