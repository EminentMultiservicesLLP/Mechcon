using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class ClientEntity
    {
        public int ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string Society { get; set; }
        public string Street { get; set; }
        public string landmark { get; set; }
        public string Village { get; set; }
        public string Division { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> Country { get; set; }
        public string Pin { get; set; }
        public string ContactPerson { get; set; }
        public string ContactDesignation { get; set; }
        public string Fax { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public DateTime InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }

        public int? AccountId { get; set; }
        public int? Paytermsid { get; set; }
        public string VATTINNo { get; set; }
        public string ServiceTaxNo { get; set; }
        public string ExciseCode { get; set; }
        public string PANNo { get; set; }
        public int? CreditPeriod { get; set; }
        public string CST { get; set; }
        public DateTime? DateOfAssociation { get; set; }

        public string strDateOfAssociation { get; set; }
        public string IncomeTaxNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankAcNo { get; set; }
        public string RTGSCODE { get; set; }
        public string MICRNo { get; set; }
        public string IFSCCODE { get; set; }
        public string GSTIN { get; set; }
        public List<ConsigneeEntity> Consignee { get; set; }
        public string FullAddress { get; set; }
    }
    public class ConsigneeEntity
    {
        public int ClientId { get; set; }
        public int ConsigneeId { get; set; }
        public string ConsigneeName { get; set; }
        public string ConAddress { get; set; }
        public string ConGSTIN { get; set; }
    }
}
