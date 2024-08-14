using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class WarrantyMaintenanceEntity
    {
        public int WarrantyId { get; set; }
        public string Code { get; set; }
        public int RequisitionId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public DateTime MaintenanceTime { get; set; }
        public string DownTime { get; set; }
        public string Days { get; set; }
        public string MaintenanceCost { get; set; }
        public string ActualFault { get; set; }
        public int DoneBy { get; set; }
        public int MaintenanceType { get; set; }
        public Nullable<DateTime> LogDate { get; set; }
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
        public bool Deactive { get; set; }

        public List<WarrantySparePartsEntity> spareparts { get; set; }
    }
}
