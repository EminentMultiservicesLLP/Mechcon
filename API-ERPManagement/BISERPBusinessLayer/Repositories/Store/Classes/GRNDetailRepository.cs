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
    public class GRNDetailRepository : IGRNDetailRepository
    {

        public GRNDetailEntity GetByID(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GRNDetailEntity> GetAllList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GRNDetailEntity> GetDetailByGRNId(int GRNId)
        {
            List<GRNDetailEntity> podetails = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("GRNId", GRNId, DbType.Int32);
                DataTable dtPIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAllGRNDetails, param, CommandType.StoredProcedure);

                podetails = dtPIndent.AsEnumerable()
                            .Select(row => new GRNDetailEntity
                            {
                                ID = row.Field<int>("ID"),
                                ItemID = row.Field<int>("ItemID"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemCode = row.Field<string>("ItemCode"), 
                                PackSizeId = row.Field<int>("PackSizeId"),
                                PackSize = row.Field<string>("PackSize"),
                                UnitName = row.Field<string>("UnitName"),
                                Qty = row.Field<double?>("Qty"),
                                FreeQty = row.Field<double?>("FreeQty"),
                                Rate = row.Field<double?>("Rate"),
                                MRP = row.Field<double?>("MRP"),                                
                                Amount = row.Field<double?>("Amount"),
                                BatchID = row.Field<int>("BatchID"),
                                Taxes = row.Field<string>("Taxes"),
                                TaxRates = row.Field<string>("TaxRates"),
                                BatchName = row.Field<string>("BatchName"),
                                ExpiryDate = row.Field<DateTime>("ExpiryDate"),
                                strExpiryDate = row.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                                CExpiryDate = row.Field<DateTime>("CExpiryDate"),
                                strCExpiryDate = row.Field<DateTime?>("CExpiryDate").DateTimeFormat1(),
                                CBatch = row.Field<string>("CBatch"),
                                DiscountPer = row.Field<double?>("DiscountPer"),
                                Discount = row.Field<double?>("Discount"),
                                TaxAmount = row.Field<double?>("TaxAmount"),                                
                                CQty = row.Field<double?>("CQty"),
                                CFreeQty = row.Field<double?>("CFreeQty"),
                                CRate = row.Field<double?>("CRate"),
                                CMrp = row.Field<double?>("CMrp"),
                                TransC = row.Field<double?>("TransC"),
                                InstallC = row.Field<double?>("InstallC"),
                                OtherC = row.Field<double?>("OtherC"),
                                ServiceAmt = row.Field<double?>("ServiceAmt"),
                                CustomDuty = row.Field<double?>("CustomDuty"),
                                Status = row.Field<string>("Status"),
                                CGST = row.Field<double?>("CGST"),
                                SGST = row.Field<double?>("SGST"),
                                IGST = row.Field<double?>("IGST"),
                                UGST = row.Field<double?>("UGST"),
                                HSNCode = row.Field<string>("HSNCode"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                RejectedQty = row.Field<double>("RejectedQty"),
                                PoPendingQty = row.Field<double>("PoPendingQty"),
                                RecieveQty = row.Field<double>("RecieveQty"),
                                RejectionReason = row.Field<string>("RejectionReason"),
                            }).ToList();
            }
            return podetails;
        }
        public int CreateNewEntry(GRNEntity grnentity, GRNDetailEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Id", entity.ID, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("GrnID", grnentity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
            paramCollection.Add(new DBParameter("Qty", entity.Qty, DbType.Double));
            paramCollection.Add(new DBParameter("PackSize", entity.PackSize, DbType.String));
            paramCollection.Add(new DBParameter("PackSizeId", entity.PackSizeId, DbType.Int32));
            paramCollection.Add(new DBParameter("FreeQty", entity.FreeQty, DbType.Double));
            paramCollection.Add(new DBParameter("Rate", entity.Rate, DbType.Double));
            paramCollection.Add(new DBParameter("TaxRate", entity.TaxRate, DbType.Double));
            paramCollection.Add(new DBParameter("TaxAmount", entity.TaxAmount, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("DiscountPer", entity.DiscountPer, DbType.Double));
            paramCollection.Add(new DBParameter("MRP", (entity.MRP == null ? 0 : entity.MRP), DbType.Double));
            paramCollection.Add(new DBParameter("BatchName", entity.BatchName, DbType.String));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("lendingRate", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("Taxes", entity.Taxes, DbType.String));
            paramCollection.Add(new DBParameter("TaxRates", entity.TaxRates, DbType.Double));
            paramCollection.Add(new DBParameter("TransC", entity.TransC, DbType.Double));
            paramCollection.Add(new DBParameter("InstallC", entity.InstallC, DbType.Double));
            paramCollection.Add(new DBParameter("OtherC", entity.OtherC, DbType.Double));
            paramCollection.Add(new DBParameter("ServiceAmt", entity.ServiceAmt, DbType.Double));
            paramCollection.Add(new DBParameter("CustomDuty", entity.CustomDuty, DbType.Double));
            paramCollection.Add(new DBParameter("CQty", entity.CQty, DbType.Double));
            paramCollection.Add(new DBParameter("CFreeQty", entity.CFreeQty, DbType.Double));
            paramCollection.Add(new DBParameter("CRate", entity.CRate, DbType.Double));
            paramCollection.Add(new DBParameter("CMrp", entity.CMrp, DbType.Double));
            paramCollection.Add(new DBParameter("CBatch", entity.CBatch, DbType.String));
            paramCollection.Add(new DBParameter("ConsigneeGrnDtlID", entity.ConSigneeGrnDtlID, DbType.Int32));
            paramCollection.Add(new DBParameter("IGST", entity.IGST, DbType.Double));
            paramCollection.Add(new DBParameter("CGST", entity.CGST, DbType.Double));
            paramCollection.Add(new DBParameter("UGST", entity.UGST, DbType.Double));
            paramCollection.Add(new DBParameter("SGST", entity.SGST, DbType.Double));
            paramCollection.Add(new DBParameter("RejectedQty", entity.RejectedQty, DbType.Double));
            paramCollection.Add(new DBParameter("RejectionReason", entity.RejectionReason, DbType.String));
            paramCollection.Add(new DBParameter("RecieveQty", entity.RecieveQty, DbType.Double));
            paramCollection.Add(new DBParameter("InsertedBy", grnentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", grnentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", grnentity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", grnentity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", grnentity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertGRNDetail, paramCollection, CommandType.StoredProcedure, "Id");
            if (entity.Taxes != null)
            {
                if (entity.Taxes.Trim() != "")
                {
                    paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("GrnDtlID", iResult, DbType.Int32));
                    paramCollection.Add(new DBParameter("GrnID", grnentity.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Taxes", entity.Taxes.ToString(), DbType.String));
                    dbHelper.ExecuteNonQuery(StoreQuery.InsertGRNTaxDetail, paramCollection, CommandType.StoredProcedure);
                }
            }
            return iResult;
        }

        public bool UpdateGRNDetailEntry(GRNEntity grnentity, GRNDetailEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("Id", entity.ID, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("GrnID", grnentity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
            paramCollection.Add(new DBParameter("Qty", entity.Qty, DbType.Double));
            paramCollection.Add(new DBParameter("PackSize", entity.PackSize, DbType.String));
            paramCollection.Add(new DBParameter("PackSizeId", entity.PackSizeId, DbType.Int32));
            paramCollection.Add(new DBParameter("FreeQty", entity.FreeQty, DbType.Double));
            paramCollection.Add(new DBParameter("Rate", entity.Rate, DbType.Double));
            paramCollection.Add(new DBParameter("TaxRate", entity.TaxRate, DbType.Double));
            paramCollection.Add(new DBParameter("TaxAmount", entity.TaxAmount, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("DiscountPer", entity.DiscountPer, DbType.Double));
            paramCollection.Add(new DBParameter("MRP", (entity.MRP == null ? 0 : entity.MRP), DbType.Double));
            paramCollection.Add(new DBParameter("BatchName", entity.BatchName, DbType.String));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("lendingRate", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("Taxes", entity.Taxes, DbType.String));
            paramCollection.Add(new DBParameter("TaxRates", entity.TaxRates, DbType.Double));
            paramCollection.Add(new DBParameter("TransC", entity.TransC, DbType.Double));
            paramCollection.Add(new DBParameter("InstallC", entity.InstallC, DbType.Double));
            paramCollection.Add(new DBParameter("OtherC", entity.OtherC, DbType.Double));
            paramCollection.Add(new DBParameter("ServiceAmt", entity.ServiceAmt, DbType.Double));
            paramCollection.Add(new DBParameter("CustomDuty", entity.CustomDuty, DbType.Double));
            paramCollection.Add(new DBParameter("CQty", entity.CQty, DbType.Double));
            paramCollection.Add(new DBParameter("CFreeQty", entity.CFreeQty, DbType.Double));
            paramCollection.Add(new DBParameter("CRate", entity.CRate, DbType.Double));
            paramCollection.Add(new DBParameter("CMrp", entity.CMrp, DbType.Double));
            paramCollection.Add(new DBParameter("CBatch", entity.CBatch, DbType.String));
            paramCollection.Add(new DBParameter("IGST", entity.IGST, DbType.Double));
            paramCollection.Add(new DBParameter("CGST", entity.CGST, DbType.Double));
            paramCollection.Add(new DBParameter("UGST", entity.UGST, DbType.Double));
            paramCollection.Add(new DBParameter("SGST", entity.SGST, DbType.Double));
            paramCollection.Add(new DBParameter("RejectedQty", entity.RejectedQty, DbType.Double));
            paramCollection.Add(new DBParameter("RecieveQty", entity.RecieveQty, DbType.Double));
            paramCollection.Add(new DBParameter("RejectionReason", entity.RejectionReason, DbType.String));
            paramCollection.Add(new DBParameter("ConsigneeGrnDtlID", entity.ConSigneeGrnDtlID, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", grnentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", grnentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", grnentity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", grnentity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", grnentity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertGRNDetail, paramCollection, CommandType.StoredProcedure, "Id");
            if (entity.Taxes != null)
            {
                if (entity.Taxes.Trim() != "")
                {
                    paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("GrnDtlID", iResult, DbType.Int32));
                    paramCollection.Add(new DBParameter("GrnID", grnentity.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Taxes", entity.Taxes.ToString(), DbType.String));
                    dbHelper.ExecuteNonQuery(StoreQuery.InsertGRNTaxDetail, paramCollection, CommandType.StoredProcedure);
                }
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool UpdateEntry(GRNDetailEntity entity, DBHelper dbHelper)
        {
            throw new NotImplementedException();
        }

        public bool AuthCancelGRNDetail(GRNEntity grnentity, GRNDetailEntity dtentity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("GrnDtlID", dtentity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("GrnID", grnentity.ID, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemId", dtentity.ItemID, DbType.Int32));
            paramCollection.Add(new DBParameter("StoreId", grnentity.Storeid, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedQty", dtentity.Qty, DbType.Double));
            paramCollection.Add(new DBParameter("Authorised", dtentity.Authorised, DbType.Boolean));
            paramCollection.Add(new DBParameter("AuthorisedAmt", dtentity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("podtlid", 0, DbType.Int32));
            paramCollection.Add(new DBParameter("pPackSizeId", dtentity.PackSizeId, DbType.Int32));
            //paramCollection.Add(new DBParameter("Status", dtentity.Status, DbType.String));
            paramCollection.Add(new DBParameter("GrnTypeid", grnentity.GrnTypeID, DbType.Int32));
            //paramCollection.Add(new DBParameter("ConsgineeGrnDtlID", dtentity.ConSigneeGrnDtlID, DbType.Int32));
            paramCollection.Add(new DBParameter("TotalQty", dtentity.Qty, DbType.Double));
            paramCollection.Add(new DBParameter("AuthFreeQty", dtentity.FreeQty, DbType.Double));
            paramCollection.Add(new DBParameter("InsertedBy", grnentity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", grnentity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", grnentity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", grnentity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", grnentity.InsertedMacID, DbType.String));
            iResult = dbHelper.ExecuteNonQuery(StoreQuery.AuthCancelGRNDetails, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
