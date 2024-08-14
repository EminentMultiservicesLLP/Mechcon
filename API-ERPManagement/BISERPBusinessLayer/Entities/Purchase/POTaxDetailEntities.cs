using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Purchase
{
    public class POTaxDetailEntities
    {
        public int PoDtlID { get; set; }
        public int PoID { get; set; }
        public int ItemID { get; set; }
        public int TaxID { get; set; }
        public float Rate { get; set; }
        public float Amount { get; set; }
        public string Tax_printname { get; set; }
        public string INCL_EXCL { get; set; }
    }
}
