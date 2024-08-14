using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class GRNVendorItemDetailEntity
    {
        public int ID { get; set; }
        public int GrnId { get; set; }
        public int ItemId { get; set; }
        public int BatchId { get; set; }
        public int IssueDetailsId { get; set; }
        public Nullable<double> AcceptedQuantity { get; set; }
        public Nullable<double> PendingQuantity { get; set; }
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
