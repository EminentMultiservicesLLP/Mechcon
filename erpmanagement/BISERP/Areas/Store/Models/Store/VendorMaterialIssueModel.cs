using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class VendorMaterialIssueModel
    {
        public int IssueId { get; set; }
        public string IssueNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string strIssueDate { get; set; }
        [Display(Name = "Nature")]
        public int Nature { get; set; }

        [Display(Name = "Store")]
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        [Display(Name = "Manufacturer")]
        public int ManufactureId { get; set; }
        public string Manufacturer { get; set; }
        public Nullable<bool> Authorised { get; set; }
        public int AuthorisedBy { get; set; }
        public DateTime AuthorisedDate { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public int CancelledBy { get; set; }
        public DateTime CancelledDate { get; set; }
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
        public Nullable<bool> Deactive { get; set; }
        public List<VendorMaterialIssueDtModel> VendorMaterialIssueDt { get; set; }
        
    }
}