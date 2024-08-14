using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Areas.Masters.Models
{
    public class MechconMasterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string emailID { get; set; }
        [Display(Name = "CIN No.")]
        public string CINNumber { get; set; }
        [Display(Name = "GST No.")]
        public string GSTNumber { get; set; }
        public bool Deactive { get; set; }
        public int InsertedBy { get; set; }        
        public DateTime InsertedON { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public string InsertedMacName { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Message { get; set; }
    

    }
}