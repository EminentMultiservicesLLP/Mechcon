using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Branch
{
    public class GuardInfoEntity
    {
        public int GuardId { get; set; }
        public int BranchId { get; set; }
        public string EmpFName { get; set; }
        public string EmpMName { get; set; }
        public string EmpLName { get; set; }
        public double age { get; set; }
        public int Gender { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Bloodgroup { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Designation { get; set; }
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
        public bool Deactive { get; set; }
        public GuardDetailsEntity GuardDt { get; set; }
    }
}
