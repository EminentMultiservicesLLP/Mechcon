using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.AdminPanel.Models
{
    public class EmployeeEnrollmentModel
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string PasswordValidate { get; set; }
        public string UserID { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public int DesignationID { get; set; }
        public string Designation { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedByUserID { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public string Message { get; set; }
        public int IsDeactive { get; set; }
    }
    public class DepartmentModel
    {
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public bool Deactive { get; set; }
    }
    public class DesignationModel
    {
        public int DesignationID { get; set; }
        public string Designation { get; set; }
        public bool Deactive { get; set; }
    }
}
