using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class MaterialReturnEntity
    {   
        public int ReturnID { get; set; }
        public Nullable<int> IssueId { get; set; }
        public string ReturnNo { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public string StrReturnDate { get; set; }
        public Nullable<int> ReturnedBy { get; set; }
        public Nullable<int> ReturnFrom { get; set; }
        public string StrReturnFrom { get; set; }
        public string StrReturnTo { get; set; }
        public Nullable<int> ReturnTo { get; set; }
        public string ReturnType { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<bool> Authorized { get; set; }
        public Nullable<int> AuthorizedBy { get; set; }
        public Nullable<System.DateTime> AuthDate { get; set; }
        public string AuthMacId { get; set; }
        public string AuthMacName { get; set; }
        public string AuthIPAddress { get; set; }
        public Nullable<int> cancelled { get; set; }
        public Nullable<int> cancelledBy { get; set; }
        public Nullable<System.DateTime> cancelledOn { get; set; }
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
        public string FromStore { get; set; }
        public string ToStore { get; set; }
        public int FromStoreID { get; set; }
        public int ToStoreID { get; set; }
        public string IssueNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string strIssueDate { get; set; }
        public double ReturnAmount { get; set; }
        public List<MaterialReturnDetailsEntities> MaterialReturnDt { get; set; }
    }
}
