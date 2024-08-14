using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.QueryCollection.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPDataLayer.DataAccess;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BISERPBusinessLayer.Repositories.Transport.Classes
{
    public class DriverRepository : IDriverRepository
    {
        public IEnumerable<DriverEntity> GetAllDriver()
        {
            List<DriverEntity> employee = null;
            using (DBHelper dbHelper = new DBHelper())
            {
             
                DataTable dtBranches = dbHelper.ExecuteDataTable(TransportQuery.GetAllDriver, CommandType.StoredProcedure);
                employee = dtBranches.AsEnumerable()
                            .Select(row => new DriverEntity
                            {
                                EmpId = row.Field<long>("EmpId"),
                                TicketCode = row.Field<string>("TicketCode"),
                                BranchId = row.Field<int>("BranchId"),
                                EmpName = row.Field<string>("EmpName")
                            }).ToList();
            }
            return employee;
        }
        public IEnumerable<DriverEntity> GetAllDriverSchedule(int branchId)
        {
            List<DriverEntity> employee = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("BranchId", branchId, DbType.Int32);
                var dtBranches = dbHelper.ExecuteDataTable(TransportQuery.GetAllDriverSchedule, param,CommandType.StoredProcedure);
                employee = dtBranches.AsEnumerable()
                            .Select(row => new DriverEntity
                            {
                                EmpId = row.Field<long>("EmpId"),
                                TicketCode = row.Field<string>("TicketCode"),
                                BranchId = row.Field<int>("BranchId"),
                                EmpName = row.Field<string>("EmpName")
                            }).ToList();
            }
            return employee;
        }
    }
}
