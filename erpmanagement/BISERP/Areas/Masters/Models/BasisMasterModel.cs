﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class BasisMasterModel
    {
        public int BasisID { get; set; }
        public string BasisCode { get; set; }
        public string BasisDesc { get; set; }
        public bool Deactive { get; set; }
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
    }
}