using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Transport
{
    public class FuelDetailsEntity
    {
        public int FuelDetailId { get; set; }
        public int ScheduleId { get; set; }
        public int FuelType { get; set; }
        public int FuelPump { get; set; }
        public double FuelPrice { get; set; }
        public double FuelQuantity { get; set; }
        public double FuelAmount { get; set; }
        public int Paymode { get; set; }
        public string PayCardNo { get; set; }
        public double OtherExpense { get; set; }
        public double EndReading { get; set; }
        public double BalanceAmount { get; set; }
        public bool Completed { get; set; }
        public Nullable<DateTime> CompletedDate { get; set; }

        public string StrCompletedDate { get; set; }
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


        public string StrFuelType { get; set; }
    }
}
