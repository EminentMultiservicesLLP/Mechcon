using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;


namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class ItemMasterRepository : IItemMasterRepository, IItemSupplierMappingRepository
    {
        private static readonly ILogger _loggger = Logger.Register(typeof(ItemMasterRepository));
        public ItemMasterEntities GetItemById(int Id)
        {
            ItemMasterEntities item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ItemId", Id, DbType.Int32);
                DataTable dtItem = dbHelper.ExecuteDataTable(MasterQueries.GetItemMasterById, param, CommandType.StoredProcedure);

                item = dtItem.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                VatOn = row.Field<string>("VatOn"),
                                SalesTax = row.Field<double?>("SalesTax"),
                                SalesPrice = row.Field<double?>("SalesPrice"),
                                StandardRate = row.Field<double?>("StandardRate"),
                                SealingRate = row.Field<double?>("SealingRate"),
                                OwnStock = row.Field<bool>("OwnStock"),
                                NoMrp = row.Field<bool>("NoMrp"),
                                AutoConsumed = row.Field<bool>("AutoConsumed"),
                                Asset = row.Field<bool>("Asset"),
                                OPBalance = row.Field<double?>("OPBalance"),
                                MaxLevel = row.Field<double?>("MaxLevel"),
                                MinLevel = row.Field<double?>("MinLevel"),
                                BrandId = row.Field<int?>("BrandId"),
                                Notes = row.Field<string>("Notes"),
                                IsLocalPurchaseItem = row.Field<bool>("IsLocalPurchaseItem"),
                                IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                SeasonDate = row.Field<DateTime?>("SeasonDate"),
                                strSeasonDate = row.Field<DateTime?>("SeasonDate").DateTimeFormat1(),
                                SalesTaxID = row.Field<int?>("SalesTaxID"),
                                OrderUnitID = row.Field<int?>("OrderUnitID"),
                                ReOrderLevel = row.Field<double?>("ReOrderLevel"),
                                CurrentQty = row.Field<double?>("CurrentQty"),
                                UnitID = row.Field<int?>("UnitID"),
                                BranchID = row.Field<int?>("BranchID"),
                                StockTypeID = row.Field<int?>("StockTypeID"),
                                BatchRequired = row.Field<bool>("BatchRequired"),
                                ExpiryDtRequired = row.Field<bool>("ExpiryDtRequired"),
                                PurchaseFrequency = row.Field<string>("PurchaseFrequency"),
                                ItemChangeID = row.Field<int?>("ItemChangeID"),
                                Taxid = row.Field<int?>("Taxid"),
                                ChkLooseSelling = row.Field<bool>("ChkLooseSelling"),
                                chkQualityCtrl = row.Field<bool>("chkQualityCtrl"),
                                IsNonChargable = row.Field<bool>("IsNonChargable"),
                                MarkupPercentage = row.Field<int?>("MarkupPercentage"),
                                ConsigneeFlag = row.Field<bool>("ConsigneeFlag"),
                                ConsigneeId = row.Field<int?>("ConsigneeId"),
                                HighValueFlag = row.Field<bool>("HighValueFlag"),
                                ScheduledItem = row.Field<bool>("ScheduledItem"),
                                IsNoBarcode = row.Field<bool>("IsNoBarcode"),
                                ScheduledIVItemId = row.Field<int?>("ScheduledIVItemId"),
                                TaxOnPurchase = row.Field<decimal?>("TaxOnPurchase"),
                                TaxOnSale = row.Field<decimal?>("TaxOnSale"),
                                PurchaseRate = row.Field<decimal?>("PurchaseRate"),
                                MRP = row.Field<double?>("MRP"),
                                Deactive = row.Field<bool>("Deactive"),
                                VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                HSNCode = row.Field<string>("HSNCode"),
                                UnIndentItem = row.Field<bool>("UnIndentItem")
                            }).FirstOrDefault();
            }
            return item;
        }

        public IEnumerable<ItemsModel> GetAllItems()
        {
            List<ItemsModel> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetItems, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemsModel
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),                              
                                itemtypename= row.Field<string>("itemtypename"),
                                Asset = row.Field<bool>("Asset"),
                                Service = row.Field<bool>("IsService"),
                                PackingList = row.Field<bool>("PackingList"),
                                Deactive = row.Field<bool>("Deactive")

                            }).ToList();
                
            }
            return item;
        }

        public IEnumerable<ItemMasterEntities> NEWGetActiveItems()
        {
            List<ItemMasterEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("Deactive", 0, DbType.Int32);
                DataTable dtItems = dbHelper.ExecuteDataTable(MasterQueries.GetAllItemMaster, param, CommandType.StoredProcedure);
                item = dtItems.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                VatOn = row.Field<string>("VatOn"),
                                SalesTax = row.Field<double?>("SalesTax"),
                                SalesPrice = row.Field<double?>("SalesPrice"),
                                StandardRate = row.Field<double?>("StandardRate"),
                                SealingRate = row.Field<double?>("SealingRate"),
                                OwnStock = row.Field<bool>("OwnStock"),
                                NoMrp = row.Field<bool>("NoMrp"),
                                AutoConsumed = row.Field<bool>("AutoConsumed"),
                                Asset = row.Field<bool>("Asset"),
                                OPBalance = row.Field<double?>("OPBalance"),
                                MaxLevel = row.Field<double?>("MaxLevel"),
                                MinLevel = row.Field<double?>("MinLevel"),
                                BrandId = row.Field<int?>("BrandId"),
                                Notes = row.Field<string>("Notes"),
                                IsLocalPurchaseItem = row.Field<bool>("IsLocalPurchaseItem"),
                                IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                SeasonDate = row.Field<DateTime?>("SeasonDate"),
                                strSeasonDate = row.Field<DateTime?>("SeasonDate").DateTimeFormat1(),
                                SalesTaxID = row.Field<int?>("SalesTaxID"),
                                OrderUnitID = row.Field<int?>("OrderUnitID"),
                                ReOrderLevel = row.Field<double?>("ReOrderLevel"),
                                CurrentQty = row.Field<double?>("CurrentQty"),
                                UnitID = row.Field<int?>("UnitID"),
                                BranchID = row.Field<int?>("BranchID"),
                                StockTypeID = row.Field<int?>("StockTypeID"),
                                BatchRequired = row.Field<bool>("BatchRequired"),
                                ExpiryDtRequired = row.Field<bool>("ExpiryDtRequired"),
                                PurchaseFrequency = row.Field<string>("PurchaseFrequency"),
                                ItemChangeID = row.Field<int?>("ItemChangeID"),
                                Taxid = row.Field<int?>("Taxid"),
                                ChkLooseSelling = row.Field<bool>("ChkLooseSelling"),
                                chkQualityCtrl = row.Field<bool>("chkQualityCtrl"),
                                IsNonChargable = row.Field<bool>("IsNonChargable"),
                                MarkupPercentage = row.Field<int?>("MarkupPercentage"),
                                ConsigneeFlag = row.Field<bool>("ConsigneeFlag"),
                                ConsigneeId = row.Field<int?>("ConsigneeId"),
                                HighValueFlag = row.Field<bool>("HighValueFlag"),
                                ScheduledItem = row.Field<bool>("ScheduledItem"),
                                IsNoBarcode = row.Field<bool>("IsNoBarcode"),
                                ScheduledIVItemId = row.Field<int?>("ScheduledIVItemId"),
                                TaxOnPurchase = row.Field<decimal?>("TaxOnPurchase"),
                                TaxOnSale = row.Field<decimal?>("TaxOnSale"),
                                PurchaseRate = row.Field<decimal?>("PurchaseRate"),
                                MRP = row.Field<double?>("MRP"),
                                Deactive = row.Field<bool>("Deactive"),
                                LastPORate = row.Field<double?>("LastPORate"),
                                UnitName = row.Field<string>("UnitName"),
                                PackSize = row.Field<string>("PackSize"),
                                VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                UnIndentItem = row.Field<bool>("UnIndentItem"),
                                HSNCode = row.Field<string>("HSNCode"),
                                 Service = row.Field<bool>("IsService,"),
                            }).ToList();
            }
            return item;
        }

        public IEnumerable<ItemMasterEntities> GetActiveItems(int StoreId, int CategoryId)
        {
            List<ItemMasterEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("CategoryId", CategoryId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetItems, paramCollection, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                itemtypename = row.Field<string>("itemtypename"),
                                Asset = row.Field<bool>("Asset"),
                                Service = row.Field<bool>("IsService"),
                                PackingList = row.Field<bool>("PackingList"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return item;
        }

        public IEnumerable<ItemMasterEntities> GetStoreItems(int StoreId)
        {
            List<ItemMasterEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                //DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetAllStoreItemMaster, param, CommandType.StoredProcedure);// Changed on 06.10.2016 by Vikram
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetAllItemMaster, paramCollection, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                StandardRate = row.Field<double?>("StandardRate"),                                
                                CurrentQty = row.Field<double?>("CurrentQty"),
                                BatchRequired = row.Field<bool>("BatchRequired"),
                                ExpiryDtRequired = row.Field<bool>("ExpiryDtRequired"),
                                chkQualityCtrl = row.Field<bool>("ChkQualityCtrl"),
                                MRP = row.Field<double?>("MRP"),
                                OrderingUnit = row.Field<string>("UnitName"),
                                UnitID = row.Field<int?>("UnitID"),
                                UnitName = row.Field<string>("UnitName"),
                                PackSize = row.Field<string>("PackSize"),
                                NoMrp = row.Field<bool>("NoMrp"),
                                Asset = row.Field<bool>("Asset"),
                                Deactive = row.Field<bool>("Deactive"),
                                AutoConsumed = row.Field<bool>("AutoConsumed"),
                                IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                UnIndentItem = row.Field<bool>("UnIndentItem"),
                                itemtypename = row.Field<string>("itemtypename"),
                                PurchaseRate = row.Field<decimal?>("PurchaseRate"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                HSNCode = row.Field<string>("HSNCode"),
                                //BatchName = row.Field<string>("BatchName"),
                                //BatchId = row.Field<int>("Batchid"),

                            }).ToList();
            }
            return item;
        }

        public IEnumerable<ItemMasterEntities> GetStoreStock(int StoreId)
        {
            List<ItemMasterEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetStoreItemStock, param, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ItemID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("ItemName"),
                                UnitName = row.Field<string>("UnitName"),
                                PackSize = row.Field<string>("PackSize"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                //ItemTypeID = row.Field<int>("ItemTypeID"),
                                BatchId = row.Field<int>("BatchId"),
                                CurrentQty = row.Field<double>("Qty"),
                                MRP = row.Field<double>("MRP"),
                                BatchName = row.Field<string>("BatchName"),
                                HSNCode = row.Field<string>("HSNCode"),
                                NoMrp = false,
                                Asset = false,
                                Deactive = false,
                                AutoConsumed = false,
                                BatchRequired = false,
                                ExpiryDtRequired = false,
                                IsRoutineItem = false,
                                VENDORFLAG = false,
                                UnIndentItem = false
                            }).ToList();
            }
            return item;
        }

        public IEnumerable<ItemMasterEntities> GetStorewiseVendorItems(int StoreId)
        {
            List<ItemMasterEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.StoreVendorItems, param, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ItemID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("ItemName"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                PackSize = row.Field<string>("PackSize"),
                                UnitName = row.Field<string>("UnitName"),
                                BatchId = row.Field<int>("BatchId"),
                                BatchName = row.Field<string>("BatchName"),
                                ExpiryDate = row.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                                CurrentQty = row.Field<double>("Qty"),
                                MRP = row.Field<double>("MRP"),
                                NoMrp = false,
                                Asset = false,
                                Deactive = false,
                                AutoConsumed = false,
                                BatchRequired = false,
                                ExpiryDtRequired = false,
                                IsRoutineItem = false,
                                VENDORFLAG = false,
                                UnIndentItem = false
                            }).ToList();
            }
            return item;
        }

        public IEnumerable<ItemMasterEntities> StoreItemsOPBal(int StoreId)
        {
            List<ItemMasterEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetStoreItems, param, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("ItemName"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                itemtypename = row.Field<string>("itemtypename"),
                                //BatchName = row.Field<string>("BatchName"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                StandardRate = row.Field<double?>("Rate"),
                                //CurrentQty = row.Field<double?>("CurrentQty"),
                                BatchRequired = row.Field<bool>("BatchReq"),
                                ExpiryDtRequired = row.Field<bool>("ExpReq"),
                                chkQualityCtrl = row.Field<bool>("Chkqualityctrl"),
                                MRP = row.Field<double?>("MRP"),
                                UnitID = row.Field<int?>("UnitID"),
                                UnitName = row.Field<string>("UnitName"),
                                PackSize = row.Field<string>("PackSize"),
                                NoMrp = row.Field<bool>("NoMrp"),
                                Asset = row.Field<bool>("Asset"),
                                Deactive = row.Field<bool>("Deactive"),
                                AutoConsumed = row.Field<bool>("AutoConsumed"),
                                IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                UnIndentItem = row.Field<bool>("UnIndentItem")
                            }).ToList();
            }
            return item;
        }

        public int CreateItemMaster(ItemMasterEntities entity)
        {
            int iResult = 0;
            List<int> supplierid = new List<int>();
            List<int> vendoritemid = new List<int>();
            if (entity.suppliers != null)
            {
                supplierid = (from e in entity.suppliers
                              select e.ID).ToList();
            }
            if (entity.vendorItems != null)
            {
                vendoritemid = (from e in entity.vendorItems
                                select e.ID).ToList();
            }
            StringBuilder suppbuilder = new StringBuilder();
            StringBuilder vendoritembuilder = new StringBuilder();
            foreach (int s in supplierid)
            {
                suppbuilder.Append(s).Append(",");
            }
            foreach (int s in vendoritemid)
            {
                vendoritembuilder.Append(s).Append(",");
            }
            string supplresult = suppbuilder.ToString().TrimEnd(',');
            string vendoritemresult = vendoritembuilder.ToString().TrimEnd(',');
            DBHelper dbHelper = new DBHelper();
            IDbTransaction transaction = dbHelper.BeginTransaction();
            TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                    paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
                    paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));
                    paramCollection.Add(new DBParameter("ItemTypeID", entity.ItemTypeID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ItemGroupID", entity.ItemGroupID, DbType.Int32));
                    paramCollection.Add(new DBParameter("PackSizeID", entity.PackSizeID, DbType.Int32));
                    paramCollection.Add(new DBParameter("VatOn", entity.VatOn, DbType.String));
                    paramCollection.Add(new DBParameter("SalesTax", entity.SalesTax, DbType.Double));
                    paramCollection.Add(new DBParameter("SalesPrice", entity.SalesPrice, DbType.Double));
                    paramCollection.Add(new DBParameter("StandardRate", entity.StandardRate, DbType.Double));
                    paramCollection.Add(new DBParameter("SealingRate", entity.SealingRate, DbType.Double));
                    paramCollection.Add(new DBParameter("OwnStock", entity.OwnStock, DbType.Boolean));
                    paramCollection.Add(new DBParameter("NoMrp", entity.NoMrp, DbType.Boolean));
                    paramCollection.Add(new DBParameter("AutoConsumed", entity.AutoConsumed, DbType.Boolean));
                    paramCollection.Add(new DBParameter("Asset", entity.Asset, DbType.Boolean));
                    paramCollection.Add(new DBParameter("OPBalance", entity.OPBalance, DbType.Double));
                    paramCollection.Add(new DBParameter("MaxLevel", entity.MaxLevel, DbType.Double));
                    paramCollection.Add(new DBParameter("MinLevel", entity.MinLevel, DbType.Double));
                    paramCollection.Add(new DBParameter("BrandId", entity.BrandId, DbType.Int32));
                    paramCollection.Add(new DBParameter("Notes", entity.Notes, DbType.String));
                    paramCollection.Add(new DBParameter("IsLocalPurchaseItem", entity.IsLocalPurchaseItem, DbType.Boolean));
                    paramCollection.Add(new DBParameter("IsRoutineItem", entity.IsRoutineItem, DbType.Boolean));
                    paramCollection.Add(new DBParameter("SeasonDate", entity.SeasonDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("SalesTaxID", entity.SalesTaxID, DbType.Int32));
                    paramCollection.Add(new DBParameter("OrderUnitID", entity.OrderUnitID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ReOrderLevel", entity.ReOrderLevel, DbType.Double));
                    paramCollection.Add(new DBParameter("CurrentQty", entity.CurrentQty, DbType.Double));
                    paramCollection.Add(new DBParameter("UnitID", entity.UnitID, DbType.Int32));
                    paramCollection.Add(new DBParameter("BranchID", entity.BranchID, DbType.Int32));
                    paramCollection.Add(new DBParameter("StockTypeID", entity.StockTypeID, DbType.Int32));
                    paramCollection.Add(new DBParameter("BatchRequired", entity.BatchRequired, DbType.Boolean));
                    paramCollection.Add(new DBParameter("ExpiryDtRequired", entity.ExpiryDtRequired, DbType.Boolean));
                    paramCollection.Add(new DBParameter("VENDORFLAG", entity.VENDORFLAG, DbType.Boolean));//sflag
                    paramCollection.Add(new DBParameter("UnIndentItem", entity.UnIndentItem, DbType.Boolean));
                    paramCollection.Add(new DBParameter("PurchaseFrequency", entity.PurchaseFrequency, DbType.String));
                    paramCollection.Add(new DBParameter("ItemChangeID", entity.ItemChangeID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Taxid", entity.Taxid, DbType.Int32));
                    paramCollection.Add(new DBParameter("ChkLooseSelling", entity.ChkLooseSelling, DbType.Boolean));
                    paramCollection.Add(new DBParameter("ChkQualityCtrl", entity.chkQualityCtrl, DbType.Boolean));
                    paramCollection.Add(new DBParameter("IsNonChargable", entity.IsNonChargable, DbType.Boolean));
                    paramCollection.Add(new DBParameter("MarkupPercentage", entity.MarkupPercentage, DbType.Int32));
                    paramCollection.Add(new DBParameter("ConsigneeFlag", entity.ConsigneeFlag, DbType.Boolean));
                    paramCollection.Add(new DBParameter("ConsigneeId", entity.ConsigneeId, DbType.Int32));
                    paramCollection.Add(new DBParameter("HighValueFlag", entity.HighValueFlag, DbType.Boolean));
                    paramCollection.Add(new DBParameter("scheduledItem", entity.ScheduledItem, DbType.Boolean));
                    paramCollection.Add(new DBParameter("isNoBarcode", entity.IsNoBarcode, DbType.Boolean));
                    paramCollection.Add(new DBParameter("ScheduledIVItemId", entity.ScheduledIVItemId, DbType.Int32));
                    paramCollection.Add(new DBParameter("TaxOnPurchase", entity.TaxOnPurchase, DbType.Decimal));
                    paramCollection.Add(new DBParameter("TaxOnSale", entity.TaxOnSale, DbType.Decimal));
                    paramCollection.Add(new DBParameter("PurchaseRate", entity.PurchaseRate, DbType.Decimal));
                    paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Decimal));
                    paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                    paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                    paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("HSNCode", entity.HSNCode, DbType.String));
                    paramCollection.Add(new DBParameter("IsService", entity.Service, DbType.Boolean));
                    paramCollection.Add(new DBParameter("IsPackingList", entity.PackingList, DbType.Boolean));
                    paramCollection.Add(new DBParameter("ItemNumber", entity.ItemNumber,DbType.Int32));
                    paramCollection.Add(new DBParameter("ItemCategory", entity.ItemCategory,DbType.String));
                    paramCollection.Add(new DBParameter("TaxRate", entity.TaxRate,DbType.Double));
                    paramCollection.Add(new DBParameter("ScrapItem", entity.ScrapItem,DbType.Boolean));
                    paramCollection.Add(new DBParameter("SaleItem", entity.SaleItem,DbType.Boolean));
                    paramCollection.Add(new DBParameter("InventoryItem", entity.InventoryItem,DbType.Boolean));
                    paramCollection.Add(new DBParameter("PurchaseItem", entity.PurchaseItem,DbType.Boolean));
                    paramCollection.Add(new DBParameter("Make", entity.Make,DbType.String));
                    paramCollection.Add(new DBParameter("MaterialOfConstruct", entity.MaterialOfConstruct,DbType.String));
                    iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertItemMaster, paramCollection, CommandType.StoredProcedure, "ID");

                    if (supplierid.Count > 0)
                    {
                        paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("ItemId", iResult, DbType.Int32));
                        paramCollection.Add(new DBParameter("SupplierIds", supplresult.ToString(), DbType.String));
                        paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                        dbHelper.ExecuteNonQuery(MasterQueries.InsertItemMasterSupplier, paramCollection, CommandType.StoredProcedure);
                    }
                    if (vendoritemid.Count > 0)
                    {
                        paramCollection = new DBParameterCollection();
                        paramCollection.Add(new DBParameter("ItemId", iResult, DbType.Int32));
                        paramCollection.Add(new DBParameter("VendorItemIds", vendoritemresult.ToString(), DbType.String));
                        dbHelper.ExecuteNonQuery(MasterQueries.InsertVendorItems, paramCollection, CommandType.StoredProcedure);
                    }
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in CreateItem method of ItemMasterRepository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            return iResult;
        }

        public bool UpdateItemMaster(ItemMasterEntities entity)
        {
            int iResult = 0;
            List<int> supplierid = new List<int>();
            List<int> vendoritemid = new List<int>();
            if (entity.suppliers != null)
            {
                supplierid = (from e in entity.suppliers
                              select e.ID).ToList();
            }
            if (entity.vendorItems != null)
            {
                vendoritemid = (from e in entity.vendorItems
                                select e.ID).ToList();
            }
            StringBuilder suppbuilder = new StringBuilder();
            StringBuilder vendoritembuilder = new StringBuilder();
            foreach (int s in supplierid)
            {
                suppbuilder.Append(s).Append(",");
            }
            foreach (int s in vendoritemid)
            {
                vendoritembuilder.Append(s).Append(",");
            }
            string supplresult = suppbuilder.ToString().TrimEnd(',');
            string vendoritemresult = vendoritembuilder.ToString().TrimEnd(',');
            DBHelper dbHelper = new DBHelper();
            IDbTransaction transaction = dbHelper.BeginTransaction();
            TryCatch.Run(() =>
                {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("Name", entity.Name, DbType.String));
                paramCollection.Add(new DBParameter("DescriptiveName", entity.DescriptiveName, DbType.String));

                paramCollection.Add(new DBParameter("ItemTypeID", entity.ItemTypeID, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemGroupID", entity.ItemGroupID, DbType.Int32));
                paramCollection.Add(new DBParameter("PackSizeID", entity.PackSizeID, DbType.Int32));
                paramCollection.Add(new DBParameter("VatOn", entity.VatOn, DbType.String));
                paramCollection.Add(new DBParameter("SalesTax", entity.SalesTax, DbType.Double));
                paramCollection.Add(new DBParameter("SalesPrice", entity.SalesPrice, DbType.Double));
                paramCollection.Add(new DBParameter("StandardRate", entity.StandardRate, DbType.Double));
                paramCollection.Add(new DBParameter("SealingRate", entity.SealingRate, DbType.Double));
                paramCollection.Add(new DBParameter("OwnStock", entity.OwnStock, DbType.Boolean));
                paramCollection.Add(new DBParameter("NoMrp", entity.NoMrp, DbType.Boolean));
                paramCollection.Add(new DBParameter("AutoConsumed", entity.AutoConsumed, DbType.Boolean));
                paramCollection.Add(new DBParameter("Asset", entity.Asset, DbType.Boolean));
                paramCollection.Add(new DBParameter("OPBalance", entity.OPBalance, DbType.Double));
                paramCollection.Add(new DBParameter("MaxLevel", entity.MaxLevel, DbType.Double));
                paramCollection.Add(new DBParameter("MinLevel", entity.MinLevel, DbType.Double));
                paramCollection.Add(new DBParameter("BrandId", entity.BrandId, DbType.Int32));
                paramCollection.Add(new DBParameter("Notes", entity.Notes, DbType.String));
                paramCollection.Add(new DBParameter("IsLocalPurchaseItem", entity.IsLocalPurchaseItem, DbType.Boolean));
                paramCollection.Add(new DBParameter("IsRoutineItem", entity.IsRoutineItem, DbType.Boolean));
                paramCollection.Add(new DBParameter("SeasonDate", entity.SeasonDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("SalesTaxID", entity.SalesTaxID, DbType.Int32));
                paramCollection.Add(new DBParameter("OrderUnitID", entity.OrderUnitID, DbType.Int32));
                paramCollection.Add(new DBParameter("ReOrderLevel", entity.ReOrderLevel, DbType.Double));
                paramCollection.Add(new DBParameter("CurrentQty", entity.CurrentQty, DbType.Double));
                paramCollection.Add(new DBParameter("UnitID", entity.UnitID, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchID", entity.BranchID, DbType.Int32));
                paramCollection.Add(new DBParameter("StockTypeID", entity.StockTypeID, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchRequired", entity.BatchRequired, DbType.Boolean));
                paramCollection.Add(new DBParameter("VENDORFLAG", entity.VENDORFLAG, DbType.Boolean));
                paramCollection.Add(new DBParameter("UnIndentItem", entity.UnIndentItem, DbType.Boolean));
                paramCollection.Add(new DBParameter("ExpiryDtRequired", entity.ExpiryDtRequired, DbType.Boolean));
                paramCollection.Add(new DBParameter("PurchaseFrequency", entity.PurchaseFrequency, DbType.String));
                paramCollection.Add(new DBParameter("ItemChangeID", entity.ItemChangeID, DbType.Int32));
                paramCollection.Add(new DBParameter("Taxid", entity.Taxid, DbType.Int32));
                paramCollection.Add(new DBParameter("ChkLooseSelling", entity.ChkLooseSelling, DbType.Boolean));
                paramCollection.Add(new DBParameter("ChkQualityCtrl", entity.chkQualityCtrl, DbType.Boolean));
                paramCollection.Add(new DBParameter("IsNonChargable", entity.IsNonChargable, DbType.Boolean));
                paramCollection.Add(new DBParameter("MarkupPercentage", entity.MarkupPercentage, DbType.Int32));
                paramCollection.Add(new DBParameter("ConsigneeFlag", entity.ConsigneeFlag, DbType.Boolean));
                paramCollection.Add(new DBParameter("ConsigneeId", entity.ConsigneeId, DbType.Int32));
                paramCollection.Add(new DBParameter("HighValueFlag", entity.HighValueFlag, DbType.Boolean));
                paramCollection.Add(new DBParameter("scheduledItem", entity.ScheduledItem, DbType.Boolean));
                paramCollection.Add(new DBParameter("isNoBarcode", entity.IsNoBarcode, DbType.Boolean));
                paramCollection.Add(new DBParameter("ScheduledIVItemId", entity.ScheduledIVItemId, DbType.Int32));
                paramCollection.Add(new DBParameter("TaxOnPurchase", entity.TaxOnPurchase, DbType.Decimal));
                paramCollection.Add(new DBParameter("TaxOnSale", entity.TaxOnSale, DbType.Decimal));
                paramCollection.Add(new DBParameter("PurchaseRate", entity.PurchaseRate, DbType.Decimal));
                paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Decimal));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("IsService", entity.Service, DbType.Boolean));
                paramCollection.Add(new DBParameter("IsPackingList", entity.PackingList, DbType.Boolean));
                paramCollection.Add(new DBParameter("HSNCode", entity.HSNCode, DbType.String));
                paramCollection.Add(new DBParameter("ItemNumber", entity.ItemNumber, DbType.Int32));                    // new addition 2024
                paramCollection.Add(new DBParameter("ItemCategory", entity.ItemCategory, DbType.String));
                paramCollection.Add(new DBParameter("TaxRate", entity.TaxRate, DbType.Double));
                paramCollection.Add(new DBParameter("ScrapItem", entity.ScrapItem, DbType.Boolean));
                paramCollection.Add(new DBParameter("SaleItem", entity.SaleItem, DbType.Boolean));
                paramCollection.Add(new DBParameter("InventoryItem", entity.InventoryItem, DbType.Boolean));
                paramCollection.Add(new DBParameter("PurchaseItem", entity.PurchaseItem, DbType.Boolean));
                paramCollection.Add(new DBParameter("Make", entity.Make, DbType.String));
                paramCollection.Add(new DBParameter("MaterialOfConstruct", entity.MaterialOfConstruct, DbType.String));
                    iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateItemMasterById, paramCollection, CommandType.StoredProcedure);

                if (supplierid.Count > 0)
                {
                    paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ItemId", entity.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("SupplierIds", supplresult.ToString(), DbType.String));
                    paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                    dbHelper.ExecuteNonQuery(MasterQueries.InsertItemMasterSupplier, paramCollection, CommandType.StoredProcedure);
                }
                if (vendoritemid.Count > 0)
                {
                    paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ItemId", entity.ID, DbType.Int32));
                    paramCollection.Add(new DBParameter("VendorItemIds", vendoritemresult.ToString(), DbType.String));
                    dbHelper.ExecuteNonQuery(MasterQueries.InsertVendorItems, paramCollection, CommandType.StoredProcedure);
                }
                dbHelper.CommitTransaction(transaction);
            }).IfNotNull(ex =>
            {
                dbHelper.RollbackTransaction(transaction);
                _loggger.LogError("Error in CreateItem method of ItemMasterRepository : parameter :" + Environment.NewLine + ex.StackTrace);
            });
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool DeleteItemMaster(ItemMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UnitID", entity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIP", entity.UpdatedIPAddress, DbType.String));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.DeleteUnitMasterById, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool CheckDuplicateItem( string name, string description)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Item", DbType.String));
                paramCollection.Add(new DBParameter("Code", description, DbType.String));
                paramCollection.Add(new DBParameter("Name", name, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public List<SupplierMasterEntities> GetItemSupplierMappingByItemId(int Id)
        {
            List<SupplierMasterEntities> isupplier = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ItemId", Id, DbType.Int32);
                DataTable dtSupplier = dbHelper.ExecuteDataTable(MasterQueries.GetItemSupplierMappingByItemId, param, CommandType.StoredProcedure);
                isupplier = dtSupplier.AsEnumerable()
                            .Select(row => new SupplierMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                Address = row.Field<string>("Address"),
                                Street = row.Field<string>("Street"),
                                Society = row.Field<string>("Society"),
                                Landmark = row.Field<string>("landmark"),
                                Village = row.Field<string>("Village"),
                                City = row.Field<int?>("City"),
                                Pin = row.Field<string>("Pin"),
                                Country = row.Field<int?>("Country"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactDesignation = row.Field<string>("ContactDesignation"),
                                GroupID = row.Field<int?>("GroupID"),
                                CreditPeriod = row.Field<int?>("CreditPeriod"),
                                DateOfAssociation = row.Field<DateTime>("DateOfAssociation"),
                                Fax = row.Field<string>("Fax"),
                                Phone1 = row.Field<string>("Phone1"),
                                Phone2 = row.Field<string>("Phone2"),
                                CellPhone = row.Field<string>("CellPhone"),
                                Email = row.Field<string>("Email"),
                                Web = row.Field<string>("Web"),
                                CST = row.Field<string>("CST"),
                                MST = row.Field<string>("MST"),
                                TDS = row.Field<string>("TDS"),
                                ExciseCode = row.Field<string>("ExciseCode"),
                                ExportCode = row.Field<string>("ExportCode"),
                                LedgerID = row.Field<int?>("LedgerID"),
                                EligableForAdv = row.Field<bool>("EligableForAdv"),
                                BankName = row.Field<string>("BankName"),
                                BankBranch = row.Field<string>("BankBranch"),
                                BankAcNo = row.Field<string>("BankAcNo"),
                                Note = row.Field<string>("Note"),
                                Proposed = row.Field<string>("Proposed"),
                                IncomeTaxNo = row.Field<string>("IncomeTaxNo"),
                                SuppType = row.Field<string>("SuppType"),
                                AccountId = row.Field<int?>("AccountId"),
                                Paytermsid = row.Field<int?>("Paytermsid"),
                                RTGSCODE = row.Field<string>("RTGSCODE"),
                                SupplierCategory = row.Field<int?>("SupplierCategory"),
                                SupplierType = row.Field<int?>("SupplierType"),
                                VATTINNo = row.Field<string>("VATTINNo"),
                                ServiceTaxNo = row.Field<string>("ServiceTaxNo"),
                                PANNo = row.Field<string>("PANNo"),
                                Deactive = row.Field<bool?>("Deactive")
                            }).ToList();
            }
            return isupplier;
        }

        public List<ItemMasterEntities> GetVendorItemsByItemId(int Id)
        {
            List<ItemMasterEntities> items = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ItemId", Id, DbType.Int32);
                DataTable dtItem = dbHelper.ExecuteDataTable(MasterQueries.GetVendorItems, param, CommandType.StoredProcedure);
                items = dtItem.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                OwnStock = row.Field<bool>("OwnStock"),
                                NoMrp = row.Field<bool>("NoMrp"),
                                AutoConsumed = row.Field<bool>("AutoConsumed"),
                                Asset = row.Field<bool>("Asset"),
                                IsLocalPurchaseItem = row.Field<bool>("IsLocalPurchaseItem"),
                                IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                BatchRequired = row.Field<bool>("BatchRequired"),
                                ExpiryDtRequired = row.Field<bool>("ExpiryDtRequired"),
                                ChkLooseSelling = row.Field<bool>("ChkLooseSelling"),
                                chkQualityCtrl = row.Field<bool>("chkQualityCtrl"),
                                IsNonChargable = row.Field<bool>("IsNonChargable"),
                                ConsigneeFlag = row.Field<bool>("ConsigneeFlag"),
                                HighValueFlag = row.Field<bool>("HighValueFlag"),
                                ScheduledItem = row.Field<bool>("ScheduledItem"),
                                IsNoBarcode = row.Field<bool>("IsNoBarcode"),
                                Deactive = row.Field<bool>("Deactive"),
                                VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                UnIndentItem = row.Field<bool>("UnIndentItem")
                            }).ToList();
            }
            return items;
        }

        public List<ItemMasterEntities> GetVendorItemsByItemIds(List<int> ItemIds)
        {
            List<ItemMasterEntities> items = new List<ItemMasterEntities>();
            string vendoritemresult = "";
            if(ItemIds != null)
            {
                List<int> vendoritemid = new List<int>();
                StringBuilder vendoritembuilder = new StringBuilder();
                foreach (int s in ItemIds)
                {
                    vendoritembuilder.Append(s).Append(",");
                }
                vendoritemresult = vendoritembuilder.ToString().TrimEnd(',');
            }
            if (vendoritemresult != "")
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DBParameter param = new DBParameter("ItemIds", vendoritemresult, DbType.String);
                    DataTable dtItem = dbHelper.ExecuteDataTable(MasterQueries.GetVendorItemsByItemId, param, CommandType.StoredProcedure);
                    items = dtItem.AsEnumerable()
                                .Select(row => new ItemMasterEntities
                                {
                                    ID = row.Field<int>("ID"),
                                    Code = row.Field<string>("Code"),
                                    Name = row.Field<string>("Name"),
                                    UnitName = row.Field<string>("UnitName"),
                                    PackSize = row.Field<string>("PackSize"),
                                    MRP = row.Field<double?>("MRP"),
                                    StandardRate = row.Field<double?>("StandardRate"),
                                    ItemTypeID = row.Field<int>("ItemTypeID"),
                                    PackSizeID = row.Field<int>("PackSizeID"),
                                    MarkupPercentage = row.Field<int?>("MarkupPercentage"),
                                    OwnStock = row.Field<bool>("OwnStock"),
                                    NoMrp = row.Field<bool>("NoMrp"),
                                    AutoConsumed = row.Field<bool>("AutoConsumed"),
                                    Asset = row.Field<bool>("Asset"),
                                    IsLocalPurchaseItem = row.Field<bool>("IsLocalPurchaseItem"),
                                    IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                    BatchRequired = row.Field<bool>("BatchRequired"),
                                    ExpiryDtRequired = row.Field<bool>("ExpiryDtRequired"),
                                    ChkLooseSelling = row.Field<bool>("ChkLooseSelling"),
                                    chkQualityCtrl = row.Field<bool>("chkQualityCtrl"),
                                    IsNonChargable = row.Field<bool>("IsNonChargable"),
                                    ConsigneeFlag = row.Field<bool>("ConsigneeFlag"),
                                    HighValueFlag = row.Field<bool>("HighValueFlag"),
                                    ScheduledItem = row.Field<bool>("ScheduledItem"),
                                    IsNoBarcode = row.Field<bool>("IsNoBarcode"),
                                    Deactive = row.Field<bool>("Deactive"),
                                    VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                    UnIndentItem = row.Field<bool>("UnIndentItem")
                                }).ToList();
                }
            }
            return items;
        }

        public int CreateItemSupplierMapping(ItemSupplierMappingEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierId", entity.SupplierId, DbType.Int32));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertItemMaster, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }
        public bool CheckDuplicateupdate(string code, int ID)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Item", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public IEnumerable<ItemMasterEntities> GetNonVendorItems()
        {
            List<ItemMasterEntities> item = new List<ItemMasterEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetNonVendorItems, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                VatOn = row.Field<string>("VatOn"),
                                SalesTax = row.Field<double?>("SalesTax"),
                                SalesPrice = row.Field<double?>("SalesPrice"),
                                StandardRate = row.Field<double?>("StandardRate"),
                                SealingRate = row.Field<double?>("SealingRate"),
                                OwnStock = row.Field<bool>("OwnStock"),
                                NoMrp = row.Field<bool>("NoMrp"),
                                AutoConsumed = row.Field<bool>("AutoConsumed"),
                                Asset = row.Field<bool>("Asset"),
                                OPBalance = row.Field<double?>("OPBalance"),
                                MaxLevel = row.Field<double?>("MaxLevel"),
                                MinLevel = row.Field<double?>("MinLevel"),
                                BrandId = row.Field<int?>("BrandId"),
                                Notes = row.Field<string>("Notes"),
                                IsLocalPurchaseItem = row.Field<bool>("IsLocalPurchaseItem"),
                                IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                SeasonDate = row.Field<DateTime?>("SeasonDate"),
                                strSeasonDate = row.Field<DateTime?>("SeasonDate").DateTimeFormat1(),
                                SalesTaxID = row.Field<int?>("SalesTaxID"),
                                OrderUnitID = row.Field<int?>("OrderUnitID"),
                                ReOrderLevel = row.Field<double?>("ReOrderLevel"),
                                CurrentQty = row.Field<double?>("CurrentQty"),
                                UnitID = row.Field<int?>("UnitID"),
                                BranchID = row.Field<int?>("BranchID"),
                                StockTypeID = row.Field<int?>("StockTypeID"),
                                BatchRequired = row.Field<bool>("BatchRequired"),
                                ExpiryDtRequired = row.Field<bool>("ExpiryDtRequired"),
                                PurchaseFrequency = row.Field<string>("PurchaseFrequency"),
                                ItemChangeID = row.Field<int?>("ItemChangeID"),
                                Taxid = row.Field<int?>("Taxid"),
                                ChkLooseSelling = row.Field<bool>("ChkLooseSelling"),
                                chkQualityCtrl = row.Field<bool>("chkQualityCtrl"),
                                IsNonChargable = row.Field<bool>("IsNonChargable"),
                                MarkupPercentage = row.Field<int?>("MarkupPercentage"),
                                ConsigneeFlag = row.Field<bool>("ConsigneeFlag"),
                                ConsigneeId = row.Field<int?>("ConsigneeId"),
                                HighValueFlag = row.Field<bool>("HighValueFlag"),
                                ScheduledItem = row.Field<bool>("ScheduledItem"),
                                IsNoBarcode = row.Field<bool>("IsNoBarcode"),
                                ScheduledIVItemId = row.Field<int?>("ScheduledIVItemId"),
                                TaxOnPurchase = row.Field<decimal?>("TaxOnPurchase"),
                                TaxOnSale = row.Field<decimal?>("TaxOnSale"),
                                PurchaseRate = row.Field<decimal?>("PurchaseRate"),
                                MRP = row.Field<double?>("MRP"),
                                Deactive = row.Field<bool>("Deactive"),
                                itemtypename = row.Field<string>("itemtypename"),
                                UnitName = row.Field<string>("UnitName"),
                                PackSize = row.Field<string>("PackSize"),
                                VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                UnIndentItem = row.Field<bool>("UnIndentItem")
                            }).ToList();
                if (item == null || item.Count == 0)
                    item.Add(new ItemMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = "",
                        DescriptiveName = "",
                        ItemTypeID = 0,
                        PackSizeID = 0,
                        Deactive = false,
                        OwnStock = false,
                        NoMrp = false,
                        AutoConsumed = false,
                        Asset = false,
                        IsLocalPurchaseItem = false,
                        IsRoutineItem = false,
                        BatchRequired = false,
                        ExpiryDtRequired = false,
                        ChkLooseSelling = false,
                        chkQualityCtrl = false,
                        IsNonChargable = false,
                        ConsigneeFlag = false,
                        HighValueFlag = false,
                        ScheduledItem = false,
                        IsNoBarcode = false,
                    });
            }
            return item;
        }

        public IEnumerable<ItemMasterEntities> GetNonIndentItems(int StoreId)
        {
            List<ItemMasterEntities> item = new List<ItemMasterEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", StoreId, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetNonIndentItems,param, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                OwnStock = row.Field<bool>("OwnStock"),
                                NoMrp = row.Field<bool>("NoMrp"),
                                AutoConsumed = row.Field<bool>("AutoConsumed"),
                                Asset = row.Field<bool>("Asset"),
                                IsLocalPurchaseItem = row.Field<bool>("IsLocalPurchaseItem"),
                                IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                SeasonDate = row.Field<DateTime?>("SeasonDate"),
                                strSeasonDate = row.Field<DateTime?>("SeasonDate").DateTimeFormat1(),
                                SalesTaxID = row.Field<int?>("SalesTaxID"),
                                OrderUnitID = row.Field<int?>("OrderUnitID"),
                                CurrentQty = row.Field<double?>("Qty"),
                                UnitID = row.Field<int?>("UnitID"),
                                BranchID = row.Field<int?>("BranchID"),
                                StockTypeID = row.Field<int?>("StockTypeID"),
                                BatchRequired = row.Field<bool>("BatchRequired"),
                                ExpiryDtRequired = row.Field<bool>("ExpiryDtRequired"),
                                ChkLooseSelling = row.Field<bool>("ChkLooseSelling"),
                                chkQualityCtrl = row.Field<bool>("chkQualityCtrl"),
                                IsNonChargable = row.Field<bool>("IsNonChargable"),
                                ConsigneeFlag = row.Field<bool>("ConsigneeFlag"),
                                HighValueFlag = row.Field<bool>("HighValueFlag"),
                                ScheduledItem = row.Field<bool>("ScheduledItem"),
                                IsNoBarcode = row.Field<bool>("IsNoBarcode"),
                                Deactive = row.Field<bool>("Deactive"),
                                itemtypename = row.Field<string>("itemtypename"),
                                UnitName = row.Field<string>("UnitName"),
                                VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                UnIndentItem = row.Field<bool>("UnIndentItem"),
                                BatchName = row.Field<string>("BatchName"),
                                BatchId = row.Field<int>("BatchID"),
                                ExpiryDate = row.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                            }).ToList();
                if (item == null || item.Count == 0)
                    item.Add(new ItemMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = "",
                        DescriptiveName = "",
                        ItemTypeID = 0,
                        PackSizeID = 0,
                        Deactive = false,
                        OwnStock = false,
                        NoMrp = false,
                        AutoConsumed = false,
                        Asset = false,
                        IsLocalPurchaseItem = false,
                        IsRoutineItem = false,
                        BatchRequired = false,
                        ExpiryDtRequired = false,
                        ChkLooseSelling = false,
                        chkQualityCtrl = false,
                        IsNonChargable = false,
                        ConsigneeFlag = false,
                        HighValueFlag = false,
                        ScheduledItem = false,
                        IsNoBarcode = false,
                    });
            }
            return item;
        }
        public IEnumerable<ItemMasterEntities> GetAllstorestockItems(int StoreID)
        {
            List<ItemMasterEntities> items = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("pStoreId", StoreID, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetAllstorestockItems, param, CommandType.StoredProcedure);
                items = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ItemID"),
                                BatchId = row.Field<int>("BatchId"),
                                UnitName = row.Field<string>("Unit"),
                                ExpiryDate = row.Field<DateTime?>("ExpiryDate").DateTimeFormat1(),
                                Name = row.Field<string>("ItemName"),
                                BatchName = row.Field<string>("Batch"),
                                CurrentQty = row.Field<double>("StockQty"),
                                StoreName = row.Field<string>("StoreName"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                UnitID = row.Field<int>("UnitID"),
                                Deactive = false,
                                OwnStock = false,
                                NoMrp = false,
                                AutoConsumed = false,
                                Asset = false,
                                IsLocalPurchaseItem = false,
                                IsRoutineItem = false,
                                BatchRequired = false,
                                ExpiryDtRequired = false,
                                ChkLooseSelling = false,
                                chkQualityCtrl = false,
                                IsNonChargable = false,
                                ConsigneeFlag = false,
                                HighValueFlag = false,
                                ScheduledItem = false,
                                IsNoBarcode = false,
                            }).ToList();
                
            }
            return items;
        }
        public IEnumerable<GetItemModal> GetItemCode(int itemtypeid)
        {
            List<GetItemModal> items = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("itemtypeid", itemtypeid, DbType.Int32);
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetItemCode, param, CommandType.StoredProcedure);
                items = dtManufacturer.AsEnumerable()
                            .Select(row => new GetItemModal
                            {
                                Code = row.Field<string>("Code"),

                            }).ToList();

            }
            return items;
        }

        public IEnumerable<ItemMasterEntities> GetItemsforclientbilling(int storeId)
        {
            List<ItemMasterEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", storeId, DbType.Int32));
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                //DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetAllStoreItemMaster, param, CommandType.StoredProcedure);// Changed on 06.10.2016 by Vikram
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetItemsforclientbilling, paramCollection, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                StandardRate = row.Field<double?>("StandardRate"),
                                CurrentQty = row.Field<double?>("CurrentQty"),
                                BatchRequired = row.Field<bool>("BatchRequired"),
                                ExpiryDtRequired = row.Field<bool>("ExpiryDtRequired"),
                                chkQualityCtrl = row.Field<bool>("ChkQualityCtrl"),
                                MRP = row.Field<double?>("MRP"),
                                OrderingUnit = row.Field<string>("UnitName"),
                                UnitID = row.Field<int?>("UnitID"),
                                UnitName = row.Field<string>("UnitName"),
                                PackSize = row.Field<string>("PackSize"),
                                NoMrp = row.Field<bool>("NoMrp"),
                                Asset = row.Field<bool>("Asset"),
                                Deactive = row.Field<bool>("Deactive"),
                                AutoConsumed = row.Field<bool>("AutoConsumed"),
                                IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                UnIndentItem = row.Field<bool>("UnIndentItem"),
                                itemtypename = row.Field<string>("itemtypename"),
                                PurchaseRate = row.Field<decimal?>("PurchaseRate"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                HSNCode = row.Field<string>("HSNCode"),
                            }).ToList();
            }
            return item;
        }

        public IEnumerable<ItemMasterEntities> GetItemDetails(string ItemIdList ,int StoreID)
        {
            List<ItemMasterEntities> item = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ItemIdList", ItemIdList, DbType.String));
                paramCollection.Add(new DBParameter("StoreID", StoreID, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(MasterQueries.GetItemDetails, paramCollection, CommandType.StoredProcedure);
                item = dtManufacturer.AsEnumerable()
                            .Select(row => new ItemMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                DescriptiveName = row.Field<string>("DescriptiveName"),
                                ItemTypeID = row.Field<int>("ItemTypeID"),
                                PackSizeID = row.Field<int>("PackSizeID"),
                                ItemGroupID = row.Field<int?>("ItemGroupID"),
                                VatOn = row.Field<string>("VatOn"),
                                SalesTax = row.Field<double?>("SalesTax"),
                                SalesPrice = row.Field<double?>("SalesPrice"),
                                StandardRate = row.Field<double?>("StandardRate"),
                                SealingRate = row.Field<double?>("SealingRate"),
                                OwnStock = row.Field<bool>("OwnStock"),
                                NoMrp = row.Field<bool>("NoMrp"),
                                AutoConsumed = row.Field<bool>("AutoConsumed"),
                                Asset = row.Field<bool>("Asset"),
                                OPBalance = row.Field<double?>("OPBalance"),
                                MaxLevel = row.Field<double?>("MaxLevel"),
                                MinLevel = row.Field<double?>("MinLevel"),
                                BrandId = row.Field<int?>("BrandId"),
                                Notes = row.Field<string>("Notes"),
                                IsLocalPurchaseItem = row.Field<bool>("IsLocalPurchaseItem"),
                                IsRoutineItem = row.Field<bool>("IsRoutineItem"),
                                SeasonDate = row.Field<DateTime?>("SeasonDate"),
                                strSeasonDate = row.Field<DateTime?>("SeasonDate").DateTimeFormat1(),
                                SalesTaxID = row.Field<int?>("SalesTaxID"),
                                OrderUnitID = row.Field<int?>("OrderUnitID"),
                                ReOrderLevel = row.Field<double?>("ReOrderLevel"),
                                CurrentQty = row.Field<double?>("CurrentQty"),
                                UnitID = row.Field<int?>("UnitID"),
                                BranchID = row.Field<int?>("BranchID"),
                                StockTypeID = row.Field<int?>("StockTypeID"),
                                BatchRequired = row.Field<bool>("BatchRequired"),
                                ExpiryDtRequired = row.Field<bool>("ExpiryDtRequired"),
                                PurchaseFrequency = row.Field<string>("PurchaseFrequency"),
                                ItemChangeID = row.Field<int?>("ItemChangeID"),
                                Taxid = row.Field<int?>("Taxid"),
                                ChkLooseSelling = row.Field<bool>("ChkLooseSelling"),
                                chkQualityCtrl = row.Field<bool>("chkQualityCtrl"),
                                IsNonChargable = row.Field<bool>("IsNonChargable"),
                                MarkupPercentage = row.Field<int?>("MarkupPercentage"),
                                ConsigneeFlag = row.Field<bool>("ConsigneeFlag"),
                                ConsigneeId = row.Field<int?>("ConsigneeId"),
                                HighValueFlag = row.Field<bool>("HighValueFlag"),
                                ScheduledItem = row.Field<bool>("ScheduledItem"),
                                IsNoBarcode = row.Field<bool>("IsNoBarcode"),
                                ScheduledIVItemId = row.Field<int?>("ScheduledIVItemId"),
                                TaxOnPurchase = row.Field<decimal?>("TaxOnPurchase"),
                                TaxOnSale = row.Field<decimal?>("TaxOnSale"),
                                PurchaseRate = row.Field<decimal?>("PurchaseRate"),
                                MRP = row.Field<double?>("MRP"),
                                Deactive = row.Field<bool>("Deactive"),
                                itemtypename = row.Field<string>("itemtypename"),
                                UnitName = row.Field<string>("UnitName"),
                                PackSize = row.Field<string>("PackSize"),
                                VENDORFLAG = row.Field<bool>("VENDORFLAG"),
                                HSNCode = row.Field<string>("HSNCode"),
                                UnIndentItem = row.Field<bool>("UnIndentItem"),
                                ItemNumber = row.Field<int?>("ItemNumber"),
                                ItemCategory = row.Field<string>("ItemCategory"),
                                TaxRate = row.Field<double?>("TaxRate"),
                                ScrapItem = row.Field<bool>("ScrapItem"),
                                SaleItem = row.Field<bool>("SaleItem"),
                                Service = row.Field<bool>("IsService"),
                                PackingList = row.Field<bool>("IsPackingList"),
                                InventoryItem = row.Field<bool>("InventoryItem"),
                                PurchaseItem = row.Field<bool>("PurchaseItem"),
                                LastPORate = row.Field<double?>("LastPORate"),
                                Make = row.Field<string>("Make"),
                                MaterialOfConstruct = row.Field<string>("MaterialOfConstruct"),
                            }).ToList();
            }
            return item;
        }

    }
}
