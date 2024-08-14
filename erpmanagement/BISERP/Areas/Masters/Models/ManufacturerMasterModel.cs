using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class ManufacturerMasterModel
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Code")]
        [Required(ErrorMessage = " ")]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [JsonProperty("Name")]
        [Required(ErrorMessage = " ")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [JsonProperty("Society")]
        [Display(Name = "Society Details")]
        public string Society { get; set; }

        [JsonProperty("Street")]
        [Display(Name = "Street Address")]
        public string Street { get; set; }

        [JsonProperty("Landmark")]
        [Display(Name = "Land Mark")]
        public string Landmark { get; set; }

        [JsonProperty("Village")]
        [Display(Name = "Suburb/Village/Region")]
        public int? Village { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("City")]
        [Required(ErrorMessage = " ")]
        [Display(Name = "City/District")]
        public int? City { get; set; }

        [JsonProperty("Pin")]
        [Display(Name = "Pincode")]
        public string Pincode { get; set; }

        [JsonProperty("State")]
        [Display(Name = "State")]
        public int? State { get; set; }

        [JsonProperty("Country")]
        [Display(Name = "Country")]
        public int? Country { get; set; }

        [JsonProperty("ContactPerson")]
        [Display(Name = "Name")]
        public string ContactPerson { get; set; }

        [JsonProperty("ContactDesignation")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [JsonProperty("BankName")]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [JsonProperty("BankAcNo")]
        [Display(Name = "Account No")]
        public string AccountNo { get; set; }

        [JsonProperty("BankBranch")]
        [Display(Name = "Branch")]
        public string BankBranch { get; set; }

        [JsonProperty("Phone1")]
        [Display(Name = "Phone 1")]
        public string Phone1 { get; set; }

        [JsonProperty("Phone2")]
        [Display(Name = "Phone 2")]
        public string Phone2 { get; set; }

        [JsonProperty("CellPhone")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [JsonProperty("Web")]
        [Display(Name = "Web Address")]
        public string Webaddress { get; set; }

        [JsonProperty("Email")]
        [Display(Name = "Email Address")]
        public string Emailaddress { get; set; }

        [JsonProperty("Note")]
        [Display(Name = "Note")]
        public string Note { get; set; }

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

        public List<CityMasterModel> Cities { get; set; }
        public List<StateMasterModel> States { get; set; }
        public List<VillageMasterModel> Villages { get; set; }
        public List<CountryMasterModel> Countries { get; set; }
        public string Message { get; set; }
    }
}