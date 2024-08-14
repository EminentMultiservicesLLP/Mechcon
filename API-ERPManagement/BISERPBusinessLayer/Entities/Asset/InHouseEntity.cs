using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class InHouseEntity
    {
        public int InHouseId { get; set; }
        public string Code { get; set; }
        public int RequisitionId { get; set; }
        public string TimeSpent { get; set; }
        public Nullable<DateTime> CompletedDate { get; set; }
        public string strCompletedDate { get; set; }
        public Nullable<DateTime> TransferDate { get; set; }
        public string strTransferDate { get; set; }
        public Nullable<DateTime> CompletedTime { get; set; }
        public string strCompletedTime { get; set; }
        public string Feedback { get; set; }
        public string TransferReason { get; set; }
        public Nullable<DateTime> LogDate { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsTransfer { get; set; }
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

        public TechnicianEntryEntity TechEntry { get; set; }
        public List<MaterialConsumptionEntity> consumption { get; set; }

        public string RequisitionNo { get; set; }
        public string strRequestDate { get; set; }
        public string RequestedBy { get; set; }
        public string Priority { get; set; }

        public int VendorId { get; set; }
    }
}
