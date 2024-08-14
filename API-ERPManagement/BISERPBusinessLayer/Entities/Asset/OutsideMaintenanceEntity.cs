using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class OutsideMaintenanceEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int RequisitionId { get; set; }
        public double MaterialCost { get; set; }
        public double ManPowerCost { get; set; }
        public double ExternalOrderCost { get; set; }
        public int ApprovedBy { get; set; }
        public string InsuranceClaim { get; set; }
        public Nullable<DateTime> ScheduledFromDate { get; set; }
        public Nullable<DateTime> ScheduledToDate { get; set; }
        public string JobDescription { get; set; }
        public Nullable<DateTime> LogDate { get; set; }
        public string Remark { get; set; }
        public int MaintenanceTypeId { get; set; }
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
        public List<SparePartsOuthouseUtilEntity> SpareParts { get; set; }
        public string  strScheduledFromDate { get; set; }
        public string strScheduledToDate { get; set; }

        public int VendorId { get; set; }
    }
}
