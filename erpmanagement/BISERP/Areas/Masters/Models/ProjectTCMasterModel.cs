using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class ProjectTCMasterModel
    {
        [JsonProperty("ProjectTCID")]
        public int ProjectTCID { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Code")]
        [JsonProperty("ProjectTCCode")]
        public string ProjectTCCode { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Description")]
        [JsonProperty("ProjectTCDesc")]
        public string ProjectTCDesc { get; set; }

        [JsonProperty("Deactive")]
        [Display(Name = "Deactive")]
        public bool Deactive { get; set; }

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
        public bool State { get; set; }
        public int? StoreID { get; set; }
        public bool Select { get; set; }
    }
}