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
    public class PurchaseReturnDetailsRepository : IPurchaseReturnDetailsRepository
    {
        public List<PurchaseReturnDetailsEntities> PurchaseReturnDetailsById(int ReturnID)
        {
            List<PurchaseReturnDetailsEntities> PurchaseReturnDetails = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Id", ReturnID, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(StoreQuery.GetPurchaseReturnDt, param, CommandType.StoredProcedure);

                PurchaseReturnDetails = dtPIndent.AsEnumerable()
                            .Select(row => new PurchaseReturnDetailsEntities
                            {
                                ItemID = row.Field<int?>("ItemID"),
                                ItemName = row.Field<string>("ItemName"),
                                BatchID = row.Field<int?>("BatchId"),
                                Batch = row.Field<string>("BatchName"),
                                StrExpiryDate = row.Field<string>("StrExpiryDate"),
                                Qty = row.Field<double?>("Qty"),
                                FreeQty = row.Field<double?>("FreeQty"),
                                Amount = row.Field<double?>("Amount"),
                                LandingRate = row.Field<double?>("LandingRate"),
                                ActualLandingRate = row.Field<double?>("ActualLandingRate"),
                                Reason = row.Field<string>("Reason"),
                            }).ToList();
                return PurchaseReturnDetails;
            }
        }

        public int CreatePurchaseReturntDetails(PurchaseReturnEntity entity1, PurchaseReturnDetailsEntities entity,DBHelper dbHelper)
        {
            int iResult = 0;
           
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("PrdtID", entity.PrdtID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Prid", entity1.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Store",entity1.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Itemid", entity.ItemID, DbType.Int32));
                paramCollection.Add(new DBParameter("Batchid", entity.BatchID, DbType.Int32));
                paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("Qty", entity.Qty, DbType.Int32));
                paramCollection.Add(new DBParameter("Freeqty", entity.FreeQty, DbType.Double));
              //  paramCollection.Add(new DBParameter("Quantity", entity.Quantity, DbType.Int32));
                paramCollection.Add(new DBParameter("Reason", entity.Reason, DbType.String));
                paramCollection.Add(new DBParameter("Mrp ", entity.Mrp, DbType.Double));
                paramCollection.Add(new DBParameter("Rate", entity.Rate, DbType.Double));
                paramCollection.Add(new DBParameter("Vaton", entity.VatOn, DbType.String));
                paramCollection.Add(new DBParameter("Vat", entity.Vat, DbType.String));
                paramCollection.Add(new DBParameter("TaxRate ", entity.TaxRate, DbType.Double));
                paramCollection.Add(new DBParameter("TaxAmount", entity.TaxAmount, DbType.Double));
                paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
                paramCollection.Add(new DBParameter("LandingRate ", entity.LandingRate, DbType.Double));
                paramCollection.Add(new DBParameter("ActualLandingRate", entity.ActualLandingRate, DbType.Double));
                paramCollection.Add(new DBParameter("Grndtlid", entity.Grndtlid, DbType.Int32));
                paramCollection.Add(new DBParameter("Updatedby", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Updatedon", entity.UpdatedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("Updatedipaddress", entity.UpdatedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("Updatedmacname", entity.UpdatedMacName, DbType.String));
                paramCollection.Add(new DBParameter("Updatedmacid", entity.UpdatedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Insertedbyuserid", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Insertedon", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("Insertedipaddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("Insertedmacname", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("Insertedmacid", entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsPurchaseReturnDetail, paramCollection, CommandType.StoredProcedure, "PrdtID");
                //dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPurchaseIndentDetails, paramCollection, CommandType.StoredProcedure);
                //iResult = (int)paramCollection.Get("IndentId").Value;
          
            return iResult;
        }
      
      
    }
}
