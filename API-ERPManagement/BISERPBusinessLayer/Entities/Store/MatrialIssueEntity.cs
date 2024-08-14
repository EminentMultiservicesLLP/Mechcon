using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public partial class MatrialIssueEntity
    {
        public int Issue_Id { get; set; }
        public string Issue_Number { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> Indent_Id { get; set; }
        public Nullable<int> IssuedBy { get; set; }
        public string Authorised { get; set; }
        public Nullable<int> AuthorisedBy { get; set; }
        public Nullable<System.DateTime> AuthorisedDate { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string Notes { get; set; }
        public Nullable<int> FromStoreId { get; set; }
        public Nullable<int> BranchID { get; set; }
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
    }
}
