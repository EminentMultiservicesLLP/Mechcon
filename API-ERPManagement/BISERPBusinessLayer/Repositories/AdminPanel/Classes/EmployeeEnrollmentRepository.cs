using BISERPBusinessLayer.Entities.AdminPanel;
using BISERPBusinessLayer.QueryCollection.AdminPanel;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.AdminPanel.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.AdminPanel.Classes
{
    public class EmployeeEnrollmentRepository : IEmployeeEnrollmentRepository
    {
        public IEnumerable<EmployeeEnrollmentEntity> GetUserCode()
        {
            List<EmployeeEnrollmentEntity> items = null;
            using (DBHelper dbHelper = new DBHelper())
            {

                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AdminPanelQueries.GetUserCode, CommandType.StoredProcedure);
                items = dtManufacturer.AsEnumerable()
                            .Select(row => new EmployeeEnrollmentEntity
                            {
                                UserCode = row.Field<string>("UserCode"),

                            }).ToList();

            }
            return items;
        }
        public IEnumerable<EmployeeEnrollmentEntity> GetUserDetails()
        {
            List<EmployeeEnrollmentEntity> items = null;
            using (DBHelper dbHelper = new DBHelper())
            {

                DataTable dtManufacturer = dbHelper.ExecuteDataTable(AdminPanelQueries.GetUserDetails, CommandType.StoredProcedure);
                items = dtManufacturer.AsEnumerable()
                            .Select(row => new EmployeeEnrollmentEntity
                            {
                                UserCode = row.Field<string>("UserCode"),
                                UserID = row.Field<int>("UserID"),
                                UserName = row.Field<string>("UserName"),
                                IsDeactive = row.Field<int>("IsDeactive"),
                                LoginName = row.Field<string>("LoginName"),
                                Password = row.Field<string>("Password"),
                                DepartmentID = row.Field<int>("DepartmentID"),
                                Department = row.Field<string>("Department"),
                                DesignationID = row.Field<int>("DesignationID"),
                                Designation = row.Field<string>("Designation"),
                                EmailId = row.Field<string>("EmailId"),

                            }).ToList();

            }
            return items;
        }
        public int SaveUser(EmployeeEnrollmentEntity Items)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserID", Items.UserID, DbType.Int32));
                paramCollection.Add(new DBParameter("UserCode", Items.UserCode, DbType.String));
                paramCollection.Add(new DBParameter("UserName", Items.UserName, DbType.String));
                paramCollection.Add(new DBParameter("LoginName", Items.LoginName, DbType.String));
                paramCollection.Add(new DBParameter("Password", Items.Password, DbType.String));
                paramCollection.Add(new DBParameter("DepartmentID", Items.DepartmentID, DbType.String));
                paramCollection.Add(new DBParameter("DesignationID", Items.DesignationID, DbType.String));
                paramCollection.Add(new DBParameter("EmailId", Items.EmailId, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Items.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Items.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Items.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Items.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Items.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(AdminPanelQueries.SaveUser, paramCollection, CommandType.StoredProcedure);

            }
            return iResult;
        }
        public bool CheckDuplicateItem(string UserCode, int UserID)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "UserCreation", DbType.String));
                paramCollection.Add(new DBParameter("Code", UserCode, DbType.String));
                paramCollection.Add(new DBParameter("StoreId", UserID, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(AdminPanelQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public int DeleteUser(EmployeeEnrollmentEntity Items)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {



                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserCode", Items.UserCode, DbType.String));
                paramCollection.Add(new DBParameter("UserName", Items.UserName, DbType.String));
                paramCollection.Add(new DBParameter("UserID", Items.UserID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedByUserID", Items.UpdatedByUserID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedON", Items.UpdatedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Items.UpdatedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Items.UpdatedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Items.UpdatedMacID, DbType.String));
                //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AdminPanelQueries.SaveUser, paramCollection, CommandType.StoredProcedure);
                iResult = dbHelper.ExecuteNonQuery(AdminPanelQueries.DeleteUser, paramCollection, CommandType.StoredProcedure);



            }
            return iResult;
        }
        public int ChangePassword(EmployeeEnrollmentEntity Items)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserID", Items.UserID, DbType.Int32));
                paramCollection.Add(new DBParameter("Password", Items.Password, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", Items.UpdatedByUserID, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedON", Items.UpdatedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Items.UpdatedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Items.UpdatedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Items.UpdatedMacID, DbType.String));
                //iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AdminPanelQueries.ChangePassword, paramCollection, CommandType.StoredProcedure);
                iResult = dbHelper.ExecuteNonQuery(AdminPanelQueries.ChangePassword, paramCollection, CommandType.StoredProcedure);
            }
            return iResult;
        }
        public IEnumerable<DepartmentModel> GetDepartments()
        {
            List<DepartmentModel> departments = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(AdminPanelQueries.GetDepartments, CommandType.StoredProcedure);
                departments = dt.AsEnumerable()
                                .Select(row => new DepartmentModel
                                {
                                    DepartmentID = row.Field<int>("DepartmentID"),
                                    Department = row.Field<string>("Department"),
                                    Deactive = row.Field<bool>("Deactive")
                                }).ToList();
            }
            return departments;
        }
        public IEnumerable<DesignationModel> GetDesignations()
        {
            List<DesignationModel> designations = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(AdminPanelQueries.GetDesignations, CommandType.StoredProcedure);
                designations = dt.AsEnumerable()
                                 .Select(row => new DesignationModel
                                 {
                                     DesignationID = row.Field<int>("DesignationID"),
                                     Designation = row.Field<string>("Designation"),
                                     Deactive = row.Field<bool>("Deactive")
                                 }).ToList();
            }
            return designations;
        }

    }
}
