using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Transport
{
    public class DriverScheduleEntity
    {
        public int ScheduleId { get; set; }
        public int? DriverId { get; set; }
        public int VehicleId { get; set; }
        public int? FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime FromDate { get; set; }
        public double StartReading { get; set; }
        public double Advance { get; set; }
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
        public FuelDetailsEntity fuelDetail { get; set; }
        public string DriverName { get; set; }
        public string VehicleNo { get; set; }
        public string StrFromDate { get; set; }
        public string  BranchName { get; set; }

        public string Remark { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public string ApprovedBy { get; set; }
        public string StrToDate { get; set; }
        public string StrFromTime { get; set; }
        public string StrToTime { get; set; }
        public int BranchId { get; set; }
        public string AssignTo { get; set; }
        public string TypeName { get; set; }
        public string FromBranchName { get; set; }

     
    }
}
