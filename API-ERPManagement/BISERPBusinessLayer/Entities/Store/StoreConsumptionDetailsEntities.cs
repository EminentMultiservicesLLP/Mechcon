using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
 public class StoreConsumptionDetailsEntities
    {
        public int ConsumptionDtlId { get; set; }
        public Nullable<int> ConsumptionId { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> BatchId { get; set; }
        public string Unit { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string strExpiryDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Batch { get; set; }
        public double StockQty { get; set; }
        public Nullable<double> ConsumeQty { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Cancelledby { get; set; }
        public Nullable<System.DateTime> Cancelledon { get; set; }
        public Nullable<double> CancelledQty { get; set; }
        public string CancellationRemark { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
