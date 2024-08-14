using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class MaterialIssueModel
    {
        public int IssueId { get; set; }

        [Display(Name="Issue No")]
        public string IssueNo { get; set; }

        [Display(Name = "Issue Date")]
        public Nullable<DateTime> IssueDate { get; set; }
        public string strIssueDate { get; set; }
        public int? StoreId { get; set; }
        public string StoreName { get; set; }
        public int? Indent_Id { get; set; }

        [Display(Name = "IndentType")]
        public string IndentType { get; set; }
        [Display(Name = "RequestNo")]
        public string IndentNo { get; set; }        
        public string Notes { get; set; }
        public int? FromStoreId { get; set; }
        
        [Display(Name = "Project")]
        public string FromStore { get; set; }

        [Display(Name = "To Store")]
        public string ToStore { get; set; }
        public int? BranchID { get; set; }
        public int? IssuedBy { get; set; }
        public Nullable<bool> Authorised { get; set; }        
        public Nullable<int> AuthorisedBy { get; set; }
        public Nullable<System.DateTime> AuthorisedDate { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public Nullable<int> CancelledBy { get; set; }
        public Nullable<System.DateTime> CancelledDate { get; set; }

        public Nullable<bool> Accepted { get; set; }
        public Nullable<int> AcceptedBy { get; set; }
        public Nullable<System.DateTime> AcceptedDate { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public Nullable<bool> Deactive { get; set; }
        public List<MaterialIssueDetailModel> MaterialIssueDt { get; set; }
        public Nullable<DateTime> Indent_Date { get; set; }
        public string strIndentDate { get; set; }
      
     
        public DataTable ListModelToDataTable(List<MaterialIssueModel> issue)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("IssueId");
            dt.Columns.Add(dc);

            dc = new DataColumn("IndentNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("IssueNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("IssueDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Indent_Date");
            dt.Columns.Add(dc);

            dc = new DataColumn("FromStore");
            dt.Columns.Add(dc);

            dc = new DataColumn("ToStore");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemName");
            dt.Columns.Add(dc);

            dc = new DataColumn("BatchName");
            dt.Columns.Add(dc);

            dc = new DataColumn("IssuedQuantity");
            dt.Columns.Add(dc);

            dc = new DataColumn("AuthorisedQuantity");
            dt.Columns.Add(dc);

            dc = new DataColumn("MRP");
            dt.Columns.Add(dc);

            foreach (var model in issue)
            {
                int intIssueId = model.IssueId;
                string IndentNo = model.IndentNo;
                string IssueNo = model.IssueNo;
                DateTime? IndentDate = model.Indent_Date;
                DateTime? IssueDate = model.IssueDate;
                string FromStore = model.FromStore;
                string ToStore = model.ToStore;
                
                foreach (var m in model.MaterialIssueDt)
                {
                    dt.Rows.Add(intIssueId, IndentNo, IssueNo, IssueDate, IndentDate, FromStore, ToStore, m.ItemName, m.BatchName, m.IssuedQuantity, m.AuthorisedQuantity, m.MRP);
                }
            }
            return dt;
        }

        internal DataTable ModelToData(MaterialIssueModel issue)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("IssueId");
            dt.Columns.Add(dc);

            dc = new DataColumn("IssueNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("IndentNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("FromStore");
            dt.Columns.Add(dc);

            dc = new DataColumn("ToStore");
            dt.Columns.Add(dc);

            dc = new DataColumn("IssueDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("Indent_Date");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemCode");
            dt.Columns.Add(dc);

            dc = new DataColumn("ItemName");
            dt.Columns.Add(dc);

            dc = new DataColumn("BatchName");
            dt.Columns.Add(dc);

            dc = new DataColumn("IssuedQuantity");
            dt.Columns.Add(dc);

            dc = new DataColumn("MRP");
            dt.Columns.Add(dc);

            int intIssueId = issue.IssueId;
            string IssueNo = issue.IssueNo;
            string IssueDate = Convert.ToDateTime(issue.IssueDate).ToString("dd-MM-yyyy");
            string IndentNo = issue.IndentNo;
            DateTime? IndentDate = issue.Indent_Date;
            string FromStore = issue.FromStore;
            string ToStore = issue.StoreName;
            foreach (var m in issue.MaterialIssueDt)
            {
                dt.Rows.Add(intIssueId, IssueNo, IndentNo, FromStore, ToStore, IssueDate, IndentDate, m.ItemCode, m.ItemName, m.BatchName, m.IssuedQuantity,m.MRP);
            }
            return dt;
        }

        internal object ListToModel(List<MaterialIssueModel> issue)
        {
            var result = from m in issue
                         from mdt in m.MaterialIssueDt
                         select new
                         {
                             m.IssueId,
                             m.IndentNo,
                             m.IssueNo,
                             m.IssueDate,
                             m.Indent_Date,
                             m.FromStore,
                             m.ToStore,
                             m.StoreName,
                             mdt.ItemName,
                             mdt.BatchName,
                             mdt.IssuedQuantity,
                             mdt.AuthorisedQuantity,
                             mdt.MRP,
                             
                         };
            return result;
        }

        //public IEnumerator GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }

   
}