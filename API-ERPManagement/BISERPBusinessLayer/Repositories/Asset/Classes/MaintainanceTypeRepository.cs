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
    public class MaintainanceTypeRepository : IMaintainanceTypeRepository
    {
        public IEnumerable<MaintainanceTypeEntity> GetAllMaintainanceType()
        {
            List<MaintainanceTypeEntity> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(AssetQueries.GetAllMaintainanceType, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new MaintainanceTypeEntity
                            {
                                MaintainanceTypeId = row.Field<int>("MaintainanceTypeId"),
                                MaintainanceTypeCode = row.Field<string>("MaintainanceTypeCode"),
                                MaintainanceType = row.Field<string>("MaintainanceType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return units;
        }

        public int CreateMaintainanceType(MaintainanceTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("MaintainanceTypeId", Entity.MaintainanceTypeId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("MaintainanceTypeCode", Entity.MaintainanceTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("MaintainanceType", Entity.MaintainanceType, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdAstMstMaintainanceType, paramCollection, CommandType.StoredProcedure, "MaintainanceTypeId");
            }
            return iResult;
        }

        public bool UpdateMaintainanceType(MaintainanceTypeEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("MaintainanceTypeId", Entity.MaintainanceTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("MaintainanceTypeCode", Entity.MaintainanceTypeCode, DbType.String));
                paramCollection.Add(new DBParameter("MaintainanceType", Entity.MaintainanceType, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdAstMstMaintainanceType, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<MaintainanceTypeEntity> GetActiveMaintainanceType()
        {
             List<MaintainanceTypeEntity> division = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.GetAllMaintainanceType, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new MaintainanceTypeEntity
                            {
                                MaintainanceTypeId = row.Field<int>("MaintainanceTypeId"),
                                MaintainanceTypeCode = row.Field<string>("MaintainanceTypeCode"),
                                MaintainanceType = row.Field<string>("MaintainanceType"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return division;
        }

        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "MaintainanceType", DbType.String));
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
                paramCollection.Add(new DBParameter("Type", "MaintainanceType", DbType.String));
                paramCollection.Add(new DBParameter("ID", Id, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}
