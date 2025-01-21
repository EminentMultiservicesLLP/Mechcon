using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.AdminPanel
{
    public class GroupMaster
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Deactive { get; set; }
        public string Message { get; set; }
        public List<GroupUserMapping> GroupUserMapping { get; set; }
    }
    public class GroupUserMapping
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public bool State { get; set; }
    }
}
