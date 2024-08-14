using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class AssetScheduleEntity
    {
        public int ScheduleId { get; set; }
        public int AssetId { get; set; }
        public string AssetCode { get; set; }
        public string ItemName { get; set; }
        public bool IsFrequency { get; set; }
        public int MaintenanceTypeId { get; set; }
        public string MaintenanceType { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public int NoOfTimes { get; set; }
        public int ScheduleType { get; set; }
        public int Interval { get; set; }
        public int CUR_IsFrequency { get; set; }
        public int CUR_MaintenanceTypeId { get; set; }
        public DateTime CUR_StartDate { get; set; }
        public string CUR_ToDate { get; set; }
        public int CUR_NoOfTimes { get; set; }
        public int CUR_Interval { get; set; }
        public int CUR_ScheduleType { get; set; }
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

        public List<AssetScheduleDetailsEntity> schDetails { get; set; }
        public string BranchName { get; set; }
    }
}
