using BISERPBusinessLayer.Entities.UserManagement;
using BISERPBusinessLayer.QueryCollection.UserManagement;
using BISERPBusinessLayer.Repositories.UserManagement.Interface;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.UserManagement.Class
{
    public class UserDeparment : IUserDeparment
    {
        public List<UserDepartment> GetUserAccessBranch(int UserId)
        {
            List<UserDepartment> dept = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                paramCollection.Add(new DBParameter("DeptFlag", 1, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(UserAccess.GetUserDepartments, paramCollection, CommandType.StoredProcedure);
                dept = dtManufacturer.AsEnumerable()
                            .Select(row => new UserDepartment
                            {
                                BranchId = row.Field<int>("BranchId"),
                                UserId = row.Field<long>("UserId")                                
                            }).ToList();
            }
            return dept;
        }

        public List<UserDepartment> GetUserAccessStore(int UserId)
        {
            List<UserDepartment> dept = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                paramCollection.Add(new DBParameter("DeptFlag", 2, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(UserAccess.GetUserDepartments, paramCollection, CommandType.StoredProcedure);
                dept = dtManufacturer.AsEnumerable()
                            .Select(row => new UserDepartment
                            {
                                StoreId = row.Field<int>("StoreId"),
                                UserId = row.Field<long>("UserId")
                            }).ToList();
            }
            return dept;
        }

        public List<UserDepartment> GetUserAccessUnitStore(int UserId)
        {
            List<UserDepartment> dept = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                paramCollection.Add(new DBParameter("DeptFlag", 3, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(UserAccess.GetUserDepartments, paramCollection, CommandType.StoredProcedure);
                dept = dtManufacturer.AsEnumerable()
                            .Select(row => new UserDepartment
                            {
                                UnitStoreId = row.Field<int>("UnitStoreId"),
                                UserId = row.Field<long>("UserId")
                            }).ToList();
            }
            return dept;
        }
    }
}
