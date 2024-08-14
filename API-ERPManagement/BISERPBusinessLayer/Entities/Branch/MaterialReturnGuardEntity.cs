using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Branch
{
    public class MaterialReturnGuardEntity
    {
        public int ReturnId { get; set; }
        public int IssueId { get; set; }
        public string IssueNo { get; set; }
        public string ReturnNo { get; set; }
        public DateTime ReturnDate { get; set; }
        public int StoreId { get; set; }
        public int BranchId { get; set; }
        public int EmpId { get; set; }
        public double NetAmount { get; set; }
        public double Discount { get; set; }
        public double GrossAmount { get; set; }
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
        public List<MaterialReturnGuardDtEntity> MaterialReturnGuardDt { get; set; }
    }
}
