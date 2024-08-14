using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class StorewiseItemTypeMappingMasterModel
    {
        [Display(Name = "Project")]
        [JsonProperty("StoreID")]
        public int? StoreID { get; set; }

        [JsonProperty("ItemTypeID")]
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

        public List<ItemTypeMasterModel> StorewiseITdt { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public List<int> Itemtype { get; set; }
        public Nullable<bool> Select { get; set; }

    }
}