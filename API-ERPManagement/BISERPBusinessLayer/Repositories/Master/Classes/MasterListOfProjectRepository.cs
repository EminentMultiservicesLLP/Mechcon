using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Master.Classes
{
    public class MasterListOfProjectRepository : IMasterListOfProjectRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(MasterListOfProjectRepository));

        public List<MasterListOfProjectEntity> GetMasterListOfProject(int financialYearID, int pending)
        {
            List<MasterListOfProjectEntity> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FinancialYearID", financialYearID, DbType.Int32));
                paramCollection.Add(new DBParameter("Pending", pending, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetMasterListOfProject, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new MasterListOfProjectEntity
                            {
                                ProjectID = row.Field<int>("ProjectID"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ProjectDescription = row.Field<string>("ProjectDescription"),
                                POBaseValue = row.Field<decimal>("POBaseValue"),
                                POBaseValueWithGST = row.Field<decimal>("POBaseValueWithGST"),
                                strWODate = row.Field<DateTime?>("WODate") != null ? Convert.ToDateTime(row.Field<DateTime?>("WODate")).ToString("dd-MMM-yyyy"): string.Empty,
                                ClientName = row.Field<string>("ClientName"),
                                AllocatedToName = row.Field<string>("AllocatedToName"),
                                ProjectDispatched = row.Field<string>("ProjectDispatched"),
                                strDispatchDate = row.Field<DateTime?>("DispatchDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("DispatchDate")).ToString("dd-MMM-yyyy"): string.Empty,
                            }).ToList();
            }
            return Dtl;
        }
        public List<ProjectCostingSummaryEntity> GetProjectCostingSummary(int financialYearID)
        {
            List<ProjectCostingSummaryEntity> Dtl = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FinancialYearID", financialYearID, DbType.Int32));
                DataTable dt = dbHelper.ExecuteDataTable(MasterQueries.GetProjectCostingSummary, paramCollection, CommandType.StoredProcedure);
                Dtl = dt.AsEnumerable()
                            .Select(row => new ProjectCostingSummaryEntity
                            {
                                ProjectID = row.Field<int>("ProjectID"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ProjectDescription = row.Field<string>("ProjectDescription"),
                                ClientName = row.Field<string>("ClientName"),
                                Sales = row.Field<decimal>("Sales"),
                                Purchases = row.Field<decimal>("Purchases"),
                                PercentPurchases = row.Field<decimal>("PercentPurchases"),
                                DirectCost = row.Field<decimal>("DirectCost"),
                                PercentDirectCost = row.Field<decimal>("PercentDirectCost"),
                                GrossMargin = row.Field<decimal>("GrossMargin"),
                                IndirectCost = row.Field<decimal>("IndirectCost"),
                                PercentIndirectCost = row.Field<decimal>("PercentIndirectCost"),
                                NetMargin = row.Field<decimal>("NetMargin"),
                                PercentNetMargin = row.Field<decimal>("PercentNetMargin")
                            }).ToList();
            }
            return Dtl;
        }
    }
}
