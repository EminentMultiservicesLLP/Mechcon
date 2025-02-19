﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Miscellaneous.Models
{
    public class CancelPendingMaterialIndentModel
    {
        [Display(Name = "Stores Name")]
        public int storeId { get; set; }
        public string IndentNo { get; set; }
        public Nullable<System.DateTime> Indent_Date { get; set; }
        public string ToStore { get; set; }
        public string FromStore { get; set; }
        public Nullable<int> Indent_id { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> Item_Id { get; set; }
        public Nullable<double> AuthorisedQty { get; set; }
        public Nullable<double> BalanceQty { get; set; }
       
        public Nullable<int> UNITID { get; set; }
        public string Unit { get; set; }
        public Nullable<int> indentdetails_id { get; set; }
        public Nullable<int> CurrentStock { get; set; }
        public bool Authorized { get; set; }
        public Nullable<int> AuthorizedBy { get; set; }
        public Nullable<System.DateTime> AuthorizedOn { get; set; }
        public bool Cancelled { get; set; }
        public string Cancelledreason { get; set; }
        public string strIndentDate { get; set; }



    }
}