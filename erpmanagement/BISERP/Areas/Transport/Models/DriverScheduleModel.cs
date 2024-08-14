using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Transport.Models
{
    public class DriverScheduleModel
    {
        public int ScheduleId { get; set; }

        [Display(Name = "Driver Name")]
        public int DriverId { get; set; }
        public string DriverName { get; set; }

        [Display(Name = "Vehicle No")]
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }

        [Display(Name = "From Place")]
        public int? FromPlace { get; set; }

        [Display(Name = "To Place")]
        public string ToPlace { get; set; }

        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [Display(Name = "Reading on start")]
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
        public string Message { get; set; }
        public string BranchName { get; set; }
        public string Remark { get; set; }
        public FuelDetailsModel fuelDetail { get; set; }

        [Display(Name = "Assign To")]
        public string AssignTo { get; set; }

        [Display(Name = "To Date")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "From Time")]
        public DateTime? FromTime { get; set; }

        [Display(Name = "To Time")]
        public DateTime? ToTime { get; set; }

        [Display(Name = "ApprovedBy")]
        public string ApprovedBy { get; set; }
        public string StrToDate { get; set; }
        public string StrFromTime { get; set; }
        public string StrToTime { get; set; }
        public int  BranchId { get; set; }
        internal object ListToModel(List<DriverScheduleModel> Driver)
        {
            var result = from m in Driver
                         select new
                         {
                             VehicleId = m.VehicleId,
                             VehicleNo = m.VehicleNo,
                             ScheduleId = m.ScheduleId,
                             Advance = m.Advance,
                             DriverName = m.DriverName,
                             FromDate = m.FromDate,
                             FromPlace = m.FromPlace,
                             StartReading = m.StartReading,
                             StrFromDate = m.StrFromDate,
                             ToPlace = m.ToPlace,
                             BranchName = m.BranchName,
                             BalanceAmount = m.fuelDetail.BalanceAmount,
                             Completed = m.fuelDetail.Completed,
                             CompletedDate = m.fuelDetail.StrCompletedDate,
                             EndReading = m.fuelDetail.EndReading,
                             FuelAmount = m.fuelDetail.FuelAmount,
                             FuelPrice = m.fuelDetail.FuelPrice,
                             FuelPump = m.fuelDetail.FuelPump,
                             FuelQuantity = m.fuelDetail.FuelQuantity,
                             FuelType = m.fuelDetail.FuelType,
                             OtherExpense = m.fuelDetail.OtherExpense,
                             PayCardNo = m.fuelDetail.PayCardNo,
                             Paymode = m.fuelDetail.Paymode,
                         };
            return result;
        }

        public string StrFromDate { get; set; }
    }
}