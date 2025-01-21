using BISERPBusinessLayer.Entities.AdminPanel;
using BISERPBusinessLayer.QueryCollection.AdminPanel;
using BISERPBusinessLayer.Repositories.AdminPanel.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.AdminPanel.Classes
{
    public class GroupMasterRepository : IGroupMasterRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(GroupMasterRepository));



        public GroupMaster SaveGroupMaster(GroupMaster entity)
        {
            int NewGroupID = 0;
            var GroupUserMapping = entity.GroupUserMapping != null ? Commons.ToXML(entity.GroupUserMapping) : null;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("GroupID", entity.GroupID, DbType.Int32));
                    paramCollection.Add(new DBParameter("GroupName", entity.GroupName, DbType.String));
                    paramCollection.Add(new DBParameter("GroupDesc", entity.GroupDesc, DbType.String));
                    paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                    paramCollection.Add(new DBParameter("GroupUserMapping", GroupUserMapping, DbType.Xml));
                    paramCollection.Add(new DBParameter("CreatedBy", entity.CreatedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));

                    paramCollection.Add(new DBParameter("NewGroupID", NewGroupID, DbType.Int32, ParameterDirection.Output));

                    var parameterList = dbHelper.ExecuteNonQueryForOutParameter(AdminPanelQueries.SaveGroupMaster, paramCollection, CommandType.StoredProcedure);

                    entity.GroupID = Convert.ToInt32(parameterList["NewGroupID"]);

                    dbHelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in GroupMasterRepository method of SaveGroupMaster request Repository : parameter :" + Environment.NewLine + ex.StackTrace);
                }
            }
            return entity;
        }

        public IEnumerable<GroupMaster> GetGroupMaster(int? statusID)
        {
            List<GroupMaster> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StatusID", statusID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(AdminPanelQueries.GetGroupMaster, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new GroupMaster
                            {
                                GroupID = row.Field<int>("GroupID"),
                                GroupName = row.Field<string>("GroupName"),
                                GroupDesc = row.Field<string>("GroupDesc"),
                                Deactive = row.Field<bool>("Deactive"),
                            }).ToList();
            }
            return data;
        }

        public IEnumerable<GroupUserMapping> GetGroupUserMapping(int? GroupID)
        {
            List<GroupUserMapping> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("GroupID", GroupID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(AdminPanelQueries.GetGroupUserMapping, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new GroupUserMapping
                            {
                                UserID = row.Field<int>("UserID"),
                                UserCode = row.Field<string>("UserCode"),
                                UserName = row.Field<string>("UserName"),
                                State = row.Field<bool>("State"),
                            }).ToList();
            }
            return data;
        }

        public IEnumerable<EmployeeEnrollmentEntity> GetUserByGroups(string GroupIDs)
        {
            List<EmployeeEnrollmentEntity> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("GroupIDs", GroupIDs, DbType.String);
                DataTable dt = dbHelper.ExecuteDataTable(AdminPanelQueries.GetUserByGroups, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new EmployeeEnrollmentEntity
                            {
                                UserID = row.Field<int>("UserID"),
                                UserCode = row.Field<string>("UserCode"),
                                UserName = row.Field<string>("UserName"),
                                EmailId = row.Field<string>("EmailId"),
                            }).ToList();
            }
            return data;
        }
    }
}
