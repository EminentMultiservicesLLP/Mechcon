﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class SupplierMasterModel
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

        [JsonProperty("Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [JsonProperty("Society")]
        [Display(Name = "Society Details")]
        public string Society { get; set; }

        [JsonProperty("Street")]
        [Display(Name = "Street Address")]
        public string Street { get; set; }

        [JsonProperty("Landmark")]
        [Display(Name = "Land Mark")]
        public string Landmark { get; set; }
        public string FullAddress { get; set; }

        [JsonProperty("Pin")]
        [Display(Name = "Pincode")]
        public string Pincode { get; set; }

        [JsonProperty("ContactPerson")]
        [Display(Name = "Name")]
        public string ContactPerson { get; set; }

        [JsonProperty("ContactDesignation")]
        [Display(Name = "Designation")]
        public string ContactDesignation { get; set; }


        [JsonProperty("CreditPeriod")]
        [Display(Name = "Credit Period (In Days)")]
        public int? CreditPeriod { get; set; }

        [JsonProperty("DateOfAssociation")]
        [Display(Name = "Date Of Association")]
        public DateTime? DateOfAssociation { get; set; }

        [JsonProperty("GroupID")]
        public int? GroupID { get; set; }

        [JsonProperty("Fax")]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [JsonProperty("Phone1")]
        [Display(Name = "Phone 1")]
        public string Phone1 { get; set; }

        [JsonProperty("Phone2")]
        [Display(Name = "Phone 2")]
        public string Phone2 { get; set; }

        [JsonProperty("CellPhone")]
        [Display(Name = "Mobile")]
        public string CellPhone { get; set; }

        [JsonProperty("Web")]
        [Display(Name = "Web Address")]
        public string Web { get; set; }

        [JsonProperty("Email")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        [JsonProperty("CST")]
        [Display(Name = "CST TIN No")]
        public string CST { get; set; }

        [JsonProperty("MST")]
        public string MST { get; set; }

        [JsonProperty("TDS")]
        public string TDS { get; set; }

        [JsonProperty("ExciseCode")]
        [Display(Name = "Excise Code")]
        public string ExciseCode { get; set; }

        [JsonProperty("ExportCode")]
        public string ExportCode { get; set; }

        [JsonProperty("LedgerID")]
        public int? LedgerID { get; set; }

        [JsonProperty("EligableForAdv")]
        [Display(Name = "Eligible For Advance")]
        public bool EligableForAdv { get; set; }

        [JsonProperty("BankName")]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [JsonProperty("BankAcNo")]
        [Display(Name = "Account No")]
        public string BankAcNo { get; set; }

        [JsonProperty("MICRNo")]
        [Display(Name = "MICR No.")]
        public string MICRNo { get; set; }

        [JsonProperty("BankBranch")]
        [Display(Name = "Branch")]
        public string BankBranch { get; set; }

        [JsonProperty("Note")]
        public string Note { get; set; }

        [JsonProperty("Proposed")]
        public string Proposed { get; set; }

        [JsonProperty("IncomeTaxNo")]
        [Display(Name = "Income Tax No")]
        public string IncomeTaxNo { get; set; }

        [JsonProperty("SuppType")]
        public string SuppType { get; set; }

        [JsonProperty("AccountId")]
        public int? AccountId { get; set; }

        [JsonProperty("Paytermsid")]
        public int? Paytermsid { get; set; }

        public string GSTIN { get; set; }
        [JsonProperty("RTGSCODE")]
        [Display(Name = "RTGS Code")]
        public string RTGSCODE { get; set; }

        [JsonProperty("IFSCCODE")]
        [Display(Name = "IFSC Code")]
        public string IFSCCODE { get; set; }

        [JsonProperty("SupplierCategory")]
        public int? SupplierCategory { get; set; }

        [JsonProperty("SupplierType")]
        public int? SupplierType { get; set; }

        [JsonProperty("VATTINNo")]
        [Display(Name = "VAT TIN No")]
        public string VATTINNo { get; set; }

        [JsonProperty("ServiceTaxNo")]
        [Display(Name = "Service Tax Reg. No.")]
        public string ServiceTaxNo { get; set; }

        [JsonProperty("PANNo")]
        [Display(Name = "Income Tax PAN")]
        public string PANNo { get; set; }
        [JsonProperty("City")]
        [Required(ErrorMessage = " ")]
        [Display(Name = "City")]
        public int? City { get; set; }

        [JsonProperty("State")]
        [Display(Name = "State")]
        public int? State { get; set; }

        [JsonProperty("Village")]
        [Display(Name = "Village")]
        public string Village { get; set; }

        [JsonProperty("Country")]
        [Display(Name = "Country")]
        public int? Country { get; set; }


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
        public bool Approve { get; set; }
        public bool Rejected { get; set; }

        public int IsMesme { get; set; }

        public List<CityMasterModel> Cities { get; set; }

        [JsonProperty("States")]
        public List<StateMasterModel> States { get; set; }

        [JsonProperty("Villages")]
        public List<VillageMasterModel> Villages { get; set; }
        [JsonProperty("Countries")]
        public List<CountryMasterModel> Countries { get; set; }
        public string strDateOfAssociation { get; set; }
        public string Message { get; set; }


        [Display(Name = "Vendor")]
        [JsonProperty("Vendor")]
        public bool Vendor { get; set; }
        public string Remark { get; set; }
    }
}