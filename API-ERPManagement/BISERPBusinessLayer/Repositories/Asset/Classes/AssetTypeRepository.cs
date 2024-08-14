using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.QueryCollection.Asset;
using BISERPBusinessLayer.QueryCollection.Masters;
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
    public class AssetTypeRepository : IAssetTypeRepository
    {
        public IEnumerable<AssetTypeEntity> GetAllAssetType()
        {
            List<AssetTypeEntity> assettype = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.GetAllAssetType, CommandType.StoredProcedure);
                assettype = dtUnits.AsEnumerable()
                            .Select(row => new AssetTypeEntity
                            {
                                AssetTypeId = row.Field<int>("AssetTypeId"),
                                AssetTypeCode = row.Field<string>("AssetTypeCode"),
                                AssetType = row.Field<string>("AssetType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return assettype;
        }

        public int CreateAssetType(AssetTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("AssetTypeId", Entity.AssetTypeId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("AssetTypeCode", Entity.AssetTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("AssetType", Entity.AssetType, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstMstAssetType, paramCollection, CommandType.StoredProcedure, "AssetTypeId");
            }
            return iResult;
        }

        public bool UpdateAssetType(AssetTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("AssetTypeId", Entity.AssetTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("AssetTypeCode", Entity.AssetTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("AssetType", Entity.AssetType, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdAstMstAssetType, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<AssetTypeEntity> GetActiveAssetType()
        {
            List<AssetTypeEntity> assettype = new List<AssetTypeEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.GetAllAssetType, paramCollection, CommandType.StoredProcedure);
                assettype = dtManufacturer.AsEnumerable()
                            .Select(row => new AssetTypeEntity
                            {
                                AssetTypeId = row.Field<int>("AssetTypeId"),
                                AssetTypeCode = row.Field<string>("AssetTypeCode"),
                                AssetType = row.Field<string>("AssetType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return assettype;
        }


        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "AssetType", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public bool CheckDuplicateupdate(string code, int Id)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "AssetType", DbType.String));
                paramCollection.Add(new DBParameter("ID", Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
