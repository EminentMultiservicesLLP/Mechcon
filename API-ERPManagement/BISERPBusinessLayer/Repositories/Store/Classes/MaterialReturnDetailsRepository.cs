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
    public class MaterialReturnDetailsRepository : IMaterialReturnDetailsRepository
    {
        public IEnumerable<MaterialReturnDetailsEntities> GetDetailByIssueID(int IssueId)
        {
            List<MaterialReturnDetailsEntities> MaterialReturn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IssueId", IssueId, DbType.Int32);
                DataTable dtMaterialReturn = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialReturnIssueDtlsById, param, CommandType.StoredProcedure);
                MaterialReturn = dtMaterialReturn.AsEnumerable()
                            .Select(row => new MaterialReturnDetailsEntities
                            {
                               
                                ItemID = row.Field<int?>("ItemID"),
                                BatchId = row.Field<int?>("BatchId"),
                                StockQty = row.Field<double?>("StockQty"),
                                ItemName = row.Field<string>("ItemName"),
                                Batch = row.Field<string>("Batch"),
                                ExpiryDate = row.Field<DateTime?>("ExpiryDate"),
                                strExpiryDate = Convert.ToDateTime(row.Field<DateTime?>("ExpiryDate")).ToString("dd-MMM-yyyy"),
                                IssueQty = row.Field<double?>("IssueQty"),

                            }).ToList();
            }
            return MaterialReturn;
        }
        public int CreateMaterialReturntDetails(int ReturnID, MaterialReturnDetailsEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
          
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ReturnDtlID", entity.ReturnDtlID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ReturnID", ReturnID, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Quantity", entity.Quantity, DbType.Int32));
                paramCollection.Add(new DBParameter("Reason", entity.Reason, DbType.String));
                //paramCollection.Add(new DBParameter("StockQty ", entity.StockQty, DbType.Double));
                //paramCollection.Add(new DBParameter("totalReturnQty", entity.totalReturnQty, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsMaterialReturnDetail, paramCollection, CommandType.StoredProcedure, "ReturnDtlID");
                //dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPurchaseIndentDetails, paramCollection, CommandType.StoredProcedure);
                //iResult = (int)paramCollection.Get("IndentId").Value;
          
            return iResult;
        }
        public List<MaterialReturnDetailsEntities> MaterialReturnDetailsById(int ReturnID)
        {
            List<MaterialReturnDetailsEntities> MaterialReturnDetails = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ReturnID", ReturnID, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(StoreQuery.GetMaterialReturnDTById, param, CommandType.StoredProcedure);

                MaterialReturnDetails = dtPIndent.AsEnumerable()
                            .Select(row => new MaterialReturnDetailsEntities
                            {
                                ItemID = row.Field<int?>("ItemID"),
                                ReturnDtlID = row.Field<int>("ReturnDtlID"),
                                ItemName = row.Field<string>("ItemName"),
                                BatchId = row.Field<int?>("BatchId"),
                                Batch = row.Field<string>("Batch"),
                                ExpiryDate = row.Field<DateTime?>("ExpiryDate"),
                                strExpiryDate = Convert.ToDateTime(row.Field<DateTime?>("ExpiryDate")).ToString("dd-MMM-yyyy"),
                                StockQty = row.Field<double?>("StockQty"),
                                Quantity = row.Field<int?>("Quantity"),
                                Reason = row.Field<string>("Reason"),


                            }).ToList();
                return MaterialReturnDetails;
            }
        }

      
    }
}
