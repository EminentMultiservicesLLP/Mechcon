using BISERPBusinessLayer.Entities.ScanDoc;
using BISERPBusinessLayer.QueryCollection.ScanDoc;
using BISERPBusinessLayer.Repositories.ScanDoc.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Common;

namespace BISERPBusinessLayer.Repositories.ScanDoc.Classes
{
    public class ScanDocRepository : IScanDocRepository
    {
        public int SaveScanDoc(ScanDocEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ScanDocId", Entity.ScanDocId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ScanDocType", Entity.ScanDocType, DbType.String));
                paramCollection.Add(new DBParameter("ScanDocSubType", Entity.ScanDocSubType, DbType.String));
                paramCollection.Add(new DBParameter("FileId", Entity.FileId, DbType.Int32));
                paramCollection.Add(new DBParameter("FileNames", Entity.FileNames, DbType.String));
                paramCollection.Add(new DBParameter("FilePath", Entity.FilePath, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(ScanDocQueries.SaveScanDoc, paramCollection, CommandType.StoredProcedure, "ScanDocId");
            }
            return iResult;
        }


        public IEnumerable<ScanDocEntity> GetScanDoc(int FileId, int ScanDocSubTypeId)
        {
            List<ScanDocEntity> scandocs = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FileId", FileId, DbType.Int32));
                paramCollection.Add(new DBParameter("ScanDocSubTypeId", ScanDocSubTypeId, DbType.String));
                DataTable dtscandocs = dbHelper.ExecuteDataTable(ScanDocQueries.GetAllScanDoc, paramCollection, CommandType.StoredProcedure);
                scandocs = dtscandocs.AsEnumerable()
                            .Select(row => new ScanDocEntity
                            {
                                ScanDocId = row.Field<int>("ScanDocId"),
                                ScanDocType = row.Field<string>("ScanDocType"),
                                ScanDocSubType = row.Field<string>("ScanDocSubType"),
                                FileId = row.Field<int>("FileId"),
                                FileNames = row.Field<string>("FileNames"),
                                FilePath = row.Field<string>("FilePath"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return scandocs;
        }
        public int SaveFileDownload(FileDownloadDetailsEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", Entity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("DocId", Entity.DocId, DbType.Int32));
                paramCollection.Add(new DBParameter("DocumentTypeId", Entity.DocumentTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("Filename", Entity.Filename, DbType.String));
                paramCollection.Add(new DBParameter("Filetype", Entity.Filetype, DbType.String));
                paramCollection.Add(new DBParameter("Datecreated", Entity.Datecreated, DbType.DateTime));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(ScanDocQueries.InsFileDownloaddetails, paramCollection, CommandType.StoredProcedure, "ID");
            }
            return iResult;
        }
        public IEnumerable<FileDownloadDetailsEntity> GetFileDownload(int docId)
        {
            List<FileDownloadDetailsEntity> File = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("docId", docId, DbType.Int32));
                DataTable dtFile = dbHelper.ExecuteDataTable(ScanDocQueries.GetAllFileDownloaddetails, paramCollection, CommandType.StoredProcedure);
                File = dtFile.AsEnumerable()
                            .Select(row => new FileDownloadDetailsEntity
                            {
                                ID = row.Field<int>("ID"),
                                DocId = row.Field<int>("DocId"),
                                DocumentTypeId = row.Field<int>("DocumentTypeId"),
                                Filename = row.Field<string>("Filename"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return File;
        }

        public bool DeleteScanDocfile(int fileId, int scanDocId)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                try
                {
                    dbHelper.ExecuteNonQuery("DELETE FROM ScanDoc WHERE ScanDocId = " + scanDocId);
                    bResult = true;
                }
                catch(Exception exception)
                {
                    throw exception;
                }
            }
            return bResult;
        }
    }
}
