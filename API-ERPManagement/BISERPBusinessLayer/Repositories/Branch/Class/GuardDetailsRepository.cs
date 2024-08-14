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
    public class GuardDetailsRepository : IGuardDetailsRepository
    {

        public int CreateGuardInfo(GuardInfoEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("GuardId", Entity.GuardId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BranchId", Entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("EmpFName", Entity.EmpFName, DbType.String));
                paramCollection.Add(new DBParameter("EmpMName", Entity.EmpMName, DbType.String));
                paramCollection.Add(new DBParameter("EmpLName", Entity.EmpLName, DbType.String));
                paramCollection.Add(new DBParameter("Gender", Entity.Gender, DbType.Int32));
                paramCollection.Add(new DBParameter("Address", Entity.Address, DbType.String));
                paramCollection.Add(new DBParameter("ContactNo", Entity.ContactNo, DbType.String));
                paramCollection.Add(new DBParameter("DateOfBirth", Entity.DateOfBirth, DbType.DateTime));
                paramCollection.Add(new DBParameter("Bloodgroup", Entity.Bloodgroup, DbType.String));
                paramCollection.Add(new DBParameter("Designation", Entity.Designation, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(BranchQueries.BRS_GuardInfo, paramCollection, CommandType.StoredProcedure, "GuardId");
            }
            return iResult;
        }
        public int CreateGuardDetails(GuardDetailsEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", Entity.Id, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("GuardId", Entity.GuardId, DbType.Int32));
                paramCollection.Add(new DBParameter("EmpId", Entity.EmpId, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchId", Entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Reference1", Entity.Reference1, DbType.String));
                paramCollection.Add(new DBParameter("Reference2", Entity.Reference2, DbType.String));
                paramCollection.Add(new DBParameter("Reference3", Entity.Reference3, DbType.String));
                paramCollection.Add(new DBParameter("Reference4", Entity.Reference4, DbType.String));
                paramCollection.Add(new DBParameter("PoliceVerification", Entity.PoliceVerification, DbType.Boolean));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(BranchQueries.BRS_GuardDetails, paramCollection, CommandType.StoredProcedure, "Id");
            }
            return iResult;
        }
        public IEnumerable<GuardInfoEntity> GetAllGuardInfo(int BranchId)
        {
            List<GuardInfoEntity> employee = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("BranchId", BranchId, DbType.Int32);
                DataTable dtBranches = dbHelper.ExecuteDataTable(BranchQueries.GetTempGuardInfo, param, CommandType.StoredProcedure);
                employee = dtBranches.AsEnumerable()
                            .Select(row => new GuardInfoEntity
                            {
                                BranchId = row.Field<int>("BranchId"),
                                GuardId = row.Field<int>("GuardId"),
                                EmpFName = row.Field<string>("Name"),
                                Deactive = row.Field<bool>("Deactive"),
                            }).ToList();
             
            }
            return employee;
        }

    }
}
