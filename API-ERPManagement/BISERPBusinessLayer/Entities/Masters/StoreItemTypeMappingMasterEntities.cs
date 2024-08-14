using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class StoreItemTypeMappingMasterEntities
    {
        public int? StoreID { get; set; }
        public int ItemTypeID { get; set; }
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
        public Nullable<bool> Deactive { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<ItemTypeMasterEntities> StorewiseITdt { get; set; }
        public bool Select { get; set; }
        public List<int> Itemtype { get; set; }
    }
}
