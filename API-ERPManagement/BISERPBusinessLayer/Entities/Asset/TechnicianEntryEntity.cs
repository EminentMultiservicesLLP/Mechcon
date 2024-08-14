using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Asset
{
    public class TechnicianEntryEntity
    {
        public int Id { get; set; }
        public int TechnicianId { get; set; }
        public string TimeSpent { get; set; }
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public string strFromDate { get; set; }
        public string strToDate { get; set; }
        public int InHouseId { get; set; }
        public Nullable<DateTime> WorkedDate { get; set; }
        public string strWorkedDate { get; set; }
        public string Remark { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
    }
}
