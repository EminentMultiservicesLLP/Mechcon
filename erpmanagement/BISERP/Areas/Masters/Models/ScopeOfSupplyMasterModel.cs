using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class ScopeOfSupplyMasterModel
    {
        [JsonProperty("ScopeOfSupplyID")]
        public int ScopeOfSupplyID { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Name")]
        [JsonProperty("ScopeOfSupplyName")]
        public string ScopeOfSupplyName { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Description")]
        [JsonProperty("ScopeOfSupplyDesc")]
        public string ScopeOfSupplyDesc { get; set; }

        [JsonProperty("Deactive")]
        [Display(Name = "Deactive")]
        public bool Deactive { get; set; }

        public string Message { get; set; }
    }
}