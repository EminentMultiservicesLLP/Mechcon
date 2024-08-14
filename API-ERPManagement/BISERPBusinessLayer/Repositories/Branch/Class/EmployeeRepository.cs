using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.QueryCollection.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Class
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IEnumerable<EmployeeEntity> GetAllEmployee(int UserId)
        {
            List<EmployeeEntity> employee = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtBranches = dbHelper.ExecuteDataTable(BranchQueries.GetAllEmployee, param, CommandType.StoredProcedure);
                employee = dtBranches.AsEnumerable()
                            .Select(row => new EmployeeEntity
                            {
                                BranchId = row.Field<int>("BrLocId"),
                                BranchName = row.Field<string>("BrLocName"),
                                EmpId = row.Field<long>("EmpId"),
                                TicketCode = row.Field<string>("TicketCode"),
                                EmployeeName = row.Field<string>("EmpName"),
                                FirstName = row.Field<string>("EmpFName"),
                                MiddleName = row.Field<string>("EmpMName"),
                                LastName = row.Field<string>("EmpLName"),
                                Age = row.Field<int>("Age"),
                                Status = row.Field<string>("EmployeeStatus"),
                                Designation = row.Field<string>("DesignationName"),
                                Gender = row.Field<string>("Gender"),
                                WorkedMonth = row.Field<int>("WorkedMonth")
                            }).ToList();
                if (employee == null || employee.Count == 0)
                    employee.Add(new EmployeeEntity
                    {
                        BranchId = 0,
                        BranchName = "",
                        EmpId = 0,
                        EmployeeName = "",
                    });
            }
            return employee;
        }


        public IEnumerable<EmployeeEntity> GetAllEmployeeForGuarantor(int UserId)
        {
            List<EmployeeEntity> employee = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtBranches = dbHelper.ExecuteDataTable(BranchQueries.GetMarvelEmployeeListguarantor, param, CommandType.StoredProcedure);
                employee = dtBranches.AsEnumerable()
                            .Select(row => new EmployeeEntity
                            {
                                BranchId = row.Field<int>("BrLocId"),
                                BranchName = row.Field<string>("BrLocName"),
                                EmpId = row.Field<long>("EmpId"),
                                TicketCode = row.Field<string>("TicketCode"),
                                EmployeeName = row.Field<string>("EmpName"),
                                FirstName = row.Field<string>("EmpFName"),
                                MiddleName = row.Field<string>("EmpMName"),
                                LastName = row.Field<string>("EmpLName"),
                                Designation = row.Field<string>("DesignationName"),
                                StrIsOfficeStaff = row.Field<string>("StrOfficeStaff"),
                            }).ToList();
                if (employee == null || employee.Count == 0)
                    employee.Add(new EmployeeEntity
                    {
                        BranchId = 0,
                        BranchName = "",
                        EmpId = 0,
                        EmployeeName = "",
                    });
            }
            return employee;
        }
    }
}
