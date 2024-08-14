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

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class MaterialIssueDetailRepository : IMaterialIssueDetailRepository
    {

        public int CreateMIDetails(MaterialIssueEntity mientity, MaterialIssueDetailsEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueDetailsId", entity.IssueDetailsId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IssueId", mientity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("IndentDetailId", entity.IndentDetailId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
            paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
            paramCollection.Add(new DBParameter("IssuedQuantity", entity.IssuedQuantity, DbType.Double));
            paramCollection.Add(new DBParameter("AuthorisedQuantity", entity.AuthorisedQuantity, DbType.Double));
            paramCollection.Add(new DBParameter("Status", entity.Status, DbType.String));
            paramCollection.Add(new DBParameter("FromStore", mientity.FromStoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("ToStore", mientity.StoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", mientity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", mientity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedMacName", mientity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", mientity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("InsertedIPAddress", mientity.InsertedIPAddress, DbType.String));
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertMaterialIssueDetails, paramCollection, CommandType.StoredProcedure, "IssueDetailsId");
            return iResult;
        }

        public IEnumerable<MaterialIssueDetailsEntity> GetDetailByIssueID(int IssueId)
        {
            List<MaterialIssueDetailsEntity> MaterialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IssueId", IssueId, DbType.Int32);
                DataTable dtMaterialIssue = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialIssueDtlsById, param, CommandType.StoredProcedure);
                MaterialIssue = dtMaterialIssue.AsEnumerable()
                            .Select(row => new MaterialIssueDetailsEntity
                            {
                                IssueDetailsId = row.Field<int>("IssueDetailsId"),
                                IssueId = row.Field<int>("IssueId"),
                                IndentDetailId = row.Field<int>("IndentDetailId"),
                                ItemId = row.Field<int>("ItemId"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemCode = row.Field<string>("ItemCode"),
                                UnitName = row.Field<string>("UnitName"),                                
                                BatchId = row.Field<int?>("BatchId"),
                                BatchName = row.Field<string>("BatchName"),
                                ExpiryDate = Convert.ToDateTime(row.Field<DateTime?>("ExpiryDate")).ToString("dd-MMMM-yyyy"),
                                Item_Stock = row.Field<double?>("Item_Stock"),
                                IssuedQuantity = row.Field<double>("IssuedQuantity"),
                                AuthorisedQuantity = row.Field<double?>("AuthorisedQuantity")
                            }).ToList();
            }
            return MaterialIssue;
        }

        public bool UpdateMaterialIssueAuthQty(MaterialIssueEntity mientity, MaterialIssueDetailsEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueDetailsId", entity.IssueDetailsId, DbType.Int32));
            paramCollection.Add(new DBParameter("IssueId", mientity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedBy", mientity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedOn", mientity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("AuthorisedQuantity", entity.AuthorisedQuantity, DbType.Double));

            paramCollection.Add(new DBParameter("InsertedBy", mientity.InsertedBy, DbType.Int32));
            //paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", mientity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", mientity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", mientity.InsertedMacID, DbType.String));
            if (entity.Authorised == true)
            {
                paramCollection.Add(new DBParameter("Authorised", 1, DbType.Boolean));
            }
            else
            {
                paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
            }
            paramCollection.Add(new DBParameter("FromStore", mientity.FromStoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("ToStore", mientity.StoreId, DbType.Int32));
            iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdMaterialIssueAuthQty, paramCollection, CommandType.StoredProcedure);

            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
