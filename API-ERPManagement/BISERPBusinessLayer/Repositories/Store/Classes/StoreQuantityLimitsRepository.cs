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
    public class StoreQuantityLimitsRepository : IStoreQuantityLimitsRepository
    {
        public IEnumerable<StoreQuantityLimitsEntity> GetAllItemLimits(int storeId, int ItemTypeId)
        {
            List<StoreQuantityLimitsEntity> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", storeId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemTypeId", ItemTypeId, DbType.Int32));
                DataTable dtUnits = dbHelper.ExecuteDataTable(StoreQuery.GetItemLimits, paramCollection, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new StoreQuantityLimitsEntity
                            {
                                ItemId = row.Field<int>("ItemId"),
                                ItemName = row.Field<string>("ItemName"),
                                UnitName = row.Field<string>("UnitName"),
                                MaxLevel = row.Field<double?>("MaxLevel"),
                                MinLevel = row.Field<double?>("MinLevel"),
                                ReOrderLevel = row.Field<double?>("ReOrderLevel"),
                                PackSizeId = row.Field<int?>("PackSizeId"),
                                Packsize = row.Field<string>("Packsize"),
                                storeId = row.Field<int?>("StoreID"),
                            }).ToList();
               
            }
            return units;
        }
        public int CreateQuantityLimits(StoreQuantityLimitsEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {                 
                
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("StoreID", entity.storeId, DbType.Int32));
                paramCollection.Add(new DBParameter("MinLevel", entity.MinLevel, DbType.Double));
                paramCollection.Add(new DBParameter("MaxLevel", entity.MaxLevel, DbType.Double));
                paramCollection.Add(new DBParameter("OPBalance", entity.OPBalance, DbType.Double));
                paramCollection.Add(new DBParameter("ReOrderLevel", entity.ReOrderLevel, DbType.Double));
                paramCollection.Add(new DBParameter("Packsize", entity.Packsize, DbType.String));
                paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));

                //paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.Ins_INV_ItemLimits, paramCollection, CommandType.StoredProcedure);
                //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertMaterialIndent, paramCollection, CommandType.StoredProcedure, "Indent_Id");
            }
            return iResult;
        }
        public IEnumerable<StoreQuantityLimitsEntity> GetNotificationQuantity(int UserId)
        {
            List<StoreQuantityLimitsEntity> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(StoreQuery.GetNotificationQuantity,param, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new StoreQuantityLimitsEntity
                            {
                                ItemId = row.Field<int>("ItemID"),
                                StoreName = row.Field<string>("StoreName"),
                                ItemName = row.Field<string>("ItemName"),
                                OPBalance = row.Field<double?>("QTY"),
                                MinLevel = row.Field<double?>("MinLevel"),
                                ReOrderLevel = row.Field<double?>("ReOrderLevel"),
                                storeId = row.Field<int?>("StoreID"),
                            }).ToList();

            }
            return units;
        }

        public IEnumerable<MasterReportEntity> GetMasterReport(int StoreId, DateTime? FromDate, DateTime? ToDate)
        {
            List<MasterReportEntity> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("FromDate", FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", ToDate, DbType.DateTime));
                DataTable dtUnits = dbHelper.ExecuteDataTable(StoreQuery.GetMasterReport, paramCollection, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new MasterReportEntity
                            {
                                GRNId = row.Field<int>("GRNId"),
                                GRNNo = row.Field<string>("GRNNo"),
                                GRNDate = row.Field<DateTime?>("GRNDate"),
                                strGRNDate = row.Field<DateTime?>("GRNDate").DateTimeFormat1(),
                                Storeid = row.Field<int>("StoreId"),
                                StoreName = row.Field<string>("StoreName"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ClientId = row.Field<int>("ClientId"),
                                ClientName = row.Field<string>("ClientName"),
                                ConsigneeId = row.Field<int>("ConsigneeId"),
                                InvoiceNo = row.Field<string>("InvoiceNo"),
                                ConsigneeName = row.Field<string>("ConsigneeName"),
                                InvoiceDate = row.Field<DateTime?>("InvoiceDate"),
                                strInvoiceDate = row.Field<DateTime?>("InvoiceDate").DateTimeFormat1(),
                                Address = row.Field<string>("Address"),
                                Contact = row.Field<string>("Contact"),
                                Email = row.Field<string>("Email"),
                                ItemID = row.Field<int>("ItemID"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemCategoryId = row.Field<int>("ItemCategoryId"),
                                ItemCategoryName = row.Field<string>("ItemCategoryName"),
                                GSTIN = row.Field<string>("GSTIN"),
                                StateName = row.Field<string>("StateName"),
                                CityName = row.Field<string>("CityName"),
                                PanNo = row.Field<string>("PanNo"),
                                PoID = row.Field<int>("PoID"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime?>("PODate"),
                                strPODate = row.Field<DateTime?>("PODate").DateTimeFormat1(),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                Transporter = row.Field<string>("Transporter"),
                                DCNo = row.Field<string>("DCNo"),
                                DCDate = row.Field<DateTime?>("DCDate"),
                                strDCDate = row.Field<DateTime?>("DCDate").DateTimeFormat1(),
                                Packsize = row.Field<string>("Packsize"),
                                UnitName = row.Field<string>("UnitName"),
                                HSNCode = row.Field<string>("HSNCode"),
                                Rate = row.Field<double>("Rate"),
                                Qty = row.Field<double>("Qty"),
                                Value = row.Field<double>("Value"),
                                OtherAmt = row.Field<double>("OtherAmt"),
                                Discount = row.Field<double>("Discount"),
                                TaxAmount = row.Field<double>("TaxAmount"),
                                TaxRate = row.Field<string>("TaxRate"),
                                RoundOffAmt = row.Field<double>("RoundOffAmt"),
                                GrossTotal = row.Field<double>("GrossTotal"),
                                PaymentTerm = row.Field<string>("PaymentTerm"),
                                DeliveryTerm = row.Field<string>("DeliveryTerm")
                            }).ToList();

            }
            return units;
        }
        public IEnumerable<ProjectBudgetConclusion> GetProjectBudgetConclusion(int StoreId, DateTime? FromDate, DateTime? ToDate)
        {
            List<ProjectBudgetConclusion> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("FromDate", FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", ToDate, DbType.DateTime));
                DataTable dtUnits = dbHelper.ExecuteDataTable(StoreQuery.GetProjectBudgetConclusion, paramCollection, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new ProjectBudgetConclusion
                            {
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ProjectName = row.Field<string>("ProjectName"),
                                ProjectId = row.Field<int>("ProjectId"),
                                PONo = row.Field<string>("PONo"),
                                SupplierName = row.Field<string>("SupplierName"),
                                VendorName = row.Field<string>("VendorName"),
                                ItemCategory = row.Field<string>("ItemCategory"),
                                PoDate = row.Field<string>("PoDate"),
                                BasicAmount = row.Field<double>("BasicAmount"),
                                GSTAmount = row.Field<double>("GSTAmount"),
                                GrandTotal = row.Field<double>("GrandTotal"),
                                PoStatus = row.Field<string>("PoStatus"),
                                ClosedAmount = row.Field<double>("ClosedAmount"),
                            }).ToList();

            }
            return units;
        }
    }
}
