using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Branch
{
    public class MomentOrderEntity
    {
        public int OrderId { get; set; }
        public string Code { get; set; }
        public int BatchId { get; set; }
        public int TrainingCentreId { get; set; }
        public int BranchId { get; set; }
        public int EmployeeType { get; set; }
        public long EmpId { get; set; }
        public int TempEmpId { get; set; }
        public bool Medical { get; set; }
        public string Details { get; set; }
        public string EmpName { get; set; }
        public string TicketCode { get; set; }
        public Nullable<DateTime> TrainingDate { get; set; }
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
        public bool Deactive { get; set; }
        public string Remarks { get; set; }

        public bool Accepted { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Waist { get; set; }
        public string Fitness { get; set; }
        public Nullable<int> AcceptedBy { get; set; }
        public Nullable<System.DateTime> AcceptedOn { get; set; }

        public string TrainingCentre { get; set; }
        public string BranchName { get; set; }
        public string EmployeeName { get; set; }
        public string Temporaryname { get; set; }
        public string StrTrainingDate { get; set; }
        public string BatchName { get; set; }
        public string StrEmployeeType { get; set; }
        public bool Status { get; set; }
        public string StatupCode { get; set; }
        public double BatchQty { get; set; }
    }
}
