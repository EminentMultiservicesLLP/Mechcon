using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class PrefixMaster
    {
        [Required(ErrorMessage = " ")]
        [Display(Name = "Select Prefix For")]
        [JsonProperty("SelectPrefixFor")]
        public string Prefixfor { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Select Format")]
        [JsonProperty("Select Format")]
        public string Format { get; set; }


        [Required(ErrorMessage = " ")]
        [Display(Name = "Generated Code")]
        [JsonProperty("GeneratedCode")]
        public string GeneratedCode { get; set; }



        [Required(ErrorMessage = " ")]
        [Display(Name = "Start From")]
        [JsonProperty("StartFrom")]
        public string  StartFrom{ get; set; }


        [Required(ErrorMessage = " ")]
        [Display(Name = "Reset")]
        [JsonProperty("Reset")]
        public string Reset { get; set; }


    }
}