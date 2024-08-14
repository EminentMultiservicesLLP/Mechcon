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
    public class AssetSubTypeRepository : IAssetSubTypeRepository
    {
        public IEnumerable<AssetSubTypeEntity> GetAllAssetSubType()
        {
            List<AssetSubTypeEntity> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.GetAllAssetSubType, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new AssetSubTypeEntity
                            {
                                SubTypeId = row.Field<int>("SubTypeId"),
                                SubTypeCode = row.Field<string>("SubTypeCode"),
                                SubType = row.Field<string>("SubType"),
                                AssetTypeId = row.Field<int>("AssetTypeId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return units;
        }

        public int CreateAssetSubType(AssetSubTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("SubTypeId", Entity.SubTypeId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("SubTypeCode", Entity.SubTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("SubType", Entity.SubType, DbType.String));
                paramCollection.Add(new DBParameter("AssetTypeId", Entity.AssetTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstMstAssetSubType, paramCollection, CommandType.StoredProcedure, "SubTypeId");
            }
            return iResult;
        }

        public bool UpdateAssetSubType(AssetSubTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("SubTypeId", Entity.SubTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("SubTypeCode", Entity.SubTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("SubType", Entity.SubType, DbType.String));
                paramCollection.Add(new DBParameter("AssetTypeId", Entity.AssetTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdAstMstAssetSubType, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<AssetSubTypeEntity> GetActiveAssetSubType()
        {
            List<AssetSubTypeEntity> division = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.GetAllAssetSubType, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new AssetSubTypeEntity
                            {
                                SubTypeId = row.Field<int>("SubTypeId"),
                                SubTypeCode = row.Field<string>("SubTypeCode"),
                                SubType = row.Field<string>("SubType"),
                                AssetTypeId = row.Field<int>("AssetTypeId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return division;
        }

        public IEnumerable<AssetSubTypeEntity> GetActiveAssetSubType(int AssetTypeId)
        {
            List<AssetSubTypeEntity> subtype = new List<AssetSubTypeEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("AssetTypeId", AssetTypeId, DbType.Int32);
                DataTable dtSubtype = dbHelper.ExecuteDataTable(AssetQueries.GetAssetTypewiseSubType, param, CommandType.StoredProcedure);
                subtype = dtSubtype.AsEnumerable()
                            .Select(row => new AssetSubTypeEntity
                            {
                                SubTypeId = row.Field<int>("SubTypeId"),
                                SubTypeCode = row.Field<string>("SubTypeCode"),
                                SubType = row.Field<string>("SubType"),
                                AssetTypeId = row.Field<int>("AssetTypeId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return subtype;
        }

        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "AssetSubType", DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "AssetSubType", DbType.String));
                paramCollection.Add(new DBParameter("ID", Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
