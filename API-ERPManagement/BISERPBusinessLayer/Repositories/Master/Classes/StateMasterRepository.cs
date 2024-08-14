using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Masters;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class StateMasterRepository : IStateMasterRepository
    {

        public StateMasterEntities GetStateById(int Id)
        {
            StateMasterEntities state = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", Id, DbType.Int32);
                DataTable dtState = dbHelper.ExecuteDataTable(MasterQueries.GetStoreMasterById, param, CommandType.StoredProcedure);

                state = dtState.AsEnumerable()
                            .Select(row => new StateMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                StateName = row.Field<string>("StateName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).FirstOrDefault();
            }
            return state;
        }

        public IEnumerable<StateMasterEntities> GetAllStates()
        {
            List<StateMasterEntities> states = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetAllStateMaster, CommandType.StoredProcedure);
                states = dtStores.AsEnumerable()
                            .Select(row => new StateMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                StateName = row.Field<string>("StateName"),
                                CountryID = row.Field<int>("CountryId"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return states;
        }

        public bool CreateState(StateMasterEntities entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateState(StateMasterEntities entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteState(StateMasterEntities entity)
        {
            throw new NotImplementedException();
        }
    }
}
