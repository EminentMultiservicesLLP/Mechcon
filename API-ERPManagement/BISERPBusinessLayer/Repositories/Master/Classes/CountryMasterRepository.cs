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
    public class CountryMasterRepository : ICountryMasterRepository
    {
        public CountryMasterEntities GetCountryById(int Id)
        {
            CountryMasterEntities country = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreId", Id, DbType.Int32);
                DataTable dtCountry = dbHelper.ExecuteDataTable(MasterQueries.GetStoreMasterById, param, CommandType.StoredProcedure);

                country = dtCountry.AsEnumerable()
                            .Select(row => new CountryMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                CountryName = row.Field<string>("CountryName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).FirstOrDefault();
            }
            return country;
        }

        public IEnumerable<CountryMasterEntities> GetAllCountrys()
        {
            List<CountryMasterEntities> countries = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStores = dbHelper.ExecuteDataTable(MasterQueries.GetAllCountryMaster, CommandType.StoredProcedure);
                countries = dtStores.AsEnumerable()
                            .Select(row => new CountryMasterEntities
                            {
                                ID = row.Field<int>("ID"),
                                CountryName = row.Field<string>("CountryName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return countries;
        }

        public bool CreateCountry(CountryMasterEntities entity)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCountry(CountryMasterEntities entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCountry(CountryMasterEntities entity)
        {
            throw new NotImplementedException();
        }
    }
}
