using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.Repositories.DashBoard.Interfaces;

namespace BISERPBusinessLayer.Repositories.DashBoard.Classes
{
    public class StoreDashboardRepository : IStoreDashboardRepository
    {
        public  List<DashboardRequestCounts> GetGRNCounts(DateTime? fromDt, DateTime? toDt)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDt, DbType.DateTime));
                paramCollection.Add(new DBParameter("TODATE", toDt, DbType.DateTime));

                DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.GetGRNCounts, paramCollection, CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                                .Select(row => new DashboardRequestCounts
                                {
                                    RequestType = row.Field<string>("RequestType"),
                                    RequestCount = row.Field<string>("RequestCount"),
                                    Icon = row.Field<string>("icon"),
                                    MenuId = row.Field<string>("MenuId")
                                }).ToList();

            }
            return entity;
        }
        public List<DashboardRequestCounts> GetMIndentCount(DateTime? fromDt, DateTime? toDt)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDt, DbType.DateTime));
                paramCollection.Add(new DBParameter("TODATE", toDt, DbType.DateTime));

                DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.GetMIndentCount, paramCollection, CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                                .Select(row => new DashboardRequestCounts
                                {
                                    RequestType = row.Field<string>("RequestType"),
                                    RequestCount = row.Field<string>("RequestCount"),
                                    Icon = row.Field<string>("icon"),
                                    MenuId = row.Field<string>("MenuId")
                                }).ToList();

            }
            return entity;
        }
        public List<DashboardRequestCounts> GetMIssueCount(DateTime? fromDt, DateTime? toDt)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDt, DbType.DateTime));
                paramCollection.Add(new DBParameter("TODATE", toDt, DbType.DateTime));

                DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.GetMIssueCount, paramCollection, CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                                .Select(row => new DashboardRequestCounts
                                {
                                    RequestType = row.Field<string>("RequestType"),
                                    RequestCount = row.Field<string>("RequestCount"),
                                    Icon = row.Field<string>("icon"),
                                    MenuId = row.Field<string>("MenuId")
                                }).ToList();

            }
            return entity;
        }

        public List<DashboardRequestCounts> GetVIssueCount(DateTime? fromDt, DateTime? toDt)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDt, DbType.DateTime));
                paramCollection.Add(new DBParameter("TODATE", toDt, DbType.DateTime));

                DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.GetVIssueCount, paramCollection, CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                                .Select(row => new DashboardRequestCounts
                                {
                                    RequestType = row.Field<string>("RequestType"),
                                    RequestCount = row.Field<string>("RequestCount"),
                                    Icon = row.Field<string>("icon"),
                                    MenuId = row.Field<string>("MenuId")
                                }).ToList();

            }
            return entity;
        }

        public List<DashboardRequestCounts> GetMReturnCount(DateTime? fromDt, DateTime? toDt,int type)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDt, DbType.DateTime));
                paramCollection.Add(new DBParameter("TODATE", toDt, DbType.DateTime));
                if (type == 4)
                {
                    DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.GetMReturnCount, paramCollection, CommandType.StoredProcedure);
                    entity = dtSummary.AsEnumerable()
                                    .Select(row => new DashboardRequestCounts
                                    {
                                        RequestType = row.Field<string>("RequestType"),
                                        RequestCount = row.Field<string>("RequestCount"),
                                        Icon = row.Field<string>("icon"),
                                        MenuId = row.Field<string>("MenuId")
                                    }).ToList();
                }
                if (type == 5)
                {
                    DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.GetStockDisposeCount, paramCollection, CommandType.StoredProcedure);
                    entity = dtSummary.AsEnumerable()
                                    .Select(row => new DashboardRequestCounts
                                    {
                                        RequestType = row.Field<string>("RequestType"),
                                        RequestCount = row.Field<string>("RequestCount"),
                                        Icon = row.Field<string>("icon"),
                                        MenuId = row.Field<string>("MenuId")
                                    }).ToList();
                }

            }
            return entity;
        }

        public List<DashboardRequestCounts> GetStockAdjustmentCount(DateTime? fromDt, DateTime? toDt, int type)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDt, DbType.DateTime));
                paramCollection.Add(new DBParameter("TODATE", toDt, DbType.DateTime));
                if(type==6)
                {
                    DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.GetStockAdjustmentCount, paramCollection, CommandType.StoredProcedure);
                    entity = dtSummary.AsEnumerable()
                                    .Select(row => new DashboardRequestCounts
                                    {
                                        RequestType = row.Field<string>("RequestType"),
                                        RequestCount = row.Field<string>("RequestCount"),
                                        Icon = row.Field<string>("icon"),
                                        MenuId = row.Field<string>("MenuId")
                                    }).ToList();
                }
                if (type == 7)
                {
                    DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.GetOpeningBalanceCount, paramCollection, CommandType.StoredProcedure);
                    entity = dtSummary.AsEnumerable()
                                    .Select(row => new DashboardRequestCounts
                                    {
                                        RequestType = row.Field<string>("RequestType"),
                                        RequestCount = row.Field<string>("RequestCount"),
                                        Icon = row.Field<string>("icon"),
                                        MenuId = row.Field<string>("MenuId")
                                    }).ToList();
                }
            }
            return entity;
        }

        public List<DashboardRequestCounts> SendMailStatusOfDashBoard(DateTime? fromDt, DateTime? toDt)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDt, DbType.DateTime));
                paramCollection.Add(new DBParameter("TODATE", toDt, DbType.DateTime));

                DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.SendMailStatusOfDashBoard, paramCollection, CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                                .Select(row => new DashboardRequestCounts
                                {
                                    RequestType = row.Field<string>("RequestType"),
                                    RequestCount = row.Field<string>("RequestCount"),
                                }).ToList();

            }
            return entity;
        }
    }
}
