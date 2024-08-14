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

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class AssetRepository : IAssetRepository
    {

        public IEnumerable<AssetEntity> GetAllAssets()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AssetEntity> GetBranchAssets(int BranchId)
        {
            List<AssetEntity> room = new List<AssetEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                DataTable dtRoom = dbHelper.ExecuteDataTable(AssetQueries.GetBranchAsset, paramCollection, CommandType.StoredProcedure);
                room = dtRoom.AsEnumerable()
                            .Select(row => new AssetEntity
                            {
                                AssetId = row.Field<int>("AssetId"),
                                BranchId = row.Field<int>("BranchId"),
                                ItemName = row.Field<string>("ItemName"),
                                AssetCode = row.Field<string>("AssetCode"),
                                SerialNo = row.Field<string>("SerialNo"),
                                ModelNo = row.Field<string>("ModelNo"),
                                ItemId = row.Field<int>("ItemId"),
                                SupplierId = row.Field<int>("SupplierId"),
                                SupplierName = row.Field<string>("SupplierName"),
                            }).ToList();
            }
            return room;
        }

        public bool CreateAsset(List<AssetEntity> entityasset, DBHelper dbHelper)
        {
            int falseCtr = 0;
            foreach (var entity in entityasset)
            {
                string AcquiredDate = "";
                string[] arrAcquiredDate;
                if (entity.strAcquiredDate == null)
                {
                    AcquiredDate = "01-01-2000";
                }
                else
                {
                    arrAcquiredDate = entity.strAcquiredDate.Split('/');
                    AcquiredDate = arrAcquiredDate[1].ToString() + "-" + arrAcquiredDate[0].ToString() + "-" + arrAcquiredDate[2].ToString();
                }

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("AssetId", entity.AssetId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemCode", entity.ItemCode, DbType.String));
                paramCollection.Add(new DBParameter("ItemName", entity.ItemName, DbType.String));
                paramCollection.Add(new DBParameter("AssetCode", entity.AssetCode, DbType.String));
                paramCollection.Add(new DBParameter("SerialNo", entity.SerialNo, DbType.String));
                paramCollection.Add(new DBParameter("AcquiredDate", AcquiredDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("GRNId", entity.GRNId, DbType.Int32));
                paramCollection.Add(new DBParameter("GRNDtlId", entity.GRNDtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierId", entity.SupplierId, DbType.Int32));
                paramCollection.Add(new DBParameter("ModelNo", entity.ModelNo, DbType.String));
                paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Int32));
                entity.AssetId = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstAsset, paramCollection, CommandType.StoredProcedure, "AssetId");
                if (entity.AssetId <= 0)
                    falseCtr++;
            }
            if (falseCtr > 0)
                return false;
            else
                return true;
        }

        public bool UpdateAsset(List<AssetEntity> entityasset, DBHelper dbHelper)
        {
            int falseCtr = 0;
            int Result = 0;
            foreach (var entity in entityasset)
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("AssetId", entity.AssetId, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.String));
                paramCollection.Add(new DBParameter("ItemCode", entity.ItemCode, DbType.String));
                paramCollection.Add(new DBParameter("ItemName", entity.ItemName, DbType.String));
                paramCollection.Add(new DBParameter("AssetCode", entity.AssetCode, DbType.Int32));
                paramCollection.Add(new DBParameter("SerialNo", entity.SerialNo, DbType.Int32));
                paramCollection.Add(new DBParameter("AcquiredDate", entity.AcquiredDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("GRNId", entity.GRNId, DbType.String));
                paramCollection.Add(new DBParameter("GRNDtlId", entity.GRNDtlId, DbType.String));
                paramCollection.Add(new DBParameter("SupplierId", entity.SupplierId, DbType.String));
                paramCollection.Add(new DBParameter("ModelNo", entity.ModelNo, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Int32));
                Result = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdAstAsset, paramCollection, CommandType.StoredProcedure);
                if (Result <= 0)
                    falseCtr++;
            }
            if (falseCtr > 0)
                return false;
            else
                return true;
        }
    }
}
