using BISERP.Areas.Masters.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class MaterialIndentModel
    {
        [JsonProperty("Indent_Id")]
        public int Indent_Id { get; set; }

        [Display(Name = "MR No")]
        [Required(ErrorMessage = "Indent No is required.")]
        [JsonProperty("IndentNo")]
        public string IndentNo { get; set; }

        [Display(Name = "Priority")]
        [JsonProperty("Priority")]
        public Nullable<int> Priority { get; set; }
        public string Prioritystr { get; set;}
        [JsonProperty("Indent_User_Area")]
        public string Indent_User_Area { get; set; }

       
        [JsonProperty("Indent_User_SubArea")]
        public string Indent_User_SubArea { get; set; }

        [JsonProperty("Indent_FromStoreID")]
        public Nullable<int> Indent_FromStoreID { get; set; }

        [JsonProperty("Indent_ToStoreID")]
        public Nullable<int> Indent_ToStoreID { get; set; }

        [Display(Name = "Project")]
        [JsonProperty("Indent_FromStore")]
        public string Indent_FromStore { get; set; }

        [Display(Name = "To Store")]
        [JsonProperty("Indent_ToStore")]
        public string Indent_ToStore { get; set; }

        [Display(Name = "Item Type")]
        [JsonProperty("ItemCategoryId")]
        public int? ItemCategoryId { get; set; }
        public string ItemCategory { get; set; }

        [Display(Name = "MR Type")]
        [JsonProperty("Indent_Type")]
        public string Indent_Type { get; set; }

        [Display(Name = "MR Date")]
        [JsonProperty("Indent_Date")]
        public DateTime Indent_Date { get; set; }

        public string strIndentDate { get; set; }

        [Display(Name = "Type")]
        [JsonProperty("Type")]
        public string Type { get; set; }

       
        [JsonProperty("AuthorizedBy")]
        public Nullable<int> AuthorizedBy { get; set; }


        [Display(Name = "Status")]
        [JsonProperty("Authorized")]
        public string Authorized { get; set; }

        [Display(Name = "Remarks")]
        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

       
        [JsonProperty("Cancelledby")]
        public Nullable<int> Cancelledby { get; set; }

        [JsonProperty("CancelledOn")]
        public Nullable<System.DateTime> CancelledOn { get; set; }

        [JsonProperty("CancelReason")]
        public Nullable<int> CancelReason { get; set; }

        [JsonProperty("Cancelled")]
        public Nullable<bool> Cancelled { get; set; }

        [JsonProperty("BranchID")]
        public Nullable<int> BranchID { get; set; }

        [JsonProperty("PIStatus")]
        public Nullable<int> PIStatus { get; set; }

        [JsonProperty("PIUpdatedBy")]
        public Nullable<int> PIUpdatedBy { get; set; }

        [JsonProperty("PIUpdatedOn")]
        public Nullable<System.DateTime> PIUpdatedOn { get; set; }

        [JsonProperty("UpdatedMacName")]
        public string UpdatedMacName { get; set; }

        [JsonProperty("UpdatedMacID")]
        public string UpdatedMacID { get; set; }

        [JsonProperty("UpdatedIPAddress")]
        public string UpdatedIPAddress { get; set; }

        [JsonProperty("UpdatedBy")]
        public Nullable<int> UpdatedBy { get; set; }

        [JsonProperty("UpdatedOn")]
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        [JsonProperty("InsertedBy")]
        public Nullable<int> InsertedBy { get; set; }

        [JsonProperty("InsertedON")]
        public Nullable<System.DateTime> InsertedON { get; set; }

        [JsonProperty("InsertedMacName")]
        public string InsertedMacName { get; set; }

        [JsonProperty("InsertedMacID")]
        public string InsertedMacID { get; set; }

        [JsonProperty("InsertedIPAddress")]
        public string InsertedIPAddress { get; set; }

        [JsonProperty("Deactive")]
        public Nullable<bool> Deactive { get; set; }
        public List<MaterialIndentDtModel> Materialindentdt { get; set; }

        [JsonProperty("ItemName")]
        [Display(Name = "Item Name")]
        public List<ItemMasterModel> ItemName { get; set; }
        [JsonProperty("CancellationRemark")]
        public string CancellationRemark { get; set; }

        [Display(Name = "Cancellation Remark")]
        [JsonProperty("AuthorisedRemarks")]
        public string AuthorisedRemarks { get; set; }

        [JsonProperty("Verifiedby")]
        public Nullable<int> Verifiedby { get; set; }

        [JsonProperty("VerifiedOn")]
        public Nullable<System.DateTime> VerifiedOn { get; set; }

        [JsonProperty("Verified")]
        public Nullable<bool> Verified { get; set; }

        public int StatusId { get; set; }
        public string Status { get; set; }

        public string Message { get; set; }

        internal DataTable ModelToData(MaterialIndentModel indent)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Indent_Id");
            dt.Columns.Add(dc);

            dc = new DataColumn("IndentNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("FromStore");
            dt.Columns.Add(dc);

            dc = new DataColumn("ToStore");
            dt.Columns.Add(dc);

            dc = new DataColumn("Indent_Type");
            dt.Columns.Add(dc);

            dc = new DataColumn("Indent_Date");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemCode");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemName");
            dt.Columns.Add(dc);

            dc = new DataColumn("Item_Stock");
            dt.Columns.Add(dc);

            dc = new DataColumn("User_Quantity");
            dt.Columns.Add(dc);

            dc = new DataColumn("Authorised_Quantity");
            dt.Columns.Add(dc);

            int intIndentId = indent.Indent_Id;
            string IndentNo = indent.IndentNo;
            string IndentDate = Convert.ToDateTime(indent.Indent_Date).ToString("dd-MM-yyyy");
            string FromStore = indent.Indent_FromStore;
            string ToStore = indent.Indent_ToStore;
            string Indent_Type = indent.Indent_Type;
            foreach (var m in indent.Materialindentdt)
            {
                dt.Rows.Add(intIndentId, IndentNo, FromStore, ToStore, Indent_Type, IndentDate, m.ItemCode, m.ItemName, m.Item_Stock, m.User_Quantity, m.Authorised_Quantity);
            }
            return dt;
        }

        internal object ListToModel(List<MaterialIndentModel> indent)
        {

            var result = from m in indent
                         from mdt in m.Materialindentdt
                         select new
                         {
                             m.Indent_Id,
                             m.IndentNo,
                             FromStore = m.Indent_FromStore,
                             ToStore = m.Indent_ToStore,
                             m.Remarks,
                             m.Indent_Type,
                             m.Indent_Date,
                             mdt.ItemCode,
                             mdt.ItemName,
                             mdt.Item_Stock,
                             mdt.User_Quantity,
                             mdt.Authorised_Quantity,
                             mdt.MICancelledOn,
                             mdt.MICancelReason,
                             mdt.MICancelledbyUser
                         };
            return result;
        }

        public string InsertedByName { get; set; }
        public string UpdatedByName { get; set; }
        public string VerifiedByName { get; set; }
        public string AuthorizedByName { get; set; }
        public string CancelledByName { get; set; }
    }
}