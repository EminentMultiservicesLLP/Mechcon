using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Branch
{
    public class EmployeeEntity
    {
        public long EmpId { get; set; }
        public int BranchId { get; set; }
        public int IsOfficeStaff { get; set; }
        public string BranchName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmployeeName { get; set; }
        public string TicketCode { get; set; }
        public int Age { get; set; }
        public string Status { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public int WorkedMonth { get; set; }
        public string StrIsOfficeStaff { get; set; }
    }
}
