using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Miscellaneous.Models
{
    public class StoreConumptiondtModel
    {
        public int ConsumptionDtlId { get; set; }
        public Nullable<int> ConsumptionId { get; set; }
        [JsonProperty("ItemID")]
        public Nullable<int> ItemID { get; set; }

        [JsonProperty("BatchId")]
        public Nullable<int> BatchId { get; set; }
        [JsonProperty("Unit")]
        public string Unit { get; set; }
        [JsonProperty("ExpiryDate")]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        [JsonProperty("ItemCode")]
        public string ItemCode { get; set; }
        [JsonProperty("ItemName")]
        public string ItemName { get; set; }
        [JsonProperty("Batch")]
        public string Batch { get; set; }
        public double StockQty { get; set; }
        public Nullable<double> ConsumeQty { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Cancelledby { get; set; }
        public Nullable<System.DateTime> Cancelledon { get; set; }
        public Nullable<double> CancelledQty { get; set; }
        public string CancellationRemark { get; set; }
        public string strExpiryDate { get; set; }

    }
}