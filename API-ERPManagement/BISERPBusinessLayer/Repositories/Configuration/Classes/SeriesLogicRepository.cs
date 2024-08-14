using BISERPBusinessLayer.Entities.Configuration;
using BISERPBusinessLayer.QueryCollection.Configuration;
using BISERPBusinessLayer.Repositories.Configuration.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Configuration.Classes
{
    public class SeriesLogicRepository : ISeriesLogicRepository
    {
        private static readonly ILogger _loggger = Logger.Register(typeof(SeriesLogicRepository));

        public IEnumerable<SeriesLogicEntity> GetSeriesLogic()
        {
            List<SeriesLogicEntity> logics = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStores = dbHelper.ExecuteDataTable(ConfigurationQueries.GetSeriesLogic, CommandType.StoredProcedure);
                logics = dtStores.AsEnumerable()
                            .Select(row => new SeriesLogicEntity
                            {
                                PrefixID = row.Field<int>("PrefixID"),
                                Formula = row.Field<string>("Formula"),
                                FormulaFor = row.Field<string>("FormulaFor")
                            }).ToList();
            }
            return logics;
        }
    }
}
