﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Training.Models
{
    public class TrainingTypeModel
    {
        public int TypeId { get; set; }
        public string TrainingTypeCode { get; set; }
        public string TrainingType { get; set; }
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
        public string Message { get; set; }
    }
}