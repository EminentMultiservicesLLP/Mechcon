using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class SupplierMasterEntities
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Society { get; set; }
        public string Street { get; set; }
        public string Landmark { get; set; }
        public string Village { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> Country { get; set; }
        public string Pin { get; set; }
        public string FullAddress { get; set; }
        public string GSTIN { get; set; }
        public string ContactPerson { get; set; }
        public string ContactDesignation { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<int> CreditPeriod { get; set; }
        public Nullable<System.DateTime> DateOfAssociation { get; set; }
        public string Fax { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string CST { get; set; }
        public string MST { get; set; }
        public string TDS { get; set; }
        public string ExciseCode { get; set; }
        public string ExportCode { get; set; }
        public Nullable<int> LedgerID { get; set; }
        public Nullable<bool> EligableForAdv { get; set; }

        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankAcNo { get; set; }
        public string MICRNo { get; set; }
        public string Note { get; set; }
        public string Proposed { get; set; }
        public string IncomeTaxNo { get; set; }
        public string SuppType { get; set; }
        public Nullable<int> AccountId { get; set; }
        public Nullable<int> Paytermsid { get; set; }
        public string RTGSCODE { get; set; }
        public string VATTINNo { get; set; }
        public string ServiceTaxNo { get; set; }
        public string PANNo { get; set; }
        public Nullable<int> SupplierCategory { get; set; }
        public Nullable<int> SupplierType { get; set; }
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
        public bool? Deactive { get; set; }
        public string strDateOfAssociation { get; set; }
        public string IFSCCODE { get; set; }
        public bool Rejected { get; set; }
        public string Remark { get; set; }
        public int IsMesme { get; set; }
    }
}
