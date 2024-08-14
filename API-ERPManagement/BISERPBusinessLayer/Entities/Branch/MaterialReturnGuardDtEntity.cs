using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Branch
{
    public class MaterialReturnGuardDtEntity
    {
        public int ReturnDtlId { get; set; }
        public int ReturnId { get; set; }
        public int ItemId { get; set; }
        public int BatchId { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }
        public double MRP { get; set; }
        public double Discount { get; set; }
        public double Amount { get; set; }
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
    }
}
