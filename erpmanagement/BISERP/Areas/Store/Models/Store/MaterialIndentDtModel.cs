using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class MaterialIndentDtModel
    {
        [JsonProperty("IndentDetails_Id")]
        public int IndentDetails_Id { get; set; }

        [JsonProperty("Indent_Id")]
        public int Indent_Id { get; set; }

        [JsonProperty("Item_Id")]
        public int Item_Id { get; set; }

        [JsonProperty("User_Quantity")]
        public Nullable<float> User_Quantity { get; set; }

        [JsonProperty("Authorised_Curr_Quantity")]
        public Nullable<float> Authorised_Curr_Quantity { get; set; }

        [JsonProperty("Authorised_Quantity")]
        public Nullable<float> Authorised_Quantity { get; set; }

        [JsonProperty("Item_Stock")]
        public Nullable<float>Item_Stock { get; set; }

        [JsonProperty("ItemAddedBy")]
        public string ItemAddedBy { get; set; }

        [JsonProperty("Authorised")]
        public Nullable<bool> Authorised { get; set; }

        [JsonProperty("PendingQty")]
        public Nullable<float> PendingQty { get; set; }

        [JsonProperty("indCancelledby")]
        public Nullable<int> indCancelledby { get; set; }

        [JsonProperty("indCancelledOn")]
        public Nullable<System.DateTime> indCancelledOn { get; set; }

        [JsonProperty("indCancelReason")]
        public Nullable<int> indCancelReason { get; set; }

        [JsonProperty("indCancelled")]
        public  Nullable<bool> indCancelled { get; set; }

        
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

        [Display(Name = "Deactive")]
        [JsonProperty("Deactive")]
        public Nullable<bool> Deactive { get; set; }

        [Display(Name = "Itemname")]
        public string ItemName { get; set; }
        public string ItemCode { get; set; }

        public int? BatchId { get; set; }
        public string BatchName { get; set; }
        public string ExpiryDate { get; set; }

        [Display(Name = "DispensingUnit")]
        public string DispensingUnit { get; set; }

        [Display(Name = "PackSizeName")]
        public string PackSizeName { get; set; }
        public Nullable<float> IssuedQuantity { get; set; }
        public Nullable<bool> state { get; set; }


        [JsonProperty("MICancelled")]
        public Nullable<bool> MICancelled { get; set; }

        [JsonProperty("MICancelledby")]
        public Nullable<int> MICancelledby { get; set; }
        public string MICancelledbyUser { get; set; }

        [JsonProperty("MICancelledOn")]
        public Nullable<System.DateTime> MICancelledOn { get; set; }

        public string MICancelReason { get; set; }

        public string strauthorised { get; set; }
        public double? MRP { get; set; }
        public string DescriptiveName { get; set; }
        public int PackSizeID { get; set; }
        public string HSNCode { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}