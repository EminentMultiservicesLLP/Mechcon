using BISERPBusinessLayer.Entities.UserManagement;
using BISERPBusinessLayer.QueryCollection.AdminPanel;
using BISERPBusinessLayer.Repositories.AdminPanel.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BISERPBusinessLayer.Repositories.AdminPanel.Classes
{
    public class RoleAccessRepository : IRoleAccessRepository
    {
        //public int SaveRoleAccess(UserMenuRightsEntity Items)
        //{
        //    int iResult = 0;
        //    using (DBHelper dbHelper = new DBHelper())
        //    {
        //        if (Items.RoleId > 0)
        //        { dbHelper.ExecuteNonQuery("DELETE FROM UM_MENU_ROLE WHERE ROLEID = " + Items.RoleId); }

        //        var tempResult = CreateRole(Items, dbHelper);
        //        if (tempResult.RoleId > 0)
        //        {
        //            foreach (var detail in Items.parent)
        //            {
        //                detail.RoleId = tempResult.RoleId;
        //                detail.InsertedBy = Items.InsertedBy;
        //                detail.InsertedOn = Items.InsertedOn;
        //                detail.InsertedIpAddress = Items.InsertedIpAddress;
        //                detail.InsertedMacName = Items.InsertedMacName;
        //                detail.InsertedMacId = Items.InsertedMacId;
        //                SaveRoleAccessItem(detail, dbHelper);
        //                DataTable tempDetail = dbHelper.ExecuteDataTable("select parentmenuid from um_mst_menu where menuid = " + detail.MenuId);

        //                foreach (DataRow dtRow in tempDetail.Rows)
        //                {
        //                    detail.MenuId = dtRow.Field<int>("parentmenuid");
        //                    //detail.MenuId = Convert.ToInt32(dtRow);
        //                    SaveRoleAccessItem(detail, dbHelper);
        //                }


        //            }

        //            //foreach (var detail in Items.menulist1)
        //            //{
        //            //    detail.RoleId = tempResult.RoleId;
        //            //    detail.InsertedBy = Items.InsertedBy;
        //            //    detail.InsertedOn = Items.InsertedOn;
        //            //    detail.InsertedIpAddress = Items.InsertedIpAddress;
        //            //    detail.InsertedMacName = Items.InsertedMacName;
        //            //    detail.InsertedMacId = Items.InsertedMacId;
        //            //    SaveRoleAccessItem(detail, dbHelper);
        //            //    //DataTable tempDetail=dbHelper.ExecuteDataTable("select menuid from um_mst_menu where parentmenuid = " + detail.MenuId);

        //            //    //foreach (DataRow dtRow in tempDetail.Rows)
        //            //    //{
        //            //    //    detail.MenuId = dtRow.Field<int>("menuid");
        //            //    //    //detail.MenuId = Convert.ToInt32(dtRow);
        //            //    //    SaveRoleAccessItem(detail, dbHelper);
        //            //    //}


        //            //}

        //            //foreach (var detail in Items.menulist2)
        //            //{
        //            //    detail.RoleId = tempResult.RoleId;
        //            //    detail.InsertedBy = Items.InsertedBy;
        //            //    detail.InsertedOn = Items.InsertedOn;
        //            //    detail.InsertedIpAddress = Items.InsertedIpAddress;
        //            //    detail.InsertedMacName = Items.InsertedMacName;
        //            //    detail.InsertedMacId = Items.InsertedMacId;
        //            //    SaveRoleAccessItem(detail, dbHelper);
        //            //    //DataTable tempDetail=dbHelper.ExecuteDataTable("select menuid from um_mst_menu where parentmenuid = " + detail.MenuId);

        //            //    //foreach (DataRow dtRow in tempDetail.Rows)
        //            //    //{
        //            //    //    detail.MenuId = dtRow.Field<int>("menuid");
        //            //    //    //detail.MenuId = Convert.ToInt32(dtRow);
        //            //    //    SaveRoleAccessItem(detail, dbHelper);
        //            //    //}


        //            //}
        //        }
        //    }
        //    return iResult;
        //}
        //public ParentMenuRights SaveRoleAccessItem(ParentMenuRights Items, DBHelper dbHelper)
        //{
        //    int iResult = 0;

        //    DBParameterCollection paramCollection = new DBParameterCollection();
        //    paramCollection.Add(new DBParameter("MenuId", Items.MenuId, DbType.Int32));
        //    paramCollection.Add(new DBParameter("RoleId", Items.RoleId, DbType.Int32));
        //    paramCollection.Add(new DBParameter("InsertedBy", Items.InsertedBy, DbType.Int32));
        //    paramCollection.Add(new DBParameter("InsertedOn", Items.InsertedOn, DbType.DateTime));
        //    paramCollection.Add(new DBParameter("InsertedIpAddress", Items.InsertedIpAddress, DbType.String));
        //    paramCollection.Add(new DBParameter("InsertedMacName", Items.InsertedMacName, DbType.String));
        //    paramCollection.Add(new DBParameter("InsertedMacId", Items.InsertedMacId, DbType.String));
        //    dbHelper.ExecuteNonQuery(AdminPanelQueries.SaveRoleAccess, paramCollection, CommandType.StoredProcedure);
        //    return Items;
        //}
        //public UserMenuRightsEntity CreateRole(UserMenuRightsEntity Items, DBHelper dbHelper)
        //{
        //    int iResult = 0;

        //    DBParameterCollection paramCollection = new DBParameterCollection();
        //    paramCollection.Add(new DBParameter("RoleId", Items.RoleId, DbType.Int32, ParameterDirection.InputOutput));
        //    paramCollection.Add(new DBParameter("RoleName", Items.RoleName, DbType.String));
        //    var parameterList = dbHelper.ExecuteNonQueryForOutParameter<int>(AdminPanelQueries.CreateRoleAccess, paramCollection, CommandType.StoredProcedure, "RoleId");
        //    Items.RoleId = parameterList;

        //    return Items;
        //}

        //public int SaveUserAccess(UserMenuRightsEntity Items)
        //{
        //    int iResult = 0;
        //    using (DBHelper dbHelper = new DBHelper())
        //    {
        //        if (Items.UserId > 0)
        //        { dbHelper.ExecuteNonQuery("DELETE FROM um_menurights WHERE UserId = " + Items.UserId); }
        //        foreach (var detail in Items.parent)
        //        {
        //            detail.UserId = Items.UserId;
        //            detail.InsertedBy = Items.InsertedBy;
        //            detail.InsertedOn = Items.InsertedOn;
        //            detail.InsertedIpAddress = Items.InsertedIpAddress;
        //            detail.InsertedMacName = Items.InsertedMacName;
        //            detail.InsertedMacId = Items.InsertedMacId;
        //            SaveUserAccessItem(detail, dbHelper);
        //            object tempDetail = dbHelper.ExecuteScalar("select parentmenuid from um_mst_menu where menuid = " + detail.MenuId);
        //            if (Convert.ToInt32(tempDetail) > 0)
        //            {
        //                detail.MenuId = Convert.ToInt32(tempDetail);
        //                SaveUserAccessItem(detail, dbHelper);
        //            }

        //        }
        //        //foreach (var detail in Items.menulist1)
        //        //{
        //        //    detail.UserId = Items.UserId;
        //        //    detail.InsertedBy = Items.InsertedBy;
        //        //    detail.InsertedOn = Items.InsertedOn;
        //        //    detail.InsertedIpAddress = Items.InsertedIpAddress;
        //        //    detail.InsertedMacName = Items.InsertedMacName;
        //        //    detail.InsertedMacId = Items.InsertedMacId;
        //        //    SaveUserAccessItem(detail, dbHelper);
        //        //    //object tempDetail = dbHelper.ExecuteScalar("select menuid from um_mst_menu where parentmenuid = " + detail.MenuId);
        //        //    //if (Convert.ToInt32(tempDetail) > 0)
        //        //    //{
        //        //    //    detail.MenuId = Convert.ToInt32(tempDetail);
        //        //    //    SaveUserAccessItem(detail, dbHelper);
        //        //    //}

        //        //}
        //        //foreach (var detail in Items.menulist2)
        //        //{
        //        //    detail.UserId = Items.UserId;
        //        //    detail.InsertedBy = Items.InsertedBy;
        //        //    detail.InsertedOn = Items.InsertedOn;
        //        //    detail.InsertedIpAddress = Items.InsertedIpAddress;
        //        //    detail.InsertedMacName = Items.InsertedMacName;
        //        //    detail.InsertedMacId = Items.InsertedMacId;
        //        //    SaveUserAccessItem(detail, dbHelper);
        //        //    //object tempDetail = dbHelper.ExecuteScalar("select menuid from um_mst_menu where parentmenuid = " + detail.MenuId);
        //        //    //if (Convert.ToInt32(tempDetail) > 0)
        //        //    //{
        //        //    //    detail.MenuId = Convert.ToInt32(tempDetail);
        //        //    //    SaveUserAccessItem(detail, dbHelper);
        //        //    //}

        //        //}

        //    }
        //    return iResult;
        //}
        //public ParentMenuRights SaveUserAccessItem(ParentMenuRights Items, DBHelper dbHelper)
        //{
        //    int iResult = 0;

        //    DBParameterCollection paramCollection = new DBParameterCollection();
        //    paramCollection.Add(new DBParameter("MenuId", Items.MenuId, DbType.Int32));
        //    paramCollection.Add(new DBParameter("UserId", Items.UserId, DbType.Int32));
        //    paramCollection.Add(new DBParameter("InsertedBy", Items.InsertedBy, DbType.Int32));
        //    paramCollection.Add(new DBParameter("InsertedOn", Items.InsertedOn, DbType.DateTime));
        //    paramCollection.Add(new DBParameter("InsertedIpAddress", Items.InsertedIpAddress, DbType.String));
        //    paramCollection.Add(new DBParameter("InsertedMacName", Items.InsertedMacName, DbType.String));
        //    paramCollection.Add(new DBParameter("InsertedMacId", Items.InsertedMacId, DbType.String));
        //    dbHelper.ExecuteNonQuery(AdminPanelQueries.SaveUserAccess, paramCollection, CommandType.StoredProcedure);
        //    return Items;
        //}


        //public int DeleteRole(int roleid)
        //{
        //    int iResult = 0;
        //    using (DBHelper dbHelper = new DBHelper())
        //    {

        //        DBParameterCollection paramCollection = new DBParameterCollection();
        //        paramCollection.Add(new DBParameter("RoleId", roleid, DbType.Int32));
        //        iResult = dbHelper.ExecuteNonQuery(AdminPanelQueries.DeleteRole, paramCollection, CommandType.StoredProcedure);
        //    }
        //    return iResult;
        //}
        //public List<Role> GetRoleList()
        //{
        //    List<Role> roles = null;

        //    using (DBHelper dbHelper = new DBHelper())
        //    {
        //        DataTable dtTable = dbHelper.ExecuteDataTable(AdminPanelQueries.GetRoleList, CommandType.StoredProcedure);
        //        roles = dtTable.AsEnumerable()
        //            .Select(row => new Role
        //            {
        //                RoleId = row.Field<int>("ROLEID"),
        //                RoleName = row.Field<string>("ROLENAME")
        //            }).ToList();
        //    }
        //    return roles;
        //}


        //public List<ParentMenuRights> GetMenuByRole(int roleid)
        //{
        //    List<ParentMenuRights> roles = null;
        //    DBParameterCollection paramCollection = new DBParameterCollection();
        //    paramCollection.Add(new DBParameter("roleid", roleid, DbType.String));
        //    using (DBHelper dbHelper = new DBHelper())
        //    {
        //        DataTable dtTable = dbHelper.ExecuteDataTable(AdminPanelQueries.GetMenuByRole, paramCollection, CommandType.StoredProcedure);
        //        roles = dtTable.AsEnumerable()
        //            .Select(row => new ParentMenuRights
        //            {
        //                MenuId = row.Field<int>("MenuId"),
        //                MenuName = row.Field<string>("MenuName"),
        //                PageName = row.Field<string>("PageName"),
        //                ParentMenuId = row.Field<int>("ParentMenuId"),
        //                State = Convert.ToBoolean(row.Field<int?>("State"))
        //            }).ToList();
        //    }
        //    return roles;
        //}



        //public List<ParentMenuRights> GetMenuByUser(int UserId)
        //{
        //    List<ParentMenuRights> Access = null;
        //    DBParameterCollection paramCollection = new DBParameterCollection();
        //    paramCollection.Add(new DBParameter("UserId", UserId, DbType.String));
        //    using (DBHelper dbHelper = new DBHelper())
        //    {
        //        DataTable dtTable = dbHelper.ExecuteDataTable(AdminPanelQueries.GetMenuByUser, paramCollection, CommandType.StoredProcedure);
        //        Access = dtTable.AsEnumerable()
        //            .Select(row => new ParentMenuRights
        //            {
        //                MenuId = row.Field<int>("MenuId"),
        //                MenuName = row.Field<string>("MenuName"),
        //                PageName = row.Field<string>("PageName"),
        //                ParentMenuId = row.Field<int>("ParentMenuId"),
        //                State = Convert.ToBoolean(row.Field<int?>("State"))

        //            }).ToList();
        //    }
        //    return Access;

        //}

        //public bool CheckDuplicateItem(int RoleId, string RoleName)
        //{
        //    bool bResult = false;
        //    using (DBHelper dbHelper = new DBHelper())
        //    {
        //        DBParameterCollection paramCollection = new DBParameterCollection();
        //        paramCollection.Add(new DBParameter("Type", "RoleCreation", DbType.String));
        //        paramCollection.Add(new DBParameter("Code", RoleId, DbType.String));
        //        paramCollection.Add(new DBParameter("Name", RoleName, DbType.String));
        //        paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

        //        bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(AdminPanelQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
        //    }
        //    return bResult;
        //}
        public List<MenuRole> GetUsers()
        {
            List<MenuRole> Users = null;

            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtTable = dbHelper.ExecuteDataTable(AdminPanelQueries.GetUsers, CommandType.StoredProcedure);
                Users = dtTable.AsEnumerable()
                    .Select(row => new MenuRole()
                    {
                        UserId = row.Field<int>("UserId"),
                        UserName = row.Field<string>("UserName")
                    }).ToList();
            }
            return Users;
        }

        //---------------------------------new 2024
        public List<RoleAccess> GetParentMenu()
        {
            List<RoleAccess> _result = null;
            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    DataTable dtTable = dbHelper.ExecuteDataTable(AdminPanelQueries.GetParentMenu, CommandType.StoredProcedure);
                    _result = dtTable.AsEnumerable()
                        .Select(row => new RoleAccess
                        {
                            MenuId = row.Field<int>("MenuId"),
                            MenuName = row.Field<string>("MenuName")
                        }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Console.WriteLine(ex);
            });
            return _result;

        }
        public List<UserMenuAccess> GetUserAccess(int LoginId, int UserId)
        {
            List<UserMenuAccess> _result = null;
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("LoginId", LoginId, DbType.Int32));
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                using (DBHelper dbHelper = new DBHelper())
                {
                    DataTable dtTable = dbHelper.ExecuteDataTable(AdminPanelQueries.GetUserAccess, paramCollection, CommandType.StoredProcedure);
                    _result = dtTable.AsEnumerable()
                        .Select(row => new UserMenuAccess
                        {
                            MenuId = row.Field<int>("MenuId"),
                            MenuName = row.Field<string>("MenuName"),
                            ParentMenuId = row.Field<int>("ParentMenuId"),
                            ParentMenuName = row.Field<string>("ParentMenuName"),
                            SubMenuId = row.Field<int>("SubMenuId"),
                            State = row.Field<bool>("AccessState"),
                            IsActionMenu = row.Field<bool>("IsActionMenu")
                        }).ToList();
                }
            }).IfNotNull((ex) =>
            {
                Console.WriteLine(ex);
            });
            return _result;
        }
        public int UserMenuAccess(UserFormAccess model)
        {
            int iResult = 0;
            int iDelete = 0;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollectionDelete = new DBParameterCollection();
                    paramCollectionDelete.Add(new DBParameter("UserId", model.UserId, DbType.Int32));
                    iDelete = dbHelper.ExecuteNonQuery(AdminPanelQueries.DeleteSavedMenuAccess, paramCollectionDelete, transaction, CommandType.StoredProcedure);

                    var UserAccessData = Commons.ToXML(model.UserAccess);

                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("UserId", model.UserId, DbType.Int32));
                    paramCollection.Add(new DBParameter("UserAccessData", UserAccessData, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", model.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedON", model.InsertedON, DbType.DateTime));
                    iResult = dbHelper.ExecuteNonQuery(AdminPanelQueries.SaveMenuAccess, paramCollection, transaction, CommandType.StoredProcedure);

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
