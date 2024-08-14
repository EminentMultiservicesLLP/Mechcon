using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class ManufacturerMasterEntities
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Society { get; set; }
        public string Street { get; set; }
        public string Landmark { get; set; }
        public Nullable<int> Village { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> Country { get; set; }
        public string Pin { get; set; }
        public string ContactPerson { get; set; }
        public string ContactDesignation { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string CellPhone { get; set; }
        public string Web { get; set; }
        public string Email { get; set; }
        public string BankName { get; set; }
        public string BankAcNo { get; set; }
        public string BankBranch { get; set; }
        public string Note { get; set; }       
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
        public Nullable<bool> Deactive { get; set; }
    }
}
