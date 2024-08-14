using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class BranchModel
    {
        
        [Display(Name = "BranchID")]
        [JsonProperty("BranchID")]
        public int BranchID { get; set; }


       [JsonProperty("BranchCode")]
       [Display(Name = "Code")]
        public string BranchCode { get; set; }

        [Display(Name = "Name")]
        [JsonProperty("BranchName")]
        public string BranchName { get; set; }

        [Display(Name = "Street Address")]
        [JsonProperty("Address")]
        public string Address { get; set; }


        [Display(Name = "Pincode")]
        [JsonProperty("Pin")]
        public string  Pin { get; set; }


        [Display(Name = "Name1")]
        [JsonProperty("ContactPerson1")]
        public string ContactPerson1 { get; set; }

        [Display(Name = "Name2")]
        [JsonProperty("ContactPerson2")]
        public string ContactPerson2 { get; set; }

        [Display(Name = "Name3")]
        [JsonProperty("ContactPerson3")]
        public string ContactPerson3 { get; set; }

        [Display(Name = "Phone1")]
        [JsonProperty("Phone1")]
        public string Phone1 { get; set; }

        [Display(Name = "Phone2")]
        [JsonProperty("Phone2")]
        public string Phone2 { get; set; }

        [Display(Name = "Fax")]
        [JsonProperty("Fax")]
        public string Fax { get; set; }

        [Display(Name = "Email")]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [Display(Name = "Website")]
        [JsonProperty("Website")]
        public string Website { get; set; }

        [Display(Name = "Center")]
        [JsonProperty("Center")]
        public bool Center { get; set; }

        [Display(Name = "ProjectID")]
        [JsonProperty("ProjectID")]
        public Nullable<int> ProjectID { get; set; }

        [Display(Name = "Society")]
        [JsonProperty("Society")]
        public string Society { get; set; }

        [Display(Name = "Landmark")]
        [JsonProperty("Landmark")]
        public string  Landmark { get; set; }

        [Display(Name = "Country")]
        [JsonProperty("CountryId")]
        public Nullable<int> CountryId { get; set; }
        [Display(Name = "Village")]
        [JsonProperty("VillageId")]
        public Nullable<int> VillageId { get; set; }
        [Display(Name = "City")]
        [JsonProperty("CityID")]
        public Nullable<int> CityID { get; set; }
        [Display(Name = "State")]
        [JsonProperty("StateID")]
        public Nullable<int> StateID { get; set; }
        //public int City_ID { get; set; }
        public string   Reportfooter { get; set; }

       // public  LogoRight { get; set; }
        public string UnitOf { get; set; }
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
        public string Message { get; set; }


    }
}