using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Branch
{
    public class MaterialIssueGuardDtEntity
    {
        public int IssueDetailsId { get; set; }
        public int IssueId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public double IssuedQuantity { get; set; }
        public double? MRP { get; set; }
        public double? Discount { get; set; }
        public double? Amount { get; set; }
        public DateTime RenewDate { get; set; }
        public string strRenewDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedON { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
    }
}
