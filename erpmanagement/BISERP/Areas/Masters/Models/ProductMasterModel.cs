using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class ProductMasterModel
    {
        [JsonProperty("ProductID")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Name")]
        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Description")]
        [JsonProperty("ProductDesc")]
        public string ProductDesc { get; set; }

        [JsonProperty("Deactive")]
        [Display(Name = "Deactive")]
        public bool Deactive { get; set; }

        public string Message { get; set; }
    }
}