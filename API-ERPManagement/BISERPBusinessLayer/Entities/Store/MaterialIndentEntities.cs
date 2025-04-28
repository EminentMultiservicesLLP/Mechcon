using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class MaterialIndentEntities
    {
        public int Indent_Id { get; set; }
        public string IndentNo { get; set; }
        public string Indent_User { get; set; }
        public string Indent_User_Area { get; set; }
        public string Indent_User_SubArea { get; set; }
        public Nullable<int> Indent_FromStoreID { get; set; }
        public Nullable<int> Indent_ToStoreID { get; set; }
        public Nullable<int> Priority { get; set; }
        public string Prioritystr { get; set; }
        public string Indent_FromStore { get; set; }
        public string Indent_ToStore { get; set; }
        public string Indent_Type { get; set; }
        public int? ItemCategoryId { get; set; }
        public string ItemCategory { get; set; }
        public Nullable<System.DateTime> Indent_Date { get; set; }
        public string Type { get; set; }
        public Nullable<int> AuthorizedBy { get; set; }
        public DateTime? AuthorisedOn { get; set; }
        public string strAuthorisedOn { get; set; }
        public bool Authorized { get; set; }

        public string Remarks { get; set; }
        public Nullable<int> Cancelledby { get; set; }
        public Nullable<System.DateTime> CancelledOn { get; set; }
        public Nullable<int> CancelReason { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> PIStatus { get; set; }
        public Nullable<int> PIUpdatedBy { get; set; }
        public Nullable<System.DateTime> PIUpdatedOn { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string strInsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public string AuthorisedRemarks { get; set; }
        public Nullable<bool> Deactive { get; set; }
        public Nullable<bool> IsUnitStore { get; set; }
        public string strIndentDate { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public List<MaterialIndentDetailsEntities> Materialindentdt { get; set; }
        public string InsertedByName { get; set; }
        public string UpdatedByName { get; set; }
        public string VerifiedByName { get; set; }
        public Nullable<System.DateTime> VerifiedOn { get; set; }
        public string strVerifiedOn { get; set; }
        public string AuthorizedByName { get; set; }
        public string CancelledByName { get; set; }
    }
}
