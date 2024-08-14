using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class RequestRegisterEntity
    {
        public int RequestId { get; set; }
        public int AssetId { get; set; }
        public string RequestCode { get; set; }
        public Nullable<DateTime> RequestedDate { get; set; }
        public string Priority { get; set; }
        public string Complaint { get; set; }
        public Nullable<DateTime> RequiredDate { get; set; }
        public Nullable<DateTime> DownTime { get; set; }
        public int RequestedBy { get; set; }
        public int RequisitionStatus { get; set; }
        public int MaintenanceStatus { get; set; }
        public string Assignment { get; set; }
        public string ProblemNature { get; set; }
        public int ProblemId { get; set; }
        public string Remark { get; set; }
        public int IsCompleted { get; set; }
        public string RejectionReason { get; set; }
        public int TransferTypeId { get; set; }
        public string TransferReason { get; set; }
        public Nullable<DateTime> TransferDate { get; set; }
        public int IsRenderedToMaintenance { get; set; }
        public Nullable<DateTime> CompletedDate { get; set; }
        public int IsClosed { get; set; }
        public string ClosingReason { get; set; }
        public Nullable<DateTime> ClosingDate { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }
        public string AssetCode { get; set; }
        public string ItemName { get; set; }
        public string ModelNo { get; set; }
        public string SerialNo { get; set; }
        public string Description { get; set; }
        public string StrRequestedDate { get; set; }
        public string StrRequiredDate { get; set; }
        public string UserName { get; set; }
        public string MaintenanceNo { get; set; }
        public int MaintenanceId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public string BranchName { get; set; }
        public int AssetLocationId { get; set; }
        public string StrRequisitionStatus { get; set; }
        public string StrMaintenanceStatus { get; set; }

    }
}
