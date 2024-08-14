using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class AMCProviderEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string Pincode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string ContactPerson1 { get; set; }
        public string ContactPerEmail1 { get; set; }
        public string ContactPerDesignation1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string ContactPerEmail2 { get; set; }
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
