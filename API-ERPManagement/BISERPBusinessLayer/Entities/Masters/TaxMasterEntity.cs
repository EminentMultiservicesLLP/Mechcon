using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Masters
{
    public class TaxMasterEntity
    {
        public int? Taxid { get; set; }
        public string Tax_Code { get; set; }
        public string Tax_name { get; set; }
        public int? Tax_Type { get; set; }
        public double? Tax_percentage { get; set; }
        public string Formula { get; set; }
        public Nullable<bool> Tax_EncExc { get; set; }
        public string Taxes { get; set; }
        public string Tax_Printname { get; set; }
        public string TaxSub_Type { get; set; }
        public double? FormC_Tax { get; set; }
        public Nullable<bool> IsDependent { get; set; }
        public double? Total_TaxPer { get; set; }
        public Nullable<bool> Deactive { get; set; }
        public int? OtherTaxID { get; set; }
    }
}
