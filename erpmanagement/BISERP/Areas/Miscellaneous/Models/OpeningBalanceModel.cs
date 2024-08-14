using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Miscellaneous.Models
{
    public class OpeningBalanceModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [Display(Name = "Store")]
        [JsonProperty("Storeid")]
        public int Storeid { get; set; }

        [JsonProperty("ItemId")]
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public string BatchName { get; set; }

        [JsonProperty("OpeningBal")]
        public Nullable<double> OpeningBal { get; set; }
        [JsonProperty("CurrentBal")]
        public Nullable<double> CurrentBal { get; set; }

        [JsonProperty("ExpiryDate")]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        [JsonProperty("Rate")]
        public Nullable<double> Rate { get; set; }
        [JsonProperty("MRP")]
        public Nullable<double> MRP { get; set; }
      
        public Nullable<double> PackSizeMrp { get; set; }
        
        public Nullable<double> PackLendingRate { get; set; }
        
        public Nullable<double> LendingRate { get; set; }
     
        public Nullable<double> ActualLendingRate { get; set; }
       
        [JsonProperty("UpdatedMacName")]
        public string UpdatedMacName { get; set; }

        [JsonProperty("UpdatedMacID")]
        public string UpdatedMacID { get; set; }

        [JsonProperty("UpdatedIPAddress")]
        public string UpdatedIPAddress { get; set; }

        [JsonProperty("UpdatedBy")]
        public Nullable<int> UpdatedBy { get; set; }

        [JsonProperty("UpdatedOn")]
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        [JsonProperty("InsertedBy")]
        public Nullable<int> InsertedBy { get; set; }

        [JsonProperty("InsertedON")]
        public Nullable<System.DateTime> InsertedON { get; set; }

        [JsonProperty("InsertedMacName")]
        public string InsertedMacName { get; set; }

        [JsonProperty("InsertedMacID")]
        public string InsertedMacID { get; set; }

        [JsonProperty("InsertedIPAddress")]
        public string InsertedIPAddress { get; set; }

    }
}