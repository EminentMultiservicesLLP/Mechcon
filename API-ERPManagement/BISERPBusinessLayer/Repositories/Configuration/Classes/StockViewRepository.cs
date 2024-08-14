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
    public class StockViewRepository : IStockViewRepository
    {
        private static readonly ILogger _loggger = Logger.Register(typeof(StockViewRepository));

        public IEnumerable<StockViewEntity> GetStockDetails(int StoreID)
        {
            List<StockViewEntity> details = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StoreID", StoreID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(ConfigurationQueries.GetStockDetails,param, CommandType.StoredProcedure);
                details = dt.AsEnumerable()
                            .Select(row => new StockViewEntity
                            {
                                ItemID = row.Field<int>("ItemID"),
                                ItemName = row.Field<string>("ItemName"),
                                ItemTypeName = row.Field<string>("ItemTypeName"),
                                TotalQty = row.Field<double>("TotalQty"),
                                TotalAmount = row.Field<double>("TotalAmount"),
                                GrandTotal = row.Field<double>("GrandTotal"),
                            }).ToList();
            }
            return details;
        }
    }
}
