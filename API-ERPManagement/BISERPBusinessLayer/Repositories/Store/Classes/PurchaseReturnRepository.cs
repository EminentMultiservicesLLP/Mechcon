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
    public class PurchaseReturnRepository : IPurchaseReturnRepository
    {
        public IEnumerable<PurchaseReturnEntity> GetDetailByID(int StoreId )
        {
            List<PurchaseReturnEntity> PurchaseReturn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Storeid", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("IsConsignee", 0, DbType.Boolean));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetGrnNoforPurReturnStore, paramCollection, CommandType.StoredProcedure);

                PurchaseReturn = dtMaterialIndent.AsEnumerable()
                            .Select(row => new PurchaseReturnEntity
                            {
                                GrnID = row.Field<int?>("GrnID"),
                                Grnno = row.Field<string>("GRNNo"),
                                //IndentNo = row.Field<string>("IndentNo"),
                                //Grndate = row.Field<DateTime?>("GRNDate"),
                                StrGrndate = row.Field<string>("GRNDate"),
                                supplierid = row.Field<int?>("supplierid"),
                                Supplier = row.Field<string>("Supplier"),
                                Store = row.Field<string>("Store"),
                            }).ToList();
            }
            return PurchaseReturn;
        
        }
        public IEnumerable<PurchaseReturnDetailsEntities> GetGrnDetailByID(int GrnId)
        {
            List<PurchaseReturnDetailsEntities> PurchaseReturn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", GrnId, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetGrnDtlForPurchaseReturn, paramCollection, CommandType.StoredProcedure);

                PurchaseReturn = dtMaterialIndent.AsEnumerable()
                            .Select(row => new PurchaseReturnDetailsEntities
                            {
                                ItemID = row.Field<int?>("ItemID"),
                                //  SupplierID = row.Field<int?>("SupplierID"),
                                ItemName = row.Field<string>("ItemName"),
                                UnitName = row.Field<string>("UnitName"),
                                GrnQty = row.Field<double?>("GrnQty"),
                                GrnFreeQty = row.Field<double?>("GrnFreeQty"),
                                BatchID = row.Field<int?>("BatchID"),
                                Batch = row.Field<string>("Batch"),
                                StockQty = row.Field<double?>("StockQty"),
                                ActualLandingRate = row.Field<double?>("ActualLendingRate"),
                                TaxRate = row.Field<double?>("TaxRate"),
                                TaxAmount = row.Field<double?>("TaxAmount"),
                                Discount = row.Field<double?>("Discount"),
                                VatOn = row.Field<string>("VATOn"),
                                Vat = row.Field<string>("VAT"),
                                Rate = row.Field<double?>("Rate"),
                                Mrp = row.Field<double?>("mrp"),
                                LandingRate = row.Field<double?>("LendingRate"),
                               // ID = row.Field<int>("grndtlid"),
                                balReturnQty =row.Field<double?>("balReturnQty"),
                                Grndtlid = row.Field<int?>("grndtlid")
                            }).ToList();
            }
            return PurchaseReturn;

        }
        public IEnumerable<PurchaseReturnEntity> GetAllPurchaseReturn(int storeId)
        {
            List<PurchaseReturnEntity> purchaseReturn = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Storeid", storeId, DbType.Int32));
                DataTable dtPurchaseIndent = dbHelper.ExecuteDataTable(StoreQuery.GetPurchaseReturn, paramCollection,CommandType.StoredProcedure);
                purchaseReturn = dtPurchaseIndent.AsEnumerable()
                            .Select(row => new PurchaseReturnEntity
                            {
                                ID = row.Field<int>("PRId"),
                                PRNo = row.Field<string>("PRNo"),
                                Grnno = row.Field<string>("GRNNo"),
                                PRDate = row.Field<DateTime?>("PRDate"),
                                StrPRDate = row.Field<string>("StrPRDate"),
                                Supplier = row.Field<string>("Supplier"),
                           
                            }).ToList();
            }
            return purchaseReturn;
        }
        public PurchaseReturnEntity CreatePurchaseReturn(PurchaseReturnEntity entity,DBHelper dbHelper)
        {
           
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", entity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Grnid", entity.GrnID, DbType.Int32));
                paramCollection.Add(new DBParameter("Prno", entity.PRNo, DbType.String, 100, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Prdate", entity.PRDate, DbType.DateTime));
               // paramCollection.Add(new DBParameter("ReturnedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Custom1", entity.Custom1, DbType.String));
                paramCollection.Add(new DBParameter("Custom2", entity.Custom2, DbType.String));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("IsConsignee", entity.IsConsignee, DbType.Boolean));
                paramCollection.Add(new DBParameter("BranchID", entity.LocationID, DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierId", entity.supplierid, DbType.Int32));
                paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("Remark", entity.Remark, DbType.String));
                //paramCollection.Add(new DBParameter("AuthMacId", entity.AuthMacId, DbType.Int32));
                //paramCollection.Add(new DBParameter("AuthMacName", entity.AuthMacName, DbType.Int32));
                //paramCollection.Add(new DBParameter("AuthIPAddress", entity.AuthIPAddress, DbType.Int32));
                //paramCollection.Add(new DBParameter("cancelled", entity.cancelled, DbType.Int32));
                //paramCollection.Add(new DBParameter("cancelledBy", entity.cancelledBy, DbType.Int32));
                //paramCollection.Add(new DBParameter("cancelledOn", entity.cancelledOn, DbType.DateTime));
                //paramCollection.Add(new DBParameter("FromStore", entity.FromStore, DbType.String));
                //paramCollection.Add(new DBParameter("ToStore", entity.ToStore, DbType.String));
                paramCollection.Add(new DBParameter("Insertedbyuserid", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Insertedon", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("Insertedipaddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("Insertedmacname", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("Insertedmacid", entity.InsertedMacID, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsPurchaseReturnMaster, paramCollection,  CommandType.StoredProcedure);
                entity.ID = Convert.ToInt32(parameterList["Id"].ToString());
                entity.PRNo = parameterList["Prno"].ToString();
                // iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsMaterialReturnMaster, paramCollection, CommandType.StoredProcedure, "ReturnID");
          
            return entity;
        }
        public bool PurchaseReturnAuth(PurchaseReturnEntity entity, PurchaseReturnDetailsEntities entity1)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", entity1.ItemID, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthorizedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthorizedDate", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("BatchId", entity1.BatchID, DbType.Int32));
                paramCollection.Add(new DBParameter("Quantity", entity1.Qty, DbType.Double));

                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
               //paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));


                if (entity.Authorized == true)
                {
                    paramCollection.Add(new DBParameter("Authorized", 1, DbType.Boolean));
                }
                else
                {
                    paramCollection.Add(new DBParameter("cancelled", 1, DbType.Boolean));
                }

                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdPurchaseReturnAuth, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
