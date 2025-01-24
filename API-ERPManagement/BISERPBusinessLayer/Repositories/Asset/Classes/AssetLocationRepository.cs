using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.QueryCollection.Asset;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.Entities.Purchase;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class AssetLocationRepository : IAssetLocationRepository
    {
        public int CreateAssetLocation(AssetLocationEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("AssetLocationId", Entity.AssetLocationId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("AssetId", Entity.AssetId, DbType.Int32));
                paramCollection.Add(new DBParameter("AssetTypeId", Entity.AssetTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("AssetSubTypeId", Entity.AssetSubTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("Description", Entity.Description, DbType.String));
                paramCollection.Add(new DBParameter("LocationId", Entity.LocationId, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchId", Entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("FloorId", Entity.FloorId, DbType.Int32));
                paramCollection.Add(new DBParameter("RoomId", Entity.RoomId, DbType.Int32));
                paramCollection.Add(new DBParameter("LifeTime", Entity.LifeTime, DbType.String));
                paramCollection.Add(new DBParameter("IsWarranty", Entity.IsWarranty, DbType.Boolean));
                paramCollection.Add(new DBParameter("WarrantyStart", Entity.WarrantyStart, DbType.DateTime));
                paramCollection.Add(new DBParameter("WarrantyExpire", Entity.WarrantyExpire, DbType.DateTime));
                paramCollection.Add(new DBParameter("IsFreeService", Entity.IsFreeService, DbType.Boolean));
                paramCollection.Add(new DBParameter("FreeService", Entity.FreeService, DbType.String));
                paramCollection.Add(new DBParameter("FreeServiceStartDate", Entity.FreeServiceStartDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("FreeServiceEndDate", Entity.FreeServiceEndDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("LegacyPONo", Entity.LegacyPONo, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                paramCollection.Add(new DBParameter("PermanentDeactive", Entity.PermanentDeactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstAssetLocation, paramCollection, CommandType.StoredProcedure, "AssetLocationId");
            }
            return iResult;
        }

        public bool UpdateAssetLocation(AssetLocationEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("AssetLocationId", Entity.AssetLocationId, DbType.Int32));
                paramCollection.Add(new DBParameter("AssetId", Entity.AssetId, DbType.Int32));
                paramCollection.Add(new DBParameter("AssetTypeId", Entity.AssetTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("AssetSubTypeId", Entity.AssetSubTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("Description", Entity.Description, DbType.String));
                paramCollection.Add(new DBParameter("LocationId", Entity.LocationId, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchId", Entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("FloorId", Entity.FloorId, DbType.Int32));
                paramCollection.Add(new DBParameter("RoomId", Entity.RoomId, DbType.Int32));
                paramCollection.Add(new DBParameter("LifeTime", Entity.LifeTime, DbType.String));
                paramCollection.Add(new DBParameter("IsWarranty", Entity.IsWarranty, DbType.Boolean));
                paramCollection.Add(new DBParameter("WarrantyStart", Entity.WarrantyStart, DbType.DateTime));
                paramCollection.Add(new DBParameter("WarrantyExpire", Entity.WarrantyExpire, DbType.DateTime));
                paramCollection.Add(new DBParameter("IsFreeService", Entity.IsFreeService, DbType.Boolean));
                paramCollection.Add(new DBParameter("FreeService", Entity.FreeService, DbType.String));
                paramCollection.Add(new DBParameter("FreeServiceStartDate", Entity.FreeServiceStartDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("FreeServiceEndDate", Entity.FreeServiceEndDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("LegacyPONo", Entity.LegacyPONo, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                paramCollection.Add(new DBParameter("PermanentDeactive", Entity.PermanentDeactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdAstAssetLocation, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }


        public AssetLocationEntity GetAssetLocation(int AssetId)
        {
            AssetLocationEntity assetlocation = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("AssetId", AssetId, DbType.Int32));
                DataTable dtLocation = dbHelper.ExecuteDataTable(AssetQueries.GetAssetLocation, paramCollection, CommandType.StoredProcedure);
                assetlocation = dtLocation.AsEnumerable()
                            .Select(row => new AssetLocationEntity
                            {
                                AssetLocationId = row.Field<int>("AssetLocationId"),
                                AssetId = row.Field<int>("AssetId"),
                                AssetTypeId = row.Field<int>("AssetTypeId"),
                                AssetSubTypeId = row.Field<int>("AssetSubTypeId"),
                                Description = row.Field<string>("Description"),
                                BranchId = row.Field<int>("BranchId"),
                                FloorId = row.Field<int>("FloorId"),
                                RoomId = row.Field<int>("RoomId"),
                                Branch = row.Field<string>("BranchName"),
                                Floor = row.Field<string>("FloorName"),
                                Room = row.Field<string>("RoomName"),
                                FreeService = row.Field<string>("FreeService"),
                                WarrantyStart = row.Field<DateTime?>("WarrantyStart"),
                                WarrantyExpire = row.Field<DateTime?>("WarrantyExpire"),
                                FreeServiceStartDate = row.Field<DateTime?>("FreeServiceStartDate"),
                                FreeServiceEndDate = row.Field<DateTime?>("FreeServiceEndDate"),
                                strWarrantyStart = row.Field<DateTime?>("WarrantyStart").DateTimeFormat1(),
                                strWarrantyExpire = row.Field<DateTime?>("WarrantyExpire").DateTimeFormat1(),
                                strFreeServiceStartDate = row.Field<DateTime?>("FreeServiceStartDate").DateTimeFormat1(),
                                strFreeServiceEndDate = row.Field<DateTime?>("FreeServiceEndDate").DateTimeFormat1(),
                                Deactive = row.Field<Boolean>("Deactive"),
                                PermanentDeactive = row.Field<Boolean>("PermanentDeactive")
                            }).FirstOrDefault();
            }
            return assetlocation;
        }
        public IEnumerable<PurchaseOrderEntities> GetPoNoAssetLo(int ItemId)
        {
            List<PurchaseOrderEntities> type = new List<PurchaseOrderEntities>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ItemId", ItemId, DbType.Int32));
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetPoNoAssetLo, paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new PurchaseOrderEntities
                            {
                                ID = row.Field<int>("ID"),
                                PONo = row.Field<string>("PONo"),
                                PODate = row.Field<DateTime>("PODate"),
                                strPODate = Convert.ToDateTime(row.Field<DateTime>("PODate")).ToString("dd-MMM-yyyy"),
                            }).ToList();
           
            }
            return type;
        }
        public IEnumerable<AssetLocationEntity> AssetDetailReport(int BranchId, int ItemId, int FloorId)
        {
            List<AssetLocationEntity> assetlocation = new List<AssetLocationEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", ItemId, DbType.Int32));
                paramCollection.Add(new DBParameter("FloorId", FloorId, DbType.Int32));
                DataTable dtLocation = dbHelper.ExecuteDataTable(AssetQueries.AssetDetailsItemWise, paramCollection, CommandType.StoredProcedure);
                assetlocation = dtLocation.AsEnumerable()
                            .Select(row => new AssetLocationEntity
                            {
                                AssetLocationId = row.Field<int>("AssetLocationId"),
                                AssetId = row.Field<int>("AssetId"),
                                AssetTypeId = row.Field<int>("AssetTypeId"),
                                AssetSubTypeId = row.Field<int>("AssetSubTypeId"),
                                Description = row.Field<string>("Description"),
                                BranchId = row.Field<int>("BranchId"),
                                FloorId = row.Field<int>("FloorId"),
                                RoomId = row.Field<int>("RoomId"),
                                Floor = row.Field<string>("FloorName"),
                                Room = row.Field<string>("RoomName"),
                                WarrantyStart = row.Field<DateTime?>("WarrantyStart"),
                                WarrantyExpire = row.Field<DateTime?>("WarrantyExpire"),
                                FreeServiceStartDate = row.Field<DateTime?>("FreeServiceStartDate"),
                                FreeServiceEndDate = row.Field<DateTime?>("FreeServiceEndDate"),
                                Deactive = row.Field<Boolean>("Deactive"),
                                PermanentDeactive = row.Field<Boolean>("PermanentDeactive"),
                                Branch = row.Field<string>("BranchName"),
                                AssetCode = row.Field<string>("AssetCode"),
                                ItemName = row.Field<string>("ItemName"),
                                SerialNo = row.Field<string>("SerialNo"),
                                ModelNo = row.Field<string>("ModelNo"),
                                AssetType = row.Field<string>("AssetType"),
                                AssetSubType = row.Field<string>("SubType"),
                                AcquiredDate = row.Field<string>("AcquiredDate"),
                                ItemCode = row.Field<string>("ItemCode"),
                                Remarks = row.Field<string>("Remarks"),
                                ItemId = row.Field<int>("ItemId"),
                            }).ToList();
            }
            return assetlocation;
        }
        public IEnumerable<AssetLocationEntity> AssetDetailReport(int BranchId, int AssetId)
        {
            List<AssetLocationEntity> assetlocation = new List<AssetLocationEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("AssetId", AssetId, DbType.Int32));
                DataTable dtLocation = dbHelper.ExecuteDataTable(AssetQueries.AssetDetailReport, paramCollection, CommandType.StoredProcedure);
                assetlocation = dtLocation.AsEnumerable()
                            .Select(row => new AssetLocationEntity
                            {
                                AssetLocationId = row.Field<int>("AssetLocationId"),
                                AssetId = row.Field<int>("AssetId"),
                                AssetTypeId = row.Field<int>("AssetTypeId"),
                                AssetSubTypeId = row.Field<int>("AssetSubTypeId"),
                                Description = row.Field<string>("Description"),
                                BranchId = row.Field<int>("BranchId"),
                                FloorId = row.Field<int>("FloorId"),
                                RoomId = row.Field<int>("RoomId"),
                                Floor = row.Field<string>("FloorName"),
                                Room = row.Field<string>("RoomName"),
                                WarrantyStart = row.Field<DateTime?>("WarrantyStart"),
                                WarrantyExpire = row.Field<DateTime?>("WarrantyExpire"),
                                FreeServiceStartDate = row.Field<DateTime?>("FreeServiceStartDate"),
                                FreeServiceEndDate = row.Field<DateTime?>("FreeServiceEndDate"),
                                Deactive = row.Field<Boolean>("Deactive"),
                                PermanentDeactive = row.Field<Boolean>("PermanentDeactive"),
                                Branch = row.Field<string>("BranchName"),
                                AssetCode = row.Field<string>("AssetCode"),
                                ItemName = row.Field<string>("ItemName"),
                                SerialNo = row.Field<string>("SerialNo"),
                                ModelNo = row.Field<string>("ModelNo"),
                                AssetType = row.Field<string>("AssetType"),
                                AssetSubType = row.Field<string>("SubType"),
                                AcquiredDate = row.Field<string>("AcquiredDate"),
                                ItemCode = row.Field<string>("ItemCode"),
                                Remarks = row.Field<string>("Remarks"),
                            }).ToList();
            }
            return assetlocation;
        }

        public IEnumerable<AssetLocationEntity> AssetLocationReport(int locationId)
        {
            List<AssetLocationEntity> assetlocation = new List<AssetLocationEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("locationId", locationId, DbType.Int32));
                DataTable dtLocation = dbHelper.ExecuteDataTable(AssetQueries.rptAssetLocation, paramCollection, CommandType.StoredProcedure);
                assetlocation = dtLocation.AsEnumerable()
                            .Select(row => new AssetLocationEntity
                            {
                                AssetLocationId = row.Field<int>("AssetLocationId"),
                                AssetId = row.Field<int>("AssetId"),
                                AssetTypeId = row.Field<int>("AssetTypeId"),
                                AssetSubTypeId = row.Field<int>("AssetSubTypeId"),
                                Description = row.Field<string>("Description"),
                                BranchId = row.Field<int>("BranchId"),
                                FloorId = row.Field<int>("FloorId"),
                                RoomId = row.Field<int>("RoomId"),
                                Floor = row.Field<string>("FloorName"),
                                Room = row.Field<string>("RoomName"),
                                WarrantyStart = row.Field<DateTime?>("WarrantyStart"),
                                WarrantyExpire = row.Field<DateTime?>("WarrantyExpire"),
                                FreeServiceStartDate = row.Field<DateTime?>("FreeServiceStartDate"),
                                FreeServiceEndDate = row.Field<DateTime?>("FreeServiceEndDate"),
                                Deactive = row.Field<Boolean>("Deactive"),
                                PermanentDeactive = row.Field<Boolean>("PermanentDeactive"),
                                Branch = row.Field<string>("BranchName"),
                                AssetCode = row.Field<string>("AssetCode"),
                                ItemName = row.Field<string>("ItemName"),
                                SerialNo = row.Field<string>("SerialNo"),
                                ModelNo = row.Field<string>("ModelNo"),
                                AssetType = row.Field<string>("AssetType"),
                                AssetSubType = row.Field<string>("SubType"),
                                LegacyPONo = row.Field<string>("LegacyPONo"),
                                LocationName = row.Field<string>("LocationName"),
                            }).ToList();
            }
            return assetlocation;
        }
    }
}
