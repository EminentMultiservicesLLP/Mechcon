using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
 public class StoreConsumptionCancelEntities
    {
        public int ConCancelid { get; set; }
        public Nullable<int> ConsumptionDtlid { get; set; }
        public Nullable<int> Consumptionid { get; set; }
        public Nullable<int> Itemid { get; set; }
     
        public Nullable<int> Batchid { get; set; }
        public int Storeid { get; set; }
        public string code { get; set; }
        public string Unit { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Batch { get; set; }
        public double StockQty { get; set; }
        public Nullable<double> Qty { get; set; }
        public Nullable<double> ConsumeQty { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Cancelledby { get; set; }
        public Nullable<System.DateTime> Cancelledon { get; set; }
        public string CancellationRemark { get; set; }
        public string strExpiryDate { get; set; }

        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }

    }
}
