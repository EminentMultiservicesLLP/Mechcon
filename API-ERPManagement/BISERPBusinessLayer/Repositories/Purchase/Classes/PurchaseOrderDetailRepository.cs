using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class PurchaseOrderDetailRepository : IPurchaseOrderDetailRepository
    {
        public List<PurchaseOrderDetailsEntities> GetDetailByPOID(int Id)
        {
            List<PurchaseOrderDetailsEntities> podetails = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("POID", Id, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(PurchaseQueries.GetPODetailsByPOId, param, CommandType.StoredProcedure);

                podetails = dtPIndent.AsEnumerable()
                            .Select(row => new PurchaseOrderDetailsEntities
                            {
                                ID = row.Field<int>("ID"),
                                POID = row.Field<int>("POID"),
                                ItemID = row.Field<int>("ItemID"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemCode = row.Field<string>("ItemCode"),
                                TaxRate = row.Field<double?>("TaxRate"),
                                Qty = row.Field<double?>("Qty"),
                                PackSizeId = row.Field<int>("PackSizeId"),
                                PackSize = row.Field<string>("PackSize"),
                                SupplierId = row.Field<int?>("SupplierId"),
                                SupplierName = row.Field<string>("SupplierName"),
                                OrderingUnit = row.Field<string>("OrderingUnit"),
                                Rate = row.Field<double?>("Rate"),
                                Amount = row.Field<double?>("Amount"),
                                FreeQty = row.Field<double?>("FreeQty"),
                                discountper = row.Field<double?>("discountper"),
                                Discount = row.Field<double?>("Discount"),
                                Tax = row.Field<double?>("Tax"),
                                TaxAmount = row.Field<double?>("TaxAmount"),
                                MRP = row.Field<double?>("MRP"),
                                TransC = row.Field<double?>("TransC"),
                                OtherC = row.Field<double?>("OtherC"),
                                LoadUnloadC = row.Field<double?>("LoadUnloadC"),
                                OctroiC = row.Field<double?>("OctroiC"),
                                NetAmount = row.Field<double?>("NetAmount"),
                                POIndDtlId = row.Field<int>("POIndDtlId"),                                
                                TaxIds = row.Field<string>("TaxIds"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                IGST = row.Field<double?>("IGST"),
                                CGST = row.Field<double?>("CGST"),
                                UGST = row.Field<double?>("UGST"),
                                SGST = row.Field<double?>("SGST"),
                                HSNCode = row.Field<string>("HSNCode"),
                                PoPendingQty = row.Field<double?>("PoPendingQty"),
                            }).ToList();
            }
            return podetails;
        }

        public int CreateNewEntry(PurchaseOrderEntities poEntity, PurchaseOrderDetailsEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("POID", poEntity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
            paramCollection.Add(new DBParameter("Qty", entity.Qty, DbType.Double));
            paramCollection.Add(new DBParameter("PackSizeId", entity.PackSizeId, DbType.Int32));
            paramCollection.Add(new DBParameter("PackSize", entity.PackSize, DbType.String));
            paramCollection.Add(new DBParameter("SupplierName", entity.SupplierName, DbType.String));
            paramCollection.Add(new DBParameter("SupplierId", entity.SupplierId, DbType.Int32));
            paramCollection.Add(new DBParameter("Rate", entity.Rate, DbType.Double));
            paramCollection.Add(new DBParameter("FreeQty", entity.FreeQty, DbType.Double));
            paramCollection.Add(new DBParameter("discountper", entity.discountper, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("Tax", entity.Tax, DbType.Double));
            paramCollection.Add(new DBParameter("TaxAmount", entity.TaxAmount, DbType.Double));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Double));
            paramCollection.Add(new DBParameter("TransC", entity.TransC, DbType.Double));
            paramCollection.Add(new DBParameter("OtherC", entity.OtherC, DbType.Double));
            paramCollection.Add(new DBParameter("LoadUnloadC", entity.LoadUnloadC, DbType.Double));
            paramCollection.Add(new DBParameter("OctroiC", entity.OctroiC, DbType.Double));
            paramCollection.Add(new DBParameter("NetAmount", entity.NetAmount, DbType.Double));
            paramCollection.Add(new DBParameter("POIndDtlId", entity.POIndDtlId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", poEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", poEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", poEntity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("IGST", entity.IGST, DbType.Double));
            paramCollection.Add(new DBParameter("CGST", entity.CGST, DbType.Double));
            paramCollection.Add(new DBParameter("UGST", entity.UGST, DbType.Double));
            paramCollection.Add(new DBParameter("SGST", entity.SGST, DbType.Double));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertPurchaseOrderDetails, paramCollection, CommandType.StoredProcedure, "ID");
            if (entity.TaxIds != null)
            {
                if (entity.TaxIds.Trim() != "")
                {
                    paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("PoDtlID", iResult, DbType.Int32));
                    paramCollection.Add(new DBParameter("PoID", poEntity.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
                    paramCollection.Add(new DBParameter("TaxIDs", entity.TaxIds.ToString(), DbType.String));
                    dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPOTaxDetails, paramCollection, CommandType.StoredProcedure);
                }
            }
            return iResult;
        }
      
        public bool UpdatePurchaseOrderQty(int POID, PurchaseOrderDetailsEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("POID", POID, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
                paramCollection.Add(new DBParameter("Qty", entity.Qty, DbType.Double));
                iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.UpdatePurchaseOrderDetails, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public int AmmendPoDetail(PurchaseOrderEntities poEntity, PurchaseOrderDetailsEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("POID", poEntity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
            paramCollection.Add(new DBParameter("Qty", entity.Qty, DbType.Double));
            paramCollection.Add(new DBParameter("PackSizeId", entity.PackSizeId, DbType.Int32));
            paramCollection.Add(new DBParameter("PackSize", entity.PackSize, DbType.String));
            paramCollection.Add(new DBParameter("SupplierName", entity.SupplierName, DbType.String));
            paramCollection.Add(new DBParameter("SupplierId", entity.SupplierId, DbType.Int32));
            paramCollection.Add(new DBParameter("Rate", entity.Rate, DbType.Double));
            paramCollection.Add(new DBParameter("FreeQty", entity.FreeQty, DbType.Double));
            paramCollection.Add(new DBParameter("discountper", entity.discountper, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("Tax", entity.Tax, DbType.Double));
            paramCollection.Add(new DBParameter("TaxAmount", entity.TaxAmount, DbType.Double));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Double));
            paramCollection.Add(new DBParameter("TransC", entity.TransC, DbType.Double));
            paramCollection.Add(new DBParameter("OtherC", entity.OtherC, DbType.Double));
            paramCollection.Add(new DBParameter("LoadUnloadC", entity.LoadUnloadC, DbType.Double));
            paramCollection.Add(new DBParameter("OctroiC", entity.OctroiC, DbType.Double));
            paramCollection.Add(new DBParameter("NetAmount", entity.NetAmount, DbType.Double));
            paramCollection.Add(new DBParameter("POIndDtlId", entity.POIndDtlId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", poEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", poEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", poEntity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("IGST", entity.IGST, DbType.Double));
            paramCollection.Add(new DBParameter("CGST", entity.CGST, DbType.Double));
            paramCollection.Add(new DBParameter("UGST", entity.UGST, DbType.Double));
            paramCollection.Add(new DBParameter("SGST", entity.SGST, DbType.Double));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.AmmendPoDetail, paramCollection, CommandType.StoredProcedure);
            return iResult;
        }
    }
}
