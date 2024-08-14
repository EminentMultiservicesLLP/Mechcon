using System;
using System.Collections.Generic;
using BISERPBusinessLayer.Entities.Training;
using BISERPBusinessLayer.QueryCollection.Training;
using BISERPBusinessLayer.Repositories.Training.Interfaces;
using BISERPDataLayer.DataAccess;
using System.Data;
using System.Data.Common;
using System.Linq;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Training.Classes
{
    public class MonthlyExpenditureRepository : IMonthlyExpenditureRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(MonthlyExpenditureRepository));


        public bool CreateMonthlyExpenditure(MonthlyExpenditureEntity entity)
        {
            bool isSuccess = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    var tempateResult = InsUpdMonthlyExpenditureTotal(entity, dbHelper, transaction);
                    if (tempateResult)
                    {
                        foreach (var detail in entity.ExpenditureModel)
                        {
                            detail.InsertedBy = entity.InsertedBy;
                            detail.InsertedON = entity.InsertedON;
                            detail.InsertedIPAddress = entity.InsertedIPAddress;
                            detail.InsertedMacName = entity.InsertedMacName;
                            detail.InsertedMacID = entity.InsertedMacID;
                            InsUpdMonthlyExpenditure(detail, dbHelper, transaction);
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


        public int InsUpdMonthlyExpenditure(MonthlyExpenditureDtEntity entity, DBHelper dbHelper, IDbTransaction trans)
        {
            int iResult = 0;
            var abc = entity.ExpDate;
            string newdate = "";
            string[] arrdate;
            arrdate=abc.Split('/');
            newdate = arrdate[1].ToString() + "-" + arrdate[0].ToString() + "-" + arrdate[2].ToString();
            DateTime xyz = Convert.ToDateTime(newdate);
            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("DtlId", entity.DtlId, DbType.Int32));
                paramCollection.Add(new DBParameter("BudgetHeads", entity.BudgetHeads, DbType.String));
                paramCollection.Add(new DBParameter("ExpDate", xyz, DbType.String));
                paramCollection.Add(new DBParameter("Reciepts", entity.Reciepts, DbType.String));
                paramCollection.Add(new DBParameter("ReieptsAmt", entity.ReieptsAmt, DbType.Double));
                paramCollection.Add(new DBParameter("BudgetId", entity.BudgetId, DbType.Int32));
                paramCollection.Add(new DBParameter("ExpenseAmt", entity.ExpenseAmt, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                //*******************

                iResult = dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdMonthlyExpenditure, paramCollection, trans, CommandType.StoredProcedure);
            }).IfNotNull(ex => { throw (ex); });
            return iResult;
        }

        public bool InsUpdMonthlyExpenditureTotal(MonthlyExpenditureEntity entity, DBHelper dbHelper,
            IDbTransaction trans)
        {
            bool isSuccess = false;

            TryCatch.Run(() =>
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                //paramCollection.Add(new DBParameter("Id", entity.Id, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ExpMonth", entity.ExpMonth, DbType.Int16));
                paramCollection.Add(new DBParameter("ExpYear", entity.ExpYear, DbType.Int16));
                paramCollection.Add(new DBParameter("RecieptTotal", entity.RecieptTotal, DbType.Double));
                paramCollection.Add(new DBParameter("ExpTotal", entity.ExpTotal, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                //paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));
                dbHelper.ExecuteNonQuery(TrainingQueries.InsUpdMonthlyExpenditureTotal,
                    paramCollection, trans, CommandType.StoredProcedure);

                isSuccess = true;

            }).IfNotNull(ex =>
            {
                isSuccess = false;
                throw (ex);
            });
            return isSuccess;
        }

        public IEnumerable<MonthlyExpenditureDtEntity> GetExpenditureByMonth(int expMonth, int expYear)
        {
            List<MonthlyExpenditureDtEntity> obj;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ExpMonth", expMonth, DbType.Int32));
                paramCollection.Add(new DBParameter("ExpYear", expYear, DbType.Int32));

                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetExpenditureMonthWise, paramCollection, CommandType.StoredProcedure);
                obj = dtUnits.AsEnumerable().Select(row => new MonthlyExpenditureDtEntity()
                {
                    Id = row.Field<int>("Id"),
                    DtlId = row.Field<int>("DtlId"),
                    ExpTotal = row.Field<double>("ExpTotal"),
                    RecieptTotal = row.Field<double>("RecieptTotal"),
                    ExpDate = row.Field<string>("ExpDate"),
                    BudgetId = row.Field<int>("BudgetId"),
                    BudgetHeads = row.Field<string>("BudgetHeads"),
                    Reciepts = row.Field<string>("Reciepts"),
                    ReieptsAmt = row.Field<double>("ReieptsAmt"),
                    ExpenseAmt = row.Field<double>("ExpenseAmt"),//dbsp_TC_GetMonthWiseExpenditureDtl
                    strExpDate = row.Field<string>("strExpDate")
                }).ToList();
          
            }
            return obj;

        }

        public IEnumerable<MonthlyExpenditureDtEntity> GetActualExpence(int expMonth, int expYear, string budgetHead)
        {
            List<MonthlyExpenditureDtEntity> obj;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ExpMonth", expMonth, DbType.Int32));
                paramCollection.Add(new DBParameter("ExpYear", expYear, DbType.Int32));

                DataTable dtUnits = dbHelper.ExecuteDataTable(TrainingQueries.GetActualExpence, paramCollection, CommandType.StoredProcedure);
                obj = dtUnits.AsEnumerable().Select(row => new MonthlyExpenditureDtEntity()
                {

                    ExpenseAmt = row.Field<double>("ExpenseAmt"),//dbsp_TC_GetActualExpence
                    BudgetHeads = row.Field<string>("BudgetHeads")
                }).ToList();
            }
            return obj;
        }

    }
}
