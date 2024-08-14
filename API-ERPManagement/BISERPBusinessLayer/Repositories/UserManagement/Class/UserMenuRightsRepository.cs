using BISERPBusinessLayer.Entities.UserManagement;
using BISERPBusinessLayer.QueryCollection.UserManagement;
using BISERPBusinessLayer.Repositories.UserManagement.Interface;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.UserManagement.Class
{
    public class UserMenuRightsRepository : IUserMenuRightsRepository
    {
        public List<UserMenuRightsEntity> GetAllMenuRights(int UserId, int ParentMenuId)
        {
            List<UserMenuRightsEntity> menu = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                paramCollection.Add(new DBParameter("ParentMenuId", ParentMenuId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(UserAccess.GetUserMenuRights, paramCollection, CommandType.StoredProcedure);
                menu = dtManufacturer.AsEnumerable()
                            .Select(row => new UserMenuRightsEntity
                            {
                                MenuId = row.Field<int>("MenuId"),
                                UserId = row.Field<int>("UserId"),
                                Access = row.Field<bool>("Access"),
                                Add = row.Field<bool>("Add"),
                                Edit = row.Field<bool>("Edit"),
                                DeletePerm = row.Field<bool>("DeletePerm"),
                                SuperPerm = row.Field<bool>("SuperPerm"),
                                MenuName = row.Field<string>("MenuName"),
                                PageName = row.Field<string>("PageName"),
                                IconType = row.Field<string>("IconType"),
                                ParentMenuId = row.Field<int>("ParentMenuId"),
                                SubMenuId = row.Field<int>("SubMenuId"),
                                IsActionMenu = row.Field<bool>("IsActionMenu")
                            }).ToList();
            }
            return menu;
        }

        public List<UserMenuRightsEntity> GetAllMenuRights()
        {
            List<UserMenuRightsEntity> menu = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(UserAccess.GetUserMenuRights, CommandType.StoredProcedure);
                menu = dtManufacturer.AsEnumerable()
                            .Select(row => new UserMenuRightsEntity
                            {
                                MenuId = row.Field<int>("MenuId"),
                                UserId = row.Field<int>("UserId"),
                                Access = row.Field<bool>("Access"),
                                Add = row.Field<bool>("Add"),
                                Edit = row.Field<bool>("Edit"),
                                DeletePerm = row.Field<bool>("DeletePerm"),
                                SuperPerm = row.Field<bool>("SuperPerm"),
                                MenuName = row.Field<string>("MenuName"),
                                PageName = row.Field<string>("PageName"),
                                IconType = row.Field<string>("IconType"),
                                ParentMenuId = row.Field<int>("ParentMenuId"),
                                SubMenuId = row.Field<int>("SubMenuId")
                            }).ToList();
            }
            return menu;
        }

        public List<ReportListEntity> GetReportList(int UserId, int ReportGroupId)
        {
            List<ReportListEntity> reports = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                paramCollection.Add(new DBParameter("ReportGroupId", ReportGroupId, DbType.Int32));
                DataTable dtReports = dbHelper.ExecuteDataTable(UserAccess.GetUserReportRights, paramCollection, CommandType.StoredProcedure);
                reports = dtReports.AsEnumerable()
                            .Select(row => new ReportListEntity
                            {
                                ReportID = row.Field<int>("ReportID"),
                                ReportName = row.Field<string>("ReportName"),
                                ReportFileName = row.Field<string>("ReportFileName"),
                                StoreProcedure = row.Field<string>("StoreProcedure"),
                                ReportDescription = row.Field<string>("ReportDescription")
                            }).ToList();
            }
            return reports;
        }

        public int saveManuSettings(UserMenuRightsEntity model)
        {
            int iResult = 0;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("UserId", model.UserId, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserSideBarMenuCSS", model.UserSideBarMenuCSS, DbType.String));
                    paramCollection.Add(new DBParameter("UserHeaderCss", model.UserHeaderCss, DbType.String));
                    iResult = dbHelper.ExecuteNonQuery(UserAccess.UpdateUserMenuSettings, paramCollection, transaction, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                });
            }
            return iResult;
        }
    }
}
