using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Miscellaneous.Models
{
    public class StoreConumptionModel
    {   
        public int ConsumptionId { get; set; }
         [Display(Name = "Date")]
        public Nullable<DateTime> ConsumptionDate { get; set; }

         [Display(Name = "Store")]
        public Nullable<int> StoreId { get; set; }
        public string store { get; set; }
        [JsonProperty("ConsumptionCode")]
        public string ConsumptionCode { get; set; }
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
        public List<StoreConumptiondtModel> StockConsumptiondt { get; set; }
        public string Message { get; set; }
        public string strConsumptionDate { get; set; }
    }
}