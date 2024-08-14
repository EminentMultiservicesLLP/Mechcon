using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BISERP.Areas.ScanDoc.Models
{
    public class ScanDocModel
    {
        public int ScanDocId { get; set; }
        public int ScanDocTypeId { get; set; }
        public string ScanDocType { get; set; }
        public int ScanDocSubTypeId { get; set; }
        public string ScanDocSubType { get; set; }
        public int FileId { get; set; }
        public string FileNames { get; set; }
        public string FilePath { get; set; }
        public string UpdatedMacName { get; set; }
        public string UpdatedMacID { get; set; }
        public string UpdatedIPAddress { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public int InsertedBy { get; set; }
        public Nullable<DateTime> InsertedON { get; set; }
        public string InsertedMacName { get; set; }
        public string InsertedMacID { get; set; }
        public string InsertedIPAddress { get; set; }
        public bool Deactive { get; set; }
        public string strErrorMsg { get; set; }
    }
}