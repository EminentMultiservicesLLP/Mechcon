using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class StockConumptionModel
    {   
        public int ConsumptionId { get; set; }
         [Display(Name = "Date")]

        
        public Nullable<DateTime> ConsumptionDate { get; set; }
        [Display(Name = "Store")]
        public Nullable<int> StoreId { get; set; }

        [JsonProperty("ConsumptionCode")]
        public string ConsumptionCode { get; set; }


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
        public Nullable<int> LocationID { get; set; }
        public string isWBorPK { get; set; }
        public string strExpiryDate { get; set; }
        public List<StockConumptiondtModel> StockConsumptiondt { get; set; }
    }
}