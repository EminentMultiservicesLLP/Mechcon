using BISERPBusinessLayer.Entities.SM;
using BISERPBusinessLayer.QueryCollection.SM;
using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Classes
{
    public class SM_WorkOrderRepository : ISM_WorkOrderRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(SM_WorkOrderRepository));

        public IEnumerable<WorkOrderEntities> GetEnqForWorkOrder(int UserID)
        {
            List<WorkOrderEntities> record = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserID", UserID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetEnqForWorkOrder, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new WorkOrderEntities
                            {
                                OrderBookID = row.Field<int>("OrderBookID"),
                                OrderBookNo = row.Field<string>("OrderBookNo"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                ProposalExists = row.Field<bool>("ProposalExists")
                            }).ToList();
            }
            return record;
        }

    }
}
