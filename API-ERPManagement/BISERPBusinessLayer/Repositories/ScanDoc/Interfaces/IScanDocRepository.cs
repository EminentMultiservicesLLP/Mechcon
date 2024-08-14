using BISERPBusinessLayer.Entities.ScanDoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Common;

namespace BISERPBusinessLayer.Repositories.ScanDoc.Interfaces
{
    public interface IScanDocRepository
    {
        int SaveScanDoc(ScanDocEntity Entity);
        IEnumerable<ScanDocEntity> GetScanDoc(int FileId, int ScanDocSubTypeId);
        IEnumerable<FileDownloadDetailsEntity> GetFileDownload(int docId);
        int SaveFileDownload(FileDownloadDetailsEntity Entity);
        bool DeleteScanDocfile(int fileId, int scanDocId);
    }
}
