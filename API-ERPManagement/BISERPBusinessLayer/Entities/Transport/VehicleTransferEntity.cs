using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Transport
{
    public class VehicleTransferEntity
    {
        public int TransferId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public bool TransferSold { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime SoldDate { get; set; }
        public string strTransferDate { get; set; }
        public string strSoldDate { get; set; }
        public int OldBranchId { get; set; }
        public string OldBranch { get; set; }
        public int NewBranchId { get; set; }
        public string NewBranch { get; set; }
        public int EMICompleted { get; set; }
        public int EMIPending { get; set; }
        public string TransferReason { get; set; }
        public string CustomerName { get; set; }
        public Nullable<double> SoldAmount { get; set; }
        public string SoldReason { get; set; }
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

        public Nullable<bool> Authorised { get; set; }
        public Nullable<int> AuthorisedBy { get; set; }
        public Nullable<System.DateTime> AuthorisedDate { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public Nullable<int> CancelledBy { get; set; }
        public Nullable<System.DateTime> CancelledDate { get; set; }
    }
}
