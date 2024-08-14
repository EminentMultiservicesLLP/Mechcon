using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPBusinessLayer.QueryCollection.Masters;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class CityMasterRepository : ICityMasterRepository
    {
        public CityMasterEntities GetCityById(int Id)
        {
            CityMasterEntities city = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", Id, DbType.Int32);
                DataTable dtCity = dbHelper.ExecuteDataTable(MasterQueries.GetStoreMasterById, param, CommandType.StoredProcedure);

                city = dtCity.AsEnumerable()
                            .Select(row => new CityMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                CityName = row.Field<string>("CityName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).FirstOrDefault();
            }
            return city;
        }

        public IEnumerable<CityMasterEntities> GetAllCities()
        {
            List<CityMasterEntities> cities = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetAllCityMaster, CommandType.StoredProcedure);
                cities = dtStores.AsEnumerable()
                            .Select(row => new CityMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                StateId = row.Field<int>("StateId"),
                                CityName = row.Field<string>("CityName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return cities;
        }

        public int CreateCity(CityMasterEntities Cityentity)
        {
            int iResult = 0;
            using(DBHelper dbHelper = new DBHelper())
            { 
               DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("CityName",Cityentity.CityName,DbType.String));
                paramCollection.Add(new DBParameter("CreateBy", Cityentity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("CreateOn", Cityentity.InsertedON, DbType.Int32));
                paramCollection.Add(new DBParameter("CreatedMacName", Cityentity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("CreatedMacID", Cityentity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("CreatedIPAddress", Cityentity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Cityentity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.InsertSupplier, paramCollection, CommandType.StoredProcedure);
                iResult = (int)paramCollection.Get("CityId").Value;
            }
            return iResult;
            
        }

        public bool UpdateCity(CityMasterEntities Cityentity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("CityID", Cityentity.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("CityName", Cityentity.CityName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", Cityentity.UpdatedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedIP", Cityentity.UpdatedIPAddress, DbType.String));


                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateUnitMasterById, paramCollection, CommandType.StoredProcedure);
            }
           
           if(iResult>0)
           {
               return true;
           }
           else
           {
               return false;
           }
        }

        public bool DeleteCity(CityMasterEntities entity)
        {
            throw new NotImplementedException();
        }
    }
}
