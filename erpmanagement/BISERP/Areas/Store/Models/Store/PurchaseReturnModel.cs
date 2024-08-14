using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BISERP.Areas.Store.Models.Store
{
    public class PurchaseReturnModel
    {
        [Display(Name = "Grn No")]
        public string Grnno { get; set; }
        public Nullable<System.DateTime> Grndate { get; set; }
        public string StrGrndate { get; set; }
        public string Supplier { get; set; }
        public string Store { get; set; }

        public int ID { get; set; }
        public Nullable<int> GrnID { get; set; }
        public string PRNo { get; set; }
        public Nullable<System.DateTime> PRDate { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public Nullable<bool> PostedToFA { get; set; }
         [Display(Name = "Project")]
        public Nullable<int> StoreId { get; set; }
        public Nullable<bool> IsConsignee { get; set; }
        public Nullable<int> supplierid { get; set; }
        public Nullable<bool> IsNewPurchase { get; set; }
        public Nullable<int> LocationID { get; set; }

        public Nullable<bool> Authorized { get; set; }
        public Nullable<int> AuthorizedBy { get; set; }
        public Nullable<System.DateTime> AuthDate { get; set; }
        public string AuthMacId { get; set; }
        public string AuthMacName { get; set; }
        public string AuthIPAddress { get; set; }
        public Nullable<int> cancelled { get; set; }
        public Nullable<int> cancelledBy { get; set; }
        public Nullable<System.DateTime> cancelledOn { get; set; }
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
        public List<PurchaseReturnDtModel> PurchaseReturnDt { get; set; }
        public string  Remark { get; set; }
        public string Message { get; set; }
        public string StrPRDate { get; set; }

    }
}