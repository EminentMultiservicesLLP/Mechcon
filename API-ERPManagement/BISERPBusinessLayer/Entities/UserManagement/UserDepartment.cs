using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.UserManagement
{
    public class UserDepartment
    {
        public long UserId { get; set; }
        public int? BranchId { get; set; }
        public int? StoreId { get; set; }
        public int? UnitStoreId { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedON { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public int? InsertedBy { get; set; }
        public DateTime? InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
    }
}
