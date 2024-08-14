using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class CityStateCountryMasterRepository : ICityStateCountryMasterRepository
    {
        private static readonly ILogger _loggger = Logger.Register(typeof(CityStateCountryMasterRepository));
        public int SaveCity(CityMasterEntities model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", model.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("CityName", model.CityName, DbType.String));
                paramCollection.Add(new DBParameter("StateId", model.StateId, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", model.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", model.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", model.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", model.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", model.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", false, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.SaveCity, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }
        public int SaveState(StateMasterEntities model)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ID", model.ID, DbType.Int32));
                paramCollection.Add(new DBParameter("StateName", model.StateName, DbType.String));
                paramCollection.Add(new DBParameter("CountryId", model.CountryID, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedBy", model.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", model.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", model.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", model.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", model.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", false, DbType.Boolean));

                iResult = dbHelper.ExecuteNonQuery(MasterQueries.SaveState, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }
    }
}
