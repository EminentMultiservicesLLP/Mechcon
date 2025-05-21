using BISERPBusinessLayer.Entities.DashBoard;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.QueryCollection.Common;
using BISERPBusinessLayer.QueryCollection.DashBoard;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class DashBoardRepository : IDashBoardRepository
    {
        public StoreDashBoardEntity GetItemSummary()
        {
            StoreDashBoardEntity entity = new StoreDashBoardEntity();
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtSummary = dbHelper.ExecuteDataTable(FlashDetailsQueries.DashBoardItemSummary, CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                            .Select(row => new StoreDashBoardEntity
                            {
                                TotalItem = row.Field<int>("TotalItem"),
                                ActiveItem = row.Field<int>("ActiveItem"),
                                LowStockItem = row.Field<int>("LowStockItem"),
                                TransitItemQuantity = row.Field<double>("TransitItemQuantity"),
                            }).FirstOrDefault();
            }
            return entity;
        }
		
		public IEnumerable<StoreDSBDStockSummaryEntity> GetStoreDSBDStockSummary(int byValue = 1)
        {
            IEnumerable<StoreDSBDStockSummaryEntity> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtSummary = dbHelper.ExecuteDataTable((byValue == 1? StoreQuery.GetDSBDStockValuationSummaryAllBranchWise:StoreQuery.GetDSBDStockQtySummaryAllBranchWise), CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                            .Select(row => new StoreDSBDStockSummaryEntity
                            {
                                Branch =row.Field<string>("Branch"),
                                Accesorries = row.Field<double>("Accesorries"),
                                HousseKeeping = row.Field<double>("HousseKeeping"),
                                Linen = row.Field<double>("Linen"),
                                Stationary = row.Field<double>("Stationary"),
                                Uniform = row.Field<double>("Uniform"),
                                Others = row.Field<double>("Others"),
                                TotalCost = (byValue ==0?row.Field<double>("TotalCost"):0)
                            }).ToList();
            }
            return entity;
        }

        public IEnumerable<StoreDSBDGuardIssueSummaryEntity> GetStoreDSBDGuardIssueSummary()
        {
            IEnumerable<StoreDSBDGuardIssueSummaryEntity> entity = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtSummary = dbHelper.ExecuteDataTable(StoreQuery.GetDSBDGuardIssueSummaryAllBranchWise, CommandType.StoredProcedure);
                entity = dtSummary.AsEnumerable()
                            .Select(row => new StoreDSBDGuardIssueSummaryEntity
                            {
                                Branch = row.Field<string>("BranchName"),
                                Renewal = row.Field<int>("Renewal"),
                                FreshIssue = row.Field<int>("FreshIssue"),
                                NetAmount = row.Field<double>("NetAmount"),
                                ReceivedAmount = row.Field<double>("ReceivedAmount"),
                                DiscountGiven = row.Field<double>("DiscountGiven"),
                                GrossAmount = row.Field<double>("GrossAmount")
                            }).ToList();
            }
            return entity;
        }
        #region DASHBOARD
        public IEnumerable<DashBoardCountSummuryModel> GetDashBoardCountSummury(int? FinancialYearID, int? ProjectID)
        {
            List<DashBoardCountSummuryModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                param.Add(new DBParameter("ProjectID", ProjectID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(DashBoardQueries.GetDashBoardCountSummury, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                    .Select(row => new DashBoardCountSummuryModel
                    {
                        Type = row.Field<string>("Type"),
                        Count = row.Field<int>("Count")
                    }).ToList();
            }
            return data;
        }

        public IEnumerable<Dashboard_BarTrendModel> DashboardGetMonthlySale(int? FinancialYearID, int? ProjectID)
        {
            List<Dashboard_BarTrendModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                //param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                param.Add(new DBParameter("ProjectID", ProjectID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(DashBoardQueries.GetMonthlySale, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                    .Select(row => new Dashboard_BarTrendModel
                    {
                        MonthName = row.Field<string>("MonthName"),
                        MonthId = row.Field<int>("MonthId"),
                        TotalSale = row.Field<decimal?>("TotalSale")
                    }).ToList();
            }
            return data;
        }

        public IEnumerable<Dashboard_BarTrendModel> DashboardGetMonthlyPurchase(int? FinancialYearID, int? ProjectID)
        {
            List<Dashboard_BarTrendModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                param.Add(new DBParameter("ProjectID", ProjectID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(DashBoardQueries.GetMonthlyPurchase, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                    .Select(row => new Dashboard_BarTrendModel
                    {
                        MonthName = row.Field<string>("MonthName"),
                        MonthId = row.Field<int>("MonthId"),
                        TotalPurchase = row.Field<decimal?>("TotalPurchase")
                    }).ToList();
            }
            return data;
        }

        public IEnumerable<Dashboard_BarTrendModel> DashboardGetMonthlySaleVsTarget(int? FinancialYearID, int? ProjectID)
        {
            List<Dashboard_BarTrendModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                param.Add(new DBParameter("ProjectID", ProjectID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(DashBoardQueries.GetMonthlySaleVsTarget, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                    .Select(row => new Dashboard_BarTrendModel
                    {
                        MonthName = row.Field<string>("MonthName"),
                        MonthId = row.Field<int>("MonthId"),
                        TotalSale = row.Field<decimal?>("TotalSale"),
                        TotalTarget =row.Field<decimal?>("TotalTarget")
                    }).ToList();
            }
            return data;
        }

        public IEnumerable<Dashboard_BarTrendModel> DashboardGetMonthlySaleVsExpense(int? FinancialYearID, int? ProjectID)
        {
            List<Dashboard_BarTrendModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                param.Add(new DBParameter("ProjectID", ProjectID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(DashBoardQueries.GetMonthlySaleVsExpense, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                    .Select(row => new Dashboard_BarTrendModel
                    {
                        MonthName = row.Field<string>("MonthName"),
                        MonthId = row.Field<int>("MonthId"),
                        TotalSale = row.Field<decimal?>("TotalSale"),
                        TotalExpense = row.Field<decimal?>("TotalExpense")
                    }).ToList();
            }
            return data;
        }
        public IEnumerable<Dashboard_MultiChartBarTrendModel> DashboardGetMonthlyResourcewiseTarget(int? FinancialYearID, int? ProjectID)
        {
            List<Dashboard_MultiChartBarTrendModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                param.Add(new DBParameter("ProjectID", ProjectID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(DashBoardQueries.GetMonthlyResourcewiseTarget, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                    .Select(row => new Dashboard_MultiChartBarTrendModel
                    {
                        MonthName = row.Field<string>("MonthName"),
                        ResourceName = row.Field<string>("ResourceName"),
                        TargetValue = row.Field<decimal?>("TargetValue"),
                        WonValue = row.Field<decimal?>("WonValue")
                    }).ToList();
            }
            return data;
        }

        public IEnumerable<Dashboard_ColumnChartModel> DashboardGetProjectStatusDataYearly(int? FinancialYearID, int? ProjectID, int? MaxId)
        {
            List<Dashboard_ColumnChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FinancialYearID", FinancialYearID, DbType.Int32));
                param.Add(new DBParameter("MaxId", MaxId, DbType.Int32));
                param.Add(new DBParameter("ProjectID", ProjectID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(DashBoardQueries.GetProjectStatusDataYearly, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                    .Select(row => new Dashboard_ColumnChartModel
                    {
                        ProjectID = row.Field<int>("ProjectID"),
                        PorjectName = row.Field<string>("ProjectName"),
                        ProjectCode = row.Field<string>("Code"),
                        ProjectBudget = row.Field<double?>("ProjectBudget"),
                        UtilizedCost = row.Field<double?>("UtilizedCost"),
                        RowCount = row.Field<int>("Row_Count"),

                    }).ToList();
            }
            return data;
        }
        #endregion DASHBOARD
    }
}
