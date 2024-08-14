using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Purchase
{
    public class PurchaseIndentDetailEntities
    {
        public int IndentDetailId { get; set; }
        public int ItemId { get; set; }
        public double? ItemRate { get; set; }
        public double? ItemQty { get; set; }
        public double? EstimatedCost { get; set; }
        public double? SalesTax { get; set; }
        public double? ExciseTax { get; set; }
        public double? Escalated { get; set; }
        public double? LandingRate { get; set; }
        public DateTime? DeliveryStartDate { get; set; }
        public DateTime? DeliveryEnddate { get; set; }
        public double? AuthorisedQty { get; set; }
        public int packsizeid { get; set; }
        public double? freeqty { get; set; }
        public double? Discount { get; set; }
        public double? Tax { get; set; }
        public double? TaxAmount { get; set; }
        public string VATOn { get; set; }
        public string VAT { get; set; }
        public double? MRP { get; set; }
        public double? IssueQty { get; set; }
        public double? Consumeqty { get; set; }
        public int BrandID { get; set; }
        public string POStatus { get; set; }
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

        //public int? AuthorisedBy { get; set; }
        //public DateTime? AuthorisedOn { get; set; }
        //public bool Authorised { get; set; }
        //public int? CancelledBy { get; set; }
        //public DateTime? CancelledOn { get; set; }
        //public bool Cancelled { get; set; }

        //Extra column comes here
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public double? CurrentQty { get; set; }
        public string PackSize { get; set; }
        public string DescriptiveName { get; set; }
        public string HSNCode { get; set; }
        public string IndentRemark { get; set; }
        public int IndentIdTemplateId { get; set; }   
        public string ItemsRequiredDate { get; set; }
        public double? TaxRate { get; set; }

    }
}
