using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Store
{
    public class PurchaseReturnEntity
    {
       
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
        public List<PurchaseReturnDetailsEntities> PurchaseReturnDt { get; set; }
        public string Remark { get; set; }
        public string StrPRDate { get; set; }
    }
    public class PurchaseReturnRptEntity
    {
        public string PRNo { get; set; }
        public string StrPRDate { get; set; }
        public string Store { get; set; }
        public string Grnno { get; set; }
        public string StrGrndate { get; set; }
        public string Supplier { get; set; }
        public string SupAdd { get; set; }
        public string InsertedByName { get; set; }
        public string strInsertedOn { get; set; }
        public string AuthorisedByName { get; set; }
        public string strAuthDate { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyGST { get; set; }
        public string companyCIN { get; set; }
        public string companyEmail { get; set; }
        public List<PurchaseReturnDetailsRptEntities> PurchaseReturnDt { get; set; }
    }
}
