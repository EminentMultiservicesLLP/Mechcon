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
    public class LocationRepository : ILocationRepository
    {
        public IEnumerable<LocationEntity> GetAllLocation()
        {
            List<LocationEntity> states = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStores = dbHelper.ExecuteDataTable(AssetQueries.GetAllLocation, CommandType.StoredProcedure);
                states = dtStores.AsEnumerable()
                            .Select(row => new LocationEntity
                            {
                                LocationId = row.Field<int>("LocationId"),
                                LocationCode = row.Field<string>("LocationCode"),
                                LocationName = row.Field<string>("LocationName"),
                                Address1 = row.Field<string>("Address1"),
                                Address2 = row.Field<string>("Address2"),
                                BranchId = row.Field<int>("BranchId"),
                                CityId = row.Field<int>("CityId"),
                                StateId = row.Field<int>("StateId"),
                                Pincode = row.Field<string>("Pincode"),
                                Telephone = row.Field<string>("Telephone"),
                                ConsumerNo = row.Field<string>("ConsumerNo"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return states;
        }

        public int CreateLocation(LocationEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("LocationId", entity.LocationId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("LocationCode", entity.LocationCode, DbType.String));
                paramCollection.Add(new DBParameter("LocationName", entity.LocationName, DbType.String));
                paramCollection.Add(new DBParameter("Address1", entity.Address1, DbType.String));
                paramCollection.Add(new DBParameter("Address2", entity.Address2, DbType.String));
                paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("CityId", entity.CityId, DbType.Int32));
                paramCollection.Add(new DBParameter("StateId", entity.StateId, DbType.Int32));
                paramCollection.Add(new DBParameter("Pincode", entity.Pincode, DbType.String));
                paramCollection.Add(new DBParameter("Telephone", entity.Telephone, DbType.String));
                paramCollection.Add(new DBParameter("ConsumerNo", entity.ConsumerNo, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsLocation, paramCollection, CommandType.StoredProcedure, "LocationId");
            }
            return iResult;
        }

        public bool UpdateLocation(LocationEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("LocationId", entity.LocationId, DbType.Int32));
                paramCollection.Add(new DBParameter("LocationCode", entity.LocationCode, DbType.String));
                paramCollection.Add(new DBParameter("LocationName", entity.LocationName, DbType.String));
                paramCollection.Add(new DBParameter("Address1", entity.Address1, DbType.String));
                paramCollection.Add(new DBParameter("Address2", entity.Address2, DbType.String));
                paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("CityId", entity.CityId, DbType.Int32));
                paramCollection.Add(new DBParameter("StateId", entity.StateId, DbType.Int32));
                paramCollection.Add(new DBParameter("Pincode", entity.Pincode, DbType.String));
                paramCollection.Add(new DBParameter("Telephone", entity.Telephone, DbType.String));
                paramCollection.Add(new DBParameter("ConsumerNo", entity.ConsumerNo, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.UpdLocation, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public bool CheckDuplicateItem(string code)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Location", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool CheckDuplicateupdate(string code, int LocationId)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "Location", DbType.String));
                paramCollection.Add(new DBParameter("ID", LocationId, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public IEnumerable<LocationEntity> GetActiveLocation()
        {
            List<LocationEntity> division = null;
            using (DBHelper dbHelper = new DBHelper())
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Deactive", 0, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.AstGetActiveLocation, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new LocationEntity
                            {
                                LocationId = row.Field<int>("LocationId"),
                                LocationCode = row.Field<string>("LocationCode"),
                                LocationName = row.Field<string>("LocationName"),
                                BranchId = row.Field<int>("BranchId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return division;
        }
        public IEnumerable<LocationEntity> GetBranchLocation(int BranchId)
        {
            List<LocationEntity> division = new List<LocationEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AssetQueries.GetBranchLocation, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new LocationEntity
                            {
                                LocationId = row.Field<int>("LocationId"),
                                LocationCode = row.Field<string>("LocationCode"),
                                LocationName = row.Field<string>("LocationName"),
                                BranchId = row.Field<int>("BranchId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
            }
            return division;
        }
    }
}
