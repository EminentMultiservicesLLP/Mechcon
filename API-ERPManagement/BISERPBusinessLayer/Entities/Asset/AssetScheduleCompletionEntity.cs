using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
   public class AssetScheduleCompletionEntity
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int ScheduleDetailId { get; set; }
        public int AssetId { get; set; }
        public int MaintenanceTypeId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Remark { get; set; }
        public string TODO { get; set; }
        public DateTime CompletedDate { get; set; }
        public string DoneBy { get; set; }
        public string CompletionRemark { get; set; }
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

    }
}
