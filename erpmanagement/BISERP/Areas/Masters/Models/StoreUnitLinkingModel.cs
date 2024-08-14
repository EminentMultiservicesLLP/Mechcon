using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class StoreUnitLinkingModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "UnitStore")]
        [JsonProperty("UnitStore")]
        public string UnitStore { get; set; }

        [Display(Name = "Unit Store")]
        [JsonProperty("StoreId")]
        public int StoreId { get; set; }

        [Display(Name = "Sub-Store")]
        [JsonProperty("ParentId")]
        public int ParentId { get; set; }

        [JsonProperty("StoreName")]
        public string StoreName { get; set; }
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

        [Display(Name = "Deactive")]
        [JsonProperty("Deactive")]
        public bool Deactive { get; set; }
        public IEnumerable<StoreTypeMasterModel> storetypes { get; set; }
        public string Message { get; set; }
    }
}