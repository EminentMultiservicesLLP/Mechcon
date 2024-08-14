using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.Masters.Models
{
    public class ClearanceNoteModel
    {
        public int ClearanceNoteId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int DispatchType { get; set; }
        public string DispatchTypeName { get; set; }
        public int? InsertedBy { get; set; }
        public string InsertedByName { get; set; }
        public DateTime? InsertedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}