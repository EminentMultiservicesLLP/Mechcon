using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Asset.Models
{
    public class AMCProviderModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        [Display(Name="City")]
        public int CityId { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }
        public string Pincode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }

        [Display(Name = "Con Per. 1")]
        public string ContactPerson1 { get; set; }

        [Display(Name = "Con Per. 1 Email")]
        public string ContactPerEmail1 { get; set; }

        [Display(Name = "Con Per. 1 Desig.")]
        public string ContactPerDesignation1 { get; set; }

        [Display(Name = "Con Per. 2")]
        public string ContactPerson2 { get; set; }

        [Display(Name = "Con Per. 2 Email")]
        public string ContactPerEmail2 { get; set; }

        [Display(Name = "Con Per. 2 Desig.")]
        public string ContactPerDesignation2 { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }

    }
}