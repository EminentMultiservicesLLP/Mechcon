using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;
using BISERPBusinessLayer.QueryCollection.Branch;

namespace BISERPBusinessLayer.Repositories.Branch.Class
{
    public class MaterialIssueGuardCommonRepository : IMaterialIssueGuardCommonRepository
    {
        IMaterialIssueGuardRepository _materialIssueguard;
        IMaterialIssueGuardDtRepository _materialdetailIssueguardDt;
        private static readonly ILogger _loggger = Logger.Register(typeof(MaterialIssueGuardCommonRepository));

        public MaterialIssueGuardCommonRepository(IMaterialIssueGuardRepository materialIssueguard, IMaterialIssueGuardDtRepository materialdetailIssueguardDt)
        {
            _materialIssueguard = materialIssueguard;
            _materialdetailIssueguardDt = materialdetailIssueguardDt;
        }
        public MaterialIssueGuardEntity SaveMaterialIssueGuard(MaterialIssueGuardEntity entity)
        {
            using (DBHelper dbhelper = new DBHelper())
            {
                IDbTransaction transaction = dbhelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var newEntity = _materialIssueguard.CreateEntity(entity, dbhelper);
                    entity.IssueId = newEntity.IssueId;
                    entity.IssueNo = newEntity.IssueNo;
                    if (entity.IssueId > 0)
                    {
                        foreach (var dt in entity.MaterialIssueGuardDt)
                        {
                            dt.IssueDetailsId = _materialdetailIssueguardDt.CreateEntity(entity, dt, dbhelper);
                        }
                        if (!entity.IsRenewal)
                        {
                            _materialIssueguard.UpdateEmployeeRecovery(entity, dbhelper);
                        }
                        dbhelper.CommitTransaction(transaction);
                    }                        
                    else
                    {
                        dbhelper.RollbackTransaction(transaction);
                    }
                }).IfNotNull(ex =>
                {
                    dbhelper.RollbackTransaction(transaction);
                    _loggger.LogError("Error in SaveMaterialIssueGuard method of MaterialIssueGuardCommonController : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }


        public IEnumerable<MaterialIssueGuardEntity> GetGuardIssueReceipt(int branchId, int IssueId)
        {
            List<MaterialIssueGuardEntity> MaterialIssue = new List<MaterialIssueGuardEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("branchId", branchId, DbType.Int32));
                paramCollection.Add(new DBParameter("IssueId", IssueId, DbType.Int32));
                DataTable dtMaterialIssue = dbHelper.ExecuteDataTable(BranchQueries.rptGuardIssueReceipt, paramCollection, CommandType.StoredProcedure);
                MaterialIssue = dtMaterialIssue.AsEnumerable()
                            .Select(row => new MaterialIssueGuardEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchName = row.Field<string>("BranchName"),
                                EmpName = row.Field<string>("EmpName"),
                                GrossAmount = row.Field<double>("GrossAmount"),
                                Discount = row.Field<double>("Discount"),
                                NetAmount = row.Field<double>("NetAmount"),
                                ReceivedAmount = row.Field<double>("ReceivedAmount"),
                                BalanceAmount = row.Field<double>("BalanceAmount"),
                            }).GroupBy(test => test.IssueId).Select(grp => grp.First()).ToList();

                foreach (var M in MaterialIssue)
                {
                    M.MaterialIssueGuardDt = dtMaterialIssue.AsEnumerable().Select(dtrow => new MaterialIssueGuardDtEntity
                    {
                        IssueId = dtrow.Field<int>("IssueId"),
                        IssueDetailsId = dtrow.Field<int>("IssueDetailsId"),
                        ItemId = dtrow.Field<int>("ItemId"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        IssuedQuantity = dtrow.Field<double>("IssuedQuantity"),
                        MRP = dtrow.Field<double>("MRP"),
                        Amount = dtrow.Field<double>("Amount"),
                       
                      
                    }).Where(mo => mo.IssueId == M.IssueId).ToList();
                 
                }
            }
            return MaterialIssue;
        }
        public IEnumerable<MaterialIssueGuardEntity> GetGuardIssueReceiptdt(DateTime fromdate, DateTime todate, int branchId)
        {
            List<MaterialIssueGuardEntity> MaterialIssue = new List<MaterialIssueGuardEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("branchId", branchId, DbType.Int32));
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtMaterialIssue = dbHelper.ExecuteDataTable(BranchQueries.rptGuardIssueReceiptDetails, paramCollection, CommandType.StoredProcedure);

               // DataTable dtMaterialIssue1 = dbHelper.ExecuteDataTable(BranchQueries.rptGuardIssueReceiptDetailsSummery, paramCollection, CommandType.StoredProcedure);

                MaterialIssue = dtMaterialIssue.AsEnumerable()
                            .Select(row => new MaterialIssueGuardEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchName = row.Field<string>("BranchName"),
                                EmpName = row.Field<string>("EmpName"),
                                TicketCode = row.Field<string>("TicketCode"),
                                GrossAmount = row.Field<double>("GrossAmount"),
                                Discount = row.Field<double>("Discount"),
                                NetAmount = row.Field<double>("NetAmount"),
                                ReceivedAmount = row.Field<double>("ReceivedAmount"),
                                BalanceAmount = row.Field<double>("BalanceAmount"),
                                AdminCharges = row.Field<double>("AdminCharges"),
                                OtherCharges = row.Field<double>("OtherCharges"),
                                TotalReceivedAmount = row.Field<double>("TotalReceivedAmount"),
                                TotalDiscount = row.Field<double>("TotalDiscount"),
                                TotalNetAmount = row.Field<double>("TotalNetAmount"),
                                TotalBalanceAmount = row.Field<double>("TotalBalanceAmount"),
                                InstallmentPeriod = row.Field<int>("InstallmentPeriod"),
                                StartDate = row.Field<DateTime>("StartDate"),
                                GuarantorName1 = row.Field<string>("GuarantorName1"),
                                GuarantorName2 = row.Field<string>("GuarantorName2"),
                            }).GroupBy(test => test.IssueId).Select(grp => grp.First()).ToList();

                foreach (var M in MaterialIssue)
                {
                    M.MaterialIssueGuardDt = dtMaterialIssue.AsEnumerable().Select(dtrow => new MaterialIssueGuardDtEntity
                    {
                        IssueId = dtrow.Field<int>("IssueId"),
                        IssueDetailsId = dtrow.Field<int>("IssueDetailsId"),
                        ItemId = dtrow.Field<int>("ItemId"),
                        ItemName = dtrow.Field<string>("ItemName"),
                        IssuedQuantity = dtrow.Field<double>("IssuedQuantity"),
                        MRP = dtrow.Field<double>("MRP"),
                        Amount = dtrow.Field<double>("Amount"),
                    }).Where(mo => mo.IssueId == M.IssueId).ToList();

                }
            }
            return MaterialIssue;
        }
    }
}
