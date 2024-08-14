using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class StoreConsumptionCancellationModel
    {
        public int ConCancelid { get; set; }

        public Nullable<int> ConsumptionDtlid { get; set; }
        public Nullable<int> Consumptionid { get; set; }
        [JsonProperty("Itemid")]
        public Nullable<int> Itemid { get; set; }

        [JsonProperty("Batchid")]
        public Nullable<int> Batchid { get; set; }

        [Display(Name = "Store")]
        public int Storeid { get; set; }

        [JsonProperty("code")]
        public string  code { get; set; }

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
        public Nullable<double> Qty { get; set; }
        public Nullable<double> ConsumeQty { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Cancelledby { get; set; }
        public Nullable<System.DateTime> Cancelledon { get; set; }
        public Nullable<double> CancelledQty { get; set; }
       
        public string strExpiryDate { get; set; }
        public string Message { get; set; }


        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }

    }
}