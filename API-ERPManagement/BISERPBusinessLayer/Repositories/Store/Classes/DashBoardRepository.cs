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

        public IEnumerable<DashBoardCountSummuryModel> GetDashBoardCountSummury(string FromDate, string ToDate, int? ProjectID)
        {
            List<DashBoardCountSummuryModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));
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
    }
}
