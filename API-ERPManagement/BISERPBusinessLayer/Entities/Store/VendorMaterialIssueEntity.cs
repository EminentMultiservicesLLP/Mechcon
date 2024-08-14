using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class VendorMaterialIssueEntity
    {
        public int IssueId { get; set; }
        public string IssueNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string strIssueDate { get; set; }
        public int Nature { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int VendorId { get; set; }
        public string Vendor { get; set; }
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
        public string ErrorMessage { get; set; }
        public List<VendorMaterialIssueDtEntity> VendorMaterialIssueDt { get; set; }
    }
}
