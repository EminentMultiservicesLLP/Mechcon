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
    public class MaterialTransferRepository : IMaterialTransferRepository
    {

        public MaterialIssueEntity TransferMaterialIndent(MaterialIssueEntity entity)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("IssueNo", entity.IssueNo, DbType.String,100, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Issuedate", entity.IssueDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Indent_Id", entity.Indent_Id, DbType.Int32));
                paramCollection.Add(new DBParameter("FromStoreId", entity.FromStoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchID", entity.BranchID, DbType.Int32));
                paramCollection.Add(new DBParameter("Issuedby", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Authorised", entity.Authorised, DbType.Boolean));
                paramCollection.Add(new DBParameter("Notes", entity.Notes, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsertMaterialIssue, paramCollection,  CommandType.StoredProcedure);
                entity.IssueId = Convert.ToInt32(parameterList["IssueId"].ToString());
                entity.IssueNo = parameterList["IssueNo"].ToString();

                foreach (var MIDetail in entity.MaterialIssueDt)
                {
                    MIDetail.IssueDetailsId = TransferMIDetails(entity, MIDetail, dbHelper);
                }
            }
            return entity;
        }

        public int TransferMIDetails(MaterialIssueEntity mientity, MaterialIssueDetailsEntity entity, DBHelper dbHelper)
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
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.TransferMaterialIssueDetails, paramCollection, CommandType.StoredProcedure, "IssueDetailsId");
            return iResult;
        }

        public IEnumerable<MaterialIssueEntity> GetAllList()
        {
            List<MaterialIssueEntity> materialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllMaterialIssue, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IndentNo = row.Field<string>("IndentNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                StoreId = row.Field<int?>("StoreId"),
                                FromStoreId = row.Field<int?>("FromStoreId"),
                                Indent_Id = row.Field<int?>("Indent_Id"),
                                BranchID = row.Field<int?>("BranchID"),
                                FromStore = row.Field<string>("FromStoreName"),
                                StoreName = row.Field<string>("StoreName")
                            }).ToList();
            }
            return materialIssue;
        }
    }
}
