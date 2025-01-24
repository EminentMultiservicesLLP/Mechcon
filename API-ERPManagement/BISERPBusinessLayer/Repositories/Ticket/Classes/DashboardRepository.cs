using BISERPBusinessLayer.Entities.Ticket;
using BISERPBusinessLayer.QueryCollection.Ticket;
using BISERPBusinessLayer.Repositories.Ticket.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Ticket.Classes
{
    public class DashboardRepository : IDashboardRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(DashboardRepository));

        public IEnumerable<TicketStatusSummaryEntities> GetTicketStatusSummary(string financialYear)
        {
            List<TicketStatusSummaryEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FinancialYear", financialYear, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(DashboardQueries.GetTicketStatusSummary, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TicketStatusSummaryEntities
                            {
                                StatusID = row.Field<int>("StatusID"),
                                Status = row.Field<string>("Status"),
                                TicketCount = row.Field<int>("TicketCount")
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<TicketStatusRptEntities> GetTicketStatusRpt(string financialYear)
        {
            List<TicketStatusRptEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FinancialYear", financialYear, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(DashboardQueries.GetTicketStatusRpt, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TicketStatusRptEntities
                            {
                                TicketID = row.Field<int>("TicketID"),
                                TicketNo = row.Field<string>("TicketNo"),
                                strTicketDate = row.Field<DateTime?>("TicketDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                                TicketAge = row.Field<int>("TicketAge"),
                                Subject = row.Field<string>("Subject"),
                                ClientName = row.Field<string>("ClientName"),
                                AllocatedToName = row.Field<string>("AllocatedToName"),
                                StatusID = row.Field<int>("StatusID"),
                                Status = row.Field<string>("Status"),
                                Color = row.Field<string>("Color"),
                            }).ToList();
            }
            return data;
        }

    }
}
