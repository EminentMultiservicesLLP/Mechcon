using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class ItemPackSizeMasterModel
    {  
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Code")]
        [Required(ErrorMessage = " ")]
        [Display(Name = "Pack Code")]
        public string Code { get; set; }

        [JsonProperty("Name")]
        [Required(ErrorMessage = " ")]
        [Display(Name = "Pack Name")]
        public string Name { get; set; }

        [JsonProperty("Quantity")]
        [Required(ErrorMessage = " ")]
        [Display(Name = "Pack Quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("Deactive")]
        [Display(Name = "Deactive")]
        public Nullable<bool> Deactive { get; set; }

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
        public string Message { get; set; }
    }
}