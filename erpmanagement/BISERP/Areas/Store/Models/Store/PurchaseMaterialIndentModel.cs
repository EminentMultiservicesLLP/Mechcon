using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Store.Models.Store
{
    public class PurchaseMaterialIndentModel
    {
        public int Id { get; set; }
        public int IndentId { get; set; }
        public int IndentDetailId { get; set; }
        public int PIndentId { get; set; }
    }
}