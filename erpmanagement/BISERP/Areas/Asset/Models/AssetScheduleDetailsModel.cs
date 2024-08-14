using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Asset.Models
{
    public class AssetScheduleDetailsModel
    {
        public int ScheduleDetailId { get; set; }
        public bool IsFrequency { get; set; }
        public int MaintenanceTypeId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Remark { get; set; }
        public string TODO { get; set; }
        public int ScheduleId { get; set; }
        public int ScheduleTypeId { get; set; }
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
        public string StrScheduleDate { get; set; }
        public string MaintenanceType { get; set; }

        public string AssetCode { get; set; }
        public string ItemName { get; set; }
        public int AssetId { get; set; }
    }
}