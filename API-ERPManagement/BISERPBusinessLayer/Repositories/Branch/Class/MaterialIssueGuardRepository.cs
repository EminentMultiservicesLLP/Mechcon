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
    public class MaterialIssueGuardRepository : IMaterialIssueGuardRepository
    {

        public MaterialIssueGuardEntity CreateEntity(MaterialIssueGuardEntity entity, DBHelper dbHelper)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IssueNo", entity.IssueNo, DbType.String, 50, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("Issuedate", entity.IssueDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("StartDate", entity.StartDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("InstallmentPeriod", entity.InstallmentPeriod, DbType.Int32));
            paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("BranchID", entity.BranchId, DbType.Int32));
            paramCollection.Add(new DBParameter("EmpId", entity.EmpId, DbType.Int32));
            paramCollection.Add(new DBParameter("NetAmount", entity.NetAmount, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("GrossAmount", entity.GrossAmount, DbType.Double));
            paramCollection.Add(new DBParameter("ReceivedAmount", entity.ReceivedAmount, DbType.Double));
            paramCollection.Add(new DBParameter("AdminCharges", entity.AdminCharges, DbType.Double));
            paramCollection.Add(new DBParameter("OtherCharges", entity.OtherCharges, DbType.Double));
            paramCollection.Add(new DBParameter("BalanceAmount", entity.BalanceAmount, DbType.Double));
            paramCollection.Add(new DBParameter("IsRenewal", entity.IsRenewal, DbType.Boolean));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("Guarantor1", entity.Guarantor1, DbType.Int32));
            paramCollection.Add(new DBParameter("Guarantor2", entity.Guarantor2, DbType.Int32));
            var parameterList = dbHelper.ExecuteNonQueryForOutParameter(BranchQueries.InsertMaterialIssueGuard, paramCollection,  CommandType.StoredProcedure);
            entity.IssueId = Convert.ToInt32(parameterList["IssueId"].ToString());
            entity.IssueNo = parameterList["IssueNo"].ToString();
            return entity;
        }

        public IEnumerable<MaterialIssueGuardEntity> GetIssueList(int StoreId, int EmpId)
        {
            List<MaterialIssueGuardEntity> issuelist = new List<MaterialIssueGuardEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("EmpId", EmpId, DbType.Int32));

                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(BranchQueries.GetMaterialIssueGuardList, paramCollection, CommandType.StoredProcedure);
                issuelist = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueGuardEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                StoreId = row.Field<int>("StoreId"),
                                BranchId = row.Field<int>("BranchId"),
                                EmpId = row.Field<int>("EmpId"),
                                BalanceAmount = row.Field<double>("BalanceAmount"),
                                AdminCharges = row.Field<double>("AdminCharges"),
                                OtherCharges = row.Field<double>("OtherCharges"),
                                ReceivedAmount = row.Field<double>("ReceivedAmount"),
                                InstallmentPeriod = row.Field<int>("InstallmentPeriod"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMM-yyyy"),
                                strStartDate = Convert.ToDateTime(row.Field<DateTime>("StartDate")).ToString("dd-MMM-yyyy")                                
                            }).ToList();
            }
            return issuelist;
        }

        public IEnumerable<MaterialIssueGuardEntity> GetAllGuardIssueList(int UserId)
        {
            List<MaterialIssueGuardEntity> issuelist = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));

                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(BranchQueries.GeAllGuardIssueList, paramCollection, CommandType.StoredProcedure);
                issuelist = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueGuardEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                StoreId = row.Field<int>("StoreId"),
                                BranchId = row.Field<int>("BranchId"),
                                EmpId = row.Field<int>("EmpId"),
                                InstallmentPeriod = row.Field<int>("InstallmentPeriod"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                EmpName = row.Field<string>("EmpName"),
                                TicketCode = row.Field<string>("TicketCode"),
                                StoreName = row.Field<string>("StoreName"),
                                IsRenewal = row.Field<bool>("IsRenewal"),
                                NetAmount = row.Field<double>("NetAmount"),
                                Discount = row.Field<double>("Discount"),
                                GrossAmount = row.Field<double>("GrossAmount"),
                                ReceivedAmount = row.Field<double>("ReceivedAmount"),
                                BalanceAmount = row.Field<double>("BalanceAmount"),
                                AdminCharges = row.Field<double>("AdminCharges"),
                                OtherCharges = row.Field<double>("OtherCharges"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMM-yyyy"),
                                strStartDate = Convert.ToDateTime(row.Field<DateTime>("StartDate")).ToString("dd-MMM-yyyy"),
                                GuarantorName1 = row.Field<string>("GuarantorName1"),
                                GuarantorName2 = row.Field<string>("GuarantorName2"),
                                Guarantor1 = row.Field<int?>("Guarantor1"),
                                Guarantor2 = row.Field<int?>("Guarantor2"),
                            }).ToList();
            }
            return issuelist;
        }

        public bool UpdateEmployeeRecovery(MaterialIssueGuardEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("StartDate", entity.StartDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("InstallmentPeriod", entity.InstallmentPeriod, DbType.Int32));
            paramCollection.Add(new DBParameter("BranchID", entity.BranchId, DbType.Int32));
            paramCollection.Add(new DBParameter("EmpId", entity.EmpId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));            
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));            
            iResult = dbHelper.ExecuteNonQuery(BranchQueries.InsertEmployeeLoanDetails, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<MaterialIssueGuardEntity> AllGaurdMaterialIssue(int branchId)
        {
            List<MaterialIssueGuardEntity> issuelist = null;
            using (DBHelper dbHelper = new DBHelper())
            {

                DBParameter param = new DBParameter("branchId", branchId, DbType.Int32);

                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(BranchQueries.AllGaurdMaterialIssue, param, CommandType.StoredProcedure);
                issuelist = dtMaterialIndent.AsEnumerable()
                            .Select(row => new MaterialIssueGuardEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMM-yyyy"),
                            }).ToList();
            }
            return issuelist;
        }

    }
}
