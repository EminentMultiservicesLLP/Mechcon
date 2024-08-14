using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.QueryCollection.Branch;
using BISERPBusinessLayer.Repositories.Common.Interface;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Common;
using BISERPBusinessLayer.QueryCollection.Asset;

namespace BISERPBusinessLayer.Repositories.Common.Class
{
    public class PettyCashRepository : IPettyCashRepository
    {
        public int CreatePettyCashTransaction(Entities.Common.PettyCashEntity entity)
        {
            int iResult = 0;
            string StransactionDate = "";
            string[] arrStransactionDate;
            if (entity.TransactionDate == null)
            {
                StransactionDate = DateTime.Now.AddYears(1).ToString("dd-MMM-yyyy");
            }
            else
            {
                arrStransactionDate = entity.TransactionDate.Split('/');
                StransactionDate = arrStransactionDate[1].ToString() + "-" + arrStransactionDate[0].ToString() + "-" + arrStransactionDate[2].ToString();
            }
            DateTime dtranctionDate = Convert.ToDateTime(StransactionDate);
            TryCatch.Run(() =>
            {
                using (DBHelper dbHelper = new DBHelper())
                {
                    if (entity.IsNotNull())
                    {
                        DBParameterCollection paramCollection = new DBParameterCollection();
                        if (entity.IsDeposite)
                        {

                            paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                            paramCollection.Add(new DBParameter("DepositeID", entity.TransactionId, DbType.Int32,
                                ParameterDirection.Output));
                            paramCollection.Add(new DBParameter("ByCash", entity.IsCashTransaction, DbType.Boolean));
                            paramCollection.Add(new DBParameter("DepositeDate", dtranctionDate, DbType.DateTime));
                            paramCollection.Add(new DBParameter("PayeeName", entity.PayeeName, DbType.String));
                            paramCollection.Add(new DBParameter("ChequeNumber", entity.ChequeNumber, DbType.String));
                            paramCollection.Add(new DBParameter("ChequeBank", entity.ChequeBank, DbType.String));
                            paramCollection.Add(new DBParameter("CreatedBy", entity.CreatedBy, DbType.Int32));
                            paramCollection.Add(new DBParameter("CreatedDate", entity.CreatedDate, DbType.DateTime));
                            paramCollection.Add(new DBParameter("Amount", entity.TransactionAmount, DbType.Double));
                            paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));

                            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(BranchQueries.InsCashDeposite,
                                paramCollection, CommandType.StoredProcedure, "DepositeID");

                        }
                        else
                        {
                            paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                            paramCollection.Add(new DBParameter("WithdrawalID", entity.TransactionId, DbType.Int32,
                                ParameterDirection.Output));
                            paramCollection.Add(new DBParameter("ByCash", entity.IsCashTransaction, DbType.Boolean));
                            paramCollection.Add(new DBParameter("WithdrawalDate", dtranctionDate, DbType.DateTime));
                            paramCollection.Add(new DBParameter("PayeeName", entity.PayeeName, DbType.String));
                            paramCollection.Add(new DBParameter("ChequeNumber", entity.ChequeNumber, DbType.String));
                            paramCollection.Add(new DBParameter("ChequeBank", entity.ChequeBank, DbType.String));
                            paramCollection.Add(new DBParameter("UpadtedBy", entity.CreatedBy, DbType.Int32));
                            paramCollection.Add(new DBParameter("CreatedBy", entity.CreatedBy, DbType.Int32));
                            paramCollection.Add(new DBParameter("CreatedDate", entity.CreatedDate, DbType.DateTime));
                            paramCollection.Add(new DBParameter("UpdatedDate", entity.CreatedDate, DbType.DateTime));
                            paramCollection.Add(new DBParameter("Amount", entity.TransactionAmount, DbType.Double));
                            paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));

                            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(BranchQueries.InsCashWithDrawal,
                                paramCollection, CommandType.StoredProcedure, "WithdrawalID");
                        }
                    }
                }
            });
            return iResult;
        }

        public IEnumerable<PettyCashEntity> GetPettyCashDEPOSITE(string FromDate, string ToDate, int Type)
        {
            List<PettyCashEntity> PettyCash = new List<PettyCashEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FromDate", FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Type", Type, DbType.Int32));
                DataTable dtLocation = dbHelper.ExecuteDataTable(BranchQueries.GetPettyCash, paramCollection, CommandType.StoredProcedure);
                PettyCash = dtLocation.AsEnumerable()
                            .Select(row => new PettyCashEntity
                            {
                                BranchId = row.Field<int>("BranchId"),
                                PayeeName = row.Field<string>("PayeeName"),
                                TransactionId = row.Field<int>("TransactionId"),
                                IsCashTransaction = row.Field<Boolean>("ByCash"),
                                ChequeNumber = row.Field<string>("ChequeNumber"),
                                ChequeBank = row.Field<string>("ChequeBank"),
                                TransactionAmount = row.Field<double?>("Amount"),
                                Remarks = row.Field<string>("Remarks"),
                                TransactionDate = row.Field<string>("TransactionDate"),
                                strTransactionDate = row.Field<string>("strTransactionDate"),
                                IsDeposite =  true,
                            }).ToList();
            }
            return PettyCash;
        }

        public IEnumerable<PettyCashEntity> GetPettyCashWITHDRAWAL( string FromDate, string ToDate,int Type)
        {
            List<PettyCashEntity> PettyCash = new List<PettyCashEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FromDate", FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Type", Type, DbType.Int32));
                DataTable dtLocation = dbHelper.ExecuteDataTable(BranchQueries.GetPettyCash, paramCollection, CommandType.StoredProcedure);
                PettyCash = dtLocation.AsEnumerable()
                            .Select(row => new PettyCashEntity
                            {
                                BranchId = row.Field<int>("BranchId"),
                                TransactionId = row.Field<int>("TransactionId"),
                                IsCashTransaction = row.Field<Boolean>("ByCash"),
                                ChequeNumber = row.Field<string>("ChequeNumber"),
                                ChequeBank = row.Field<string>("ChequeBank"),
                                TransactionAmount = row.Field<Double?>("Amount"),
                                Remarks = row.Field<string>("Remarks"),
                                PayeeName = row.Field<string>("PayeeName"),
                                TransactionDate = row.Field<string>("TransactionDate"),
                                 strTransactionDate = row.Field<string>("strTransactionDate"),
                                IsDeposite = false,
                            }).ToList();
            }
            return PettyCash;
        }

        public bool UpdatePettyCash(PettyCashEntity entity)
        {
            int iResult = 0;
            string StransactionDate = "";
            string[] arrStransactionDate;
            if (entity.TransactionDate == null)
            {
                StransactionDate = DateTime.Now.AddYears(1).ToString("dd-MMM-yyyy");
            }
            else
            {
                arrStransactionDate = entity.TransactionDate.Split('/');
                StransactionDate = arrStransactionDate[1].ToString() + "-" + arrStransactionDate[0].ToString() + "-" + arrStransactionDate[2].ToString();
            }
            DateTime dtranctionDate = Convert.ToDateTime(StransactionDate);
            using (DBHelper dbHelper = new DBHelper())
            {
                if (entity.IsNotNull())
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    if (entity.IsDeposite)
                    {
                        paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                        paramCollection.Add(new DBParameter("PayeeName", entity.PayeeName, DbType.String));
                        paramCollection.Add(new DBParameter("DepositeID", entity.TransactionId, DbType.Int32));
                        paramCollection.Add(new DBParameter("ByCash", entity.IsCashTransaction, DbType.Boolean));
                        paramCollection.Add(new DBParameter("DepositeDate", dtranctionDate, DbType.String));
                        paramCollection.Add(new DBParameter("ChequeNumber", entity.ChequeNumber, DbType.String));
                        paramCollection.Add(new DBParameter("ChequeBank", entity.ChequeBank, DbType.String));
                        paramCollection.Add(new DBParameter("CreatedBy", entity.CreatedBy, DbType.Int32));
                        paramCollection.Add(new DBParameter("CreatedDate", entity.CreatedDate, DbType.DateTime));
                        paramCollection.Add(new DBParameter("Amount", entity.TransactionAmount, DbType.Double));
                        paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                        iResult = dbHelper.ExecuteNonQuery(BranchQueries.UpdPettyCashDeposite, paramCollection, CommandType.StoredProcedure);
                    }
                    else
                    {
                        paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                        paramCollection.Add(new DBParameter("WithdrawalID", entity.TransactionId, DbType.Int32));
                        paramCollection.Add(new DBParameter("ByCash", entity.IsCashTransaction, DbType.Boolean));
                        paramCollection.Add(new DBParameter("WithdrawalDate", dtranctionDate, DbType.String));
                        paramCollection.Add(new DBParameter("PayeeName", entity.PayeeName, DbType.String));
                        paramCollection.Add(new DBParameter("ChequeNumber", entity.ChequeNumber, DbType.String));
                        paramCollection.Add(new DBParameter("ChequeBank", entity.ChequeBank, DbType.String));
                        paramCollection.Add(new DBParameter("UpadtedBy", entity.CreatedBy, DbType.Int32));
                        paramCollection.Add(new DBParameter("CreatedBy", entity.CreatedBy, DbType.Int32));
                        paramCollection.Add(new DBParameter("CreatedDate", entity.CreatedDate, DbType.DateTime));
                        paramCollection.Add(new DBParameter("UpdatedDate", entity.CreatedDate, DbType.DateTime));
                        paramCollection.Add(new DBParameter("Amount", entity.TransactionAmount, DbType.Double));
                        paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));

                        iResult = dbHelper.ExecuteNonQuery(BranchQueries.UpdPettyCashWITHDRAWAL, paramCollection, CommandType.StoredProcedure);
                    }
                }
            }

            if (iResult > 0)
                return true;
            else
                return false;
        }

       
    }
}
