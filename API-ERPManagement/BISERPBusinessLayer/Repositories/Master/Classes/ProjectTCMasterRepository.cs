using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
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
    public class ProjectTCMasterRepository : IProjectTCMasterRepository
    {
        public IEnumerable<ProjectTCMasterEntities> GetAllProjectTC()
        {
            List<ProjectTCMasterEntities> projectTC = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtprojectTC = dbHelper.ExecuteDataTable(MasterQueries.GetAllProjectTC, CommandType.StoredProcedure);
                projectTC = dtprojectTC.AsEnumerable().Select(row => new ProjectTCMasterEntities
                {
                    ProjectTCID = row.Field<int>("ProjectTCID"),
                    ProjectTCDesc = row.Field<string>("ProjectTCDesc").NullToString(),
                    ProjectTCCode = row.Field<string>("ProjectTCCode"),
                    Deactive = row.Field<Boolean>("Deactive")
                }).ToList();
                if (projectTC == null || projectTC.Count == 0)
                    projectTC.Add(new ProjectTCMasterEntities
                    {
                        ProjectTCID = 0,
                        ProjectTCDesc = "",
                        ProjectTCCode = "",
                        Deactive = false
                    });
            }
            return projectTC;
        }

        public int CreateProjectTC(ProjectTCMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ProjectTCID", entity.ProjectTCID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ProjectTCCode", entity.ProjectTCCode, DbType.String));
                paramCollection.Add(new DBParameter("ProjectTCDesc", entity.ProjectTCDesc, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(MasterQueries.InsertProjectTCMaster, paramCollection, CommandType.StoredProcedure, "ProjectTCID");
            }
            return iResult;
        }

        public bool UpdateProjectTC(ProjectTCMasterEntities entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ProjectTCID", entity.ProjectTCID, DbType.Int32));
                paramCollection.Add(new DBParameter("ProjectTCCode", entity.ProjectTCCode, DbType.String));
                paramCollection.Add(new DBParameter("ProjectTCDesc", entity.ProjectTCDesc, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(MasterQueries.UpdateProjectTCMaster, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool CheckDuplicateItem(string ProjectTCCode)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "ProjectTC", DbType.String));
                paramCollection.Add(new DBParameter("Code", ProjectTCCode, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateItem, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public bool CheckDuplicateupdate(string code, int ID)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "ProjectTC", DbType.String));
                paramCollection.Add(new DBParameter("ID", ID, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(MasterQueries.CheckDuplicateupdate, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
    }
}