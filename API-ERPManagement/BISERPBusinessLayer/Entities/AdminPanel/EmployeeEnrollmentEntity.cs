﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.AdminPanel
{
    public class EmployeeEnrollmentEntity
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public Nullable<int> InsertedBy { get; set; }
        public Nullable<System.DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public Nullable<int> UpdatedByUserID { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int IsDeactive { get; set; }
    }
}