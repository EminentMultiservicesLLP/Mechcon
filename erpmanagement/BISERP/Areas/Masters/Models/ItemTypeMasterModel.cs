using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class ItemTypeMasterModel
    {   
        [ JsonProperty("ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "ItemType Code")]
        [JsonProperty("Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "ItemType Name")]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "PO Exception")]
        [JsonProperty("POException")]
        public Nullable<double> POException { get; set; }

        [Display(Name = "Manufacturer Required")]
        [JsonProperty("IsManufactRequired")]       
        public Nullable <bool> IsManufactRequired { get; set; }

        [Display(Name = "Deactive")]
        [JsonProperty("Deactive")]
        public Nullable<bool>  Deactive { get; set; }

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
        public Nullable<bool> Select { get; set; }
        public string Message { get; set; }
    }
}