using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.DashBoard;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.Repositories.DashBoard.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.DashBoard.Classes
{
    public class PurchaseDashBoardRepository : IPurchaseDashBoardRepository
    {
        public List<ChartModelLastFewMonthTotalParent> SupplierCostLastSixMonth()
        {
            List<ChartModelLastFewMonthTotalParent> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtSummary = dbHelper.ExecuteDataTable(DashBoardQueries.GetSupplierCostLastSixMonth, CommandType.StoredProcedure);
                List<string> oColumns = (from DataColumn dc in dtSummary.Columns where dc.ColumnName != "SUPPLIER" select dc.ColumnName).ToList();

                entity = new List<ChartModelLastFewMonthTotalParent>();
                foreach (DataRow dr in dtSummary.Rows)
                {
                    var row = new ChartModelLastFewMonthTotalParent();
                    row.EntityName = dr["SUPPLIER"].ToString();
                    row.MonthNameValues = new List<ChartModelLastFewMonthTotalChild>();
                    foreach (string dc in oColumns)
                    {
                        row.MonthNameValues.Add(new ChartModelLastFewMonthTotalChild() { Name = dc, Value = Convert.ToDouble(dr[dc]) });
                    }
                    entity.Add(row);
                }

            }
            return entity;
        }

        public List<DashboardRequestCounts> GetPurchaseDashBoardRequestCounts_Quaterly(DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDate, DbType.Int32));
                paramCollection.Add(new DBParameter("TODATE", toDate, DbType.DateTime));

                DataTable dtSummary = dbHelper.ExecuteDataTable(DashBoardQueries.GetPurchaseDashboardRequestCountsQuaterly, paramCollection, CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                                .Select(row => new DashboardRequestCounts
                                {
                                    RequestType = row.Field<string>("RequestType"),
                                    RequestCount = row.Field<string>("RequestCount"),
                                    Icon = row.Field<string>("icon")
                                }).ToList();

            }
            return entity;
        }

        public List<DashboardRequestCounts> GetDashBoardPurchaseIndentRequestCount(DateTime? fromDate, DateTime? toDt)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("TODATE", toDt, DbType.DateTime));

                DataTable dtSummary = dbHelper.ExecuteDataTable(DashBoardQueries.GetDashboardPurchaseIndentRequestCount, paramCollection, CommandType.StoredProcedure);
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

        public List<DashboardRequestCounts> GetDashBoardPurchaseOrderRequestCount(DateTime? fromDate, DateTime? toDt)
        {
            List<DashboardRequestCounts> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FROMDATE", fromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("TODATE", toDt, DbType.DateTime));

                DataTable dtSummary = dbHelper.ExecuteDataTable(DashBoardQueries.GetDashboardPurchaseOrderRequestCount, paramCollection, CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                                .Select(row => new DashboardRequestCounts
                                {
                                    RequestType = row.Field<string>("RequestType"),
                                    RequestCount = row.Field<string>("RequestCount"),
                                    Icon = row.Field<string>("icon"),
                                    MenuId = row.Field<string>("MenuId"),
                                    TotalAmount = row.Field<string>("TotalAmount")
                                }).ToList();

            }
            return entity;
        }
    }
}
