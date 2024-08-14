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
    public class ProjectLogicRepository : IProjectLogicRepository
    {
        private static readonly ILogger _loggger = Logger.Register(typeof(ProjectLogicRepository));

        public IEnumerable<ProjectLogicEntity> GetProjectLogic()
        {
            List<ProjectLogicEntity> logics = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtStores = dbHelper.ExecuteDataTable(ConfigurationQueries.GetProjectLogic, CommandType.StoredProcedure);
                logics = dtStores.AsEnumerable()
                            .Select(row => new ProjectLogicEntity
                            {
                                Id = row.Field<int>("Id"),
                                Code = row.Field<string>("Code"),
                                FinancialYear = row.Field<string>("FinancialYear")
                            }).ToList();
            }
            return logics;
        }
    }
}
