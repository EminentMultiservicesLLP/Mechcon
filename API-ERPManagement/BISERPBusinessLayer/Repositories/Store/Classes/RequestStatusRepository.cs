using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class RequestStatusRepository : IRequestStatusRepository
    {
        public int SaveRequestStatus(RequestStatusEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("RequestId", entity.RequestId, DbType.Int32));
                paramCollection.Add(new DBParameter("RequestDate", entity.RequestDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Remark", entity.Remark, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertRequestStatus, paramCollection, CommandType.StoredProcedure, "ID");
            }
            return iResult;
        }

        public int UpdateIndentRequestStatus(int RequestId)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RequestId", RequestId, DbType.Int32));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdateMIndentRequestStatus, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }

        public int UpdateIssueRequestStatus(int RequestId)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RequestId", RequestId, DbType.Int32));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdateMIssueRequestStatus, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }

        public int UpdateIssueAuthRequestStatus(int IssueId, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueId", IssueId, DbType.Int32));
            iResult = dbhelper.ExecuteNonQuery(StoreQuery.UpdateMIssueAuthRequestStatus, paramCollection, CommandType.StoredProcedure);
            return iResult;
        }

        public int UpdatePMIndentRequestStatus(int RequestId)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RequestId", RequestId, DbType.Int32));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdatePMIndentRequestStatus, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }

        public int UpdatePORequestStatus(int RequestId)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("PIndentId", RequestId, DbType.Int32));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdatePORequestStatus, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }

        public int UpdatePMAuthRequestStatus(int RequestId)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("PIndentId", RequestId, DbType.Int32));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdatePIAuthorizeRequestStatus, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }

        public int UpdatePMAuthRequestStatus(int RequestId, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("PIndentId", RequestId, DbType.Int32, ParameterDirection.InputOutput));

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.UpdatePIAuthorizeRequestStatus, paramCollection, CommandType.StoredProcedure, "PIndentId");
            return iResult;
        }

        public bool UpdatePOAuthRequestStatus(PurchaseOrderEntities entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("RFQId", entity.RFQId, DbType.Int32));
            iResult = dbhelper.ExecuteNonQuery(StoreQuery.UpdatePOAuthRequestStatus, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public int UpdateGRNRequestStatus(int GRNId, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("GRNId", GRNId, DbType.Int32));
            iResult = dbhelper.ExecuteNonQuery(StoreQuery.UpdateGRNRequestStatus, paramCollection, CommandType.StoredProcedure);
            return iResult;
        }

        public int UpdateGRNAuthRequestStatus(int GRNId, DBHelper dbhelper)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("GRNId", GRNId, DbType.Int32));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdateGRNAuthRequestStatus, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }

        public int UpdateMRRequestStatus(int IssueId, DateTime ReturnDate, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueId", IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("ReturnDate", ReturnDate, DbType.DateTime));
            iResult = dbhelper.ExecuteNonQuery(StoreQuery.UpdateMRRequestStatus, paramCollection, CommandType.StoredProcedure);
            return iResult;
        }

        public int UpdateMRAuthRequestStatus(int ReturnID)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ReturnID", ReturnID, DbType.Int32));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdateMRAuthRequestStatus, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }

        public List<RequestStatusEntity> RequestStatusReport(DateTime Fromdate, DateTime Todate, int StoreId, string IndentNo)
        {
            List<RequestStatusEntity> grn = new List<RequestStatusEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("fromdate", Fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", Todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("IndentNo", IndentNo.Replace("~", "/").Replace("''", ""), DbType.String));
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.RequestStatusReport, paramCollection, CommandType.StoredProcedure);
                grn = dtgrn.AsEnumerable()
                             .Select(row => new RequestStatusEntity
                             {
                                 ID = row.Field<int>("ID"),
                                 RequestId = row.Field<int>("RequestId"),
                                 RequestNo = row.Field<string>("IndentNo"),
                                 strRequestDate = row.Field<DateTime?>("RequestDate").DateTimeFormat2(),
                                 Remark = row.Field<string>("Remark"),
                                 strAuthorizedDate = row.Field<DateTime?>("AuthorizedDate").DateTimeFormat2(),
                                 strPIDate = row.Field<DateTime?>("PIDate").DateTimeFormat2(),
                                 strPIAuthorizedDate = row.Field<DateTime?>("PIAuthorizedDate").DateTimeFormat2(),
                                 PIRemark = row.Field<string>("PIRemark"),

                                 strPODate = row.Field<DateTime?>("PODate").DateTimeFormat2(),
                                 strPOAuthorizedDate = row.Field<DateTime?>("POAuthorizedDate").DateTimeFormat2(),
                                 PORemark = row.Field<string>("PORemark"),

                                 strGRNDate = row.Field<DateTime?>("GRNDate").DateTimeFormat2(),
                                 strGRNAuthorizedDate = row.Field<DateTime?>("GRNAuthorizedDate").DateTimeFormat2(),
                                 GRNRemark = row.Field<string>("GRNRemark"),

                                 strMIDate = row.Field<DateTime?>("MIDate").DateTimeFormat2(),
                                 strMIAcceptedDate = row.Field<DateTime?>("MIAcceptedDate").DateTimeFormat2(),
                                 MIRemark = row.Field<string>("MIRemark"),

                                 strMRDate = row.Field<DateTime?>("MRDate").DateTimeFormat2(),
                                 strMRAuthorizedDate = row.Field<DateTime?>("MRAuthorizedDate").DateTimeFormat2(),
                                 MRRemark = row.Field<string>("MRRemark")
                             }).ToList();
            }
            return grn;
        }
    }
}
