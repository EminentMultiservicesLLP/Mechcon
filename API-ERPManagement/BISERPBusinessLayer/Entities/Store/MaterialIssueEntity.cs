using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public partial class MaterialIssueEntity
    {
        public int IssueId { get; set; }
        public string IssueNo { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> FromStoreId { get; set; }
        public string FromStore { get; set; }
        public string StoreName { get; set; }
        public Nullable<int> Indent_Id { get; set; }
        public string IndentNo { get; set; } 
        public Nullable<int> IssuedBy { get; set; }
        public bool Authorised { get; set; }
        public Nullable<int> AuthorisedBy { get; set; }
        public Nullable<System.DateTime> AuthorisedDate { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string strIssueDate { get; set; }
        public string Notes { get; set; }        
        public Nullable<int> BranchID { get; set; }
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
        public List<MaterialIssueDetailsEntity> MaterialIssueDt { get; set; }
        public string ToStore { get; set; }
        public DateTime Indent_Date { get; set; }
        public string strIndentDate { get; set; }

        public string Filename { get; set; }
        public double Total { get; set; }


    }

}
