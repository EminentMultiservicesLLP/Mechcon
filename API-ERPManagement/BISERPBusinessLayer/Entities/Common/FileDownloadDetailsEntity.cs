using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Entities.Common
{
  public class FileDownloadDetailsEntity
    {
        public int ID { get; set; }
        public int DocId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Filename { get; set; }
        public string Filetype { get; set; }
        public DateTime Datecreated { get; set; }
        public bool Deactive { get; set; }
    }
}
