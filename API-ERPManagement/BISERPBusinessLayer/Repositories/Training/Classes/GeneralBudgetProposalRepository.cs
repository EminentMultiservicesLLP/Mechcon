using System;
using System.Collections.Generic;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPDataLayer.DataAccess;
using System.Data;
using System.Data.Common;
using System.Linq;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Training.Classes
{
    public class GeneralBudgetProposalRepository : IGeneralBudgetProposalRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(GeneralBudgetProposalRepository));

        public bool CreateGeneralBudgetProposal(GeneralBudgetProposalEntity entity)
        {
            bool isSuccess = false;

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var tempateResult = InsUpdGeneralBudgetTotal(entity, dbHelper, transaction);
                    if (tempateResult)
                    {
                        foreach (var detail in entity.BudgetDtModel)
                        {
                            detail.InsertedBy = entity.InsertedBy;
                            detail.InsertedON = entity.InsertedON;
                            detail.InsertedIPAddress = entity.InsertedIPAddress;
                            detail.InsertedMacName = entity.InsertedMacName;
                            detail.InsertedMacID = entity.InsertedMacID;
                            InsUpdGeneralBudgetDtl(detail, dbHelper, transaction);
                        }

                        dbHelper.CommitTransaction(transaction);
                        isSuccess = true;
                    }
                    else
                    {
                        dbHelper.RollbackTransaction(transaction);
                    }

                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError(
                        "Error in CreateMonthlyExpenditure method of Repository : parameter :" +
                        Environment.NewLine + ex.StackTrace);

                });
            }

            return isSuccess;
        }




        public bool InsUpdGeneralBudgetTotal(GeneralBudgetProposalEntity entity, DBHelper dbHelper, IDbTransaction trans)
        {
            bool isSuccess = false;

            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                //paramCollection.Add(new DBParameter("Id", entity.Id, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("BudgetMonth", entity.BudgetMonth, DbType.Int16));
                paramCollection.Add(new DBParameter("BudgetYear", entity.BudgetYear, DbType.Int16));
                paramCollection.Add(new DBParameter("ExpenseTotal", entity.ExpenseTotal, DbType.Double));
                paramCollection.Add(new DBParameter("ProposedTotal", entity.ProposedTotal, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                //paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdGeneralBudgetProposalTotal,
                    paramCollection, trans, CommandType.StoredProcedure);

                isSuccess = true;

            }).IfNotNull(ex =>
            {
                isSuccess = false;
                throw (ex);
            });
            return isSuccess;
        }

        public int InsUpdGeneralBudgetDtl(GeneralBudgetProposalDtEntity entity, DBHelper dbHelper, IDbTransaction trans)
        {
            int iResult = 0;

            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BudgetDtlId", entity.BudgetDtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("BudgetHeads", entity.BudgetHeads, DbType.String));
                paramCollection.Add(new DBParameter("ActualExpense", entity.ActualExpense, DbType.String));
                paramCollection.Add(new DBParameter("ProposalBudget", entity.ProposalBudget, DbType.String));
                paramCollection.Add(new DBParameter("AuthorizedBudget", entity.AuthorizedBudget, DbType.String));

                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdGeneralBudgetProposalDtl,
                    paramCollection, trans, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });
            return iResult;
        }

        public IEnumerable<GeneralBudgetProposalDtEntity> GetbudgetProposalByMonth(int budgetMonth, int budgetYear)
        {
            List<GeneralBudgetProposalDtEntity> obj;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BudgetMonth", budgetMonth, DbType.Int32));
                paramCollection.Add(new DBParameter("BudgetYear", budgetYear, DbType.Int32));

                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetbudgetProposalByMonth, paramCollection, CommandType.StoredProcedure);
                obj = dtUnits.AsEnumerable().Select(row => new GeneralBudgetProposalDtEntity()
                {
                    BudgetDtlId = row.Field<int>("BudgetDtlId"),
                    BudgetHeads = row.Field<string>("BudgetHeads"),
                    ActualExpense = row.Field<double>("ActualExpense"),
                    ProposalBudget = row.Field<double>("ProposalBudget"),
                    ExpenseTotal = row.Field<double>("ExpenseTotal"),
                    ProposedTotal = row.Field<double>("ProposedTotal"),
                    BalanceAmt = row.Field<double>("BalanceAmt")
                }).ToList();
            }
            return obj;

        }

        public bool CheckDuplicateItem(string code)
        {
            bool bResult;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Type", "BudgetHeads", DbType.String));
                paramCollection.Add(new DBParameter("Code", code, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(TrainingQueries.CheckDuplicateBudgetHead, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }
        public IEnumerable<GeneralBudgetProposalEntity> GetGeneralBudgetProposal()
        {
            List<GeneralBudgetProposalEntity> BudgetProposal;
            using (DBHelper dbHelper = new DBHelper())
            {   
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(TrainingQueries.GetAllGeneralBudgetProposalTotal, CommandType.StoredProcedure);
                BudgetProposal = dtMaterialIndent.AsEnumerable()
                      .Select(row => new GeneralBudgetProposalEntity
                      {
                          BudgetId = row.Field<int>("BudgetId"),
                          MonthYear = row.Field<string>("MonthYear"),
                          ExpenseTotal = row.Field<double>("ExpenseTotal"),
                          ProposedTotal = row.Field<double>("ProposedTotal"),
                      }).ToList();

            }
            return BudgetProposal;
        }

        public IEnumerable<GeneralBudgetProposalDtEntity> GetAllGeneralBudgetProposalDtl(int budgetId)
        {
            List<GeneralBudgetProposalDtEntity> BudgetProposal;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BudgetId", budgetId, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(TrainingQueries.GetAllGeneralBudgetProposalDtl, paramCollection, CommandType.StoredProcedure);
                BudgetProposal = dtMaterialIndent.AsEnumerable()
                      .Select(row => new GeneralBudgetProposalDtEntity
                      {
                          BudgetId = row.Field<int>("BudgetId"),
                          BudgetDtlId = row.Field<int>("BudgetDtlId"),
                          BudgetHeads = row.Field<string>("BudgetHeads"),
                          ActualExpense = row.Field<double>("ActualExpense"),
                          ProposalBudget = row.Field<double>("ProposalBudget"),
                          AuthorizedBudget = row.Field<double>("AuthorizedBudget"),
                      }).ToList();
            }
            return BudgetProposal;
        }
        public bool AuthCancelGeneralBudgetProposal(GeneralBudgetProposalEntity entity)
        {
            int iResult = 0; bool isSuccess = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BudgetId", entity.BudgetId, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthorizedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthDate", entity.InsertedON, DbType.DateTime));
                if (entity.Authorized == true)
                {
                    paramCollection.Add(new DBParameter("Authorized", 1, DbType.Boolean));
                }
                else
                {
                    paramCollection.Add(new DBParameter("cancelled", 1, DbType.Boolean));
                }
                if (entity.BudgetId > 0)
                {
                    foreach (var detail in entity.BudgetDtModel)
                    {
                        detail.InsertedBy = entity.InsertedBy;
                        detail.InsertedON = entity.InsertedON;
                        detail.InsertedIPAddress = entity.InsertedIPAddress;
                        detail.InsertedMacName = entity.InsertedMacName;
                        detail.InsertedMacID = entity.InsertedMacID;
                        InsUpdGeneralBudgetDtl(detail, dbHelper, transaction);
                    }
                    iResult = dbHelper.ExecuteNonQuery(TrainingQueries.UpdGeneralBudgetProposalAuth, paramCollection, CommandType.StoredProcedure);
                    dbHelper.CommitTransaction(transaction);
                    isSuccess = true;
                }
                else
                {
                    dbHelper.RollbackTransaction(transaction);
                }

               
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
