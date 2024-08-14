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
    public class CashWithdrawalRepository : ICashWithdrawalRepository
    {

        public int CreateCashWithdrawal(CashWithdrawalEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {

                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("WithdrawalID", entity.WithdrawalID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ByCash", entity.ByCash, DbType.Boolean));
                paramCollection.Add(new DBParameter("WithdrawalDate", entity.Date, DbType.DateTime));
                paramCollection.Add(new DBParameter("PayeeName", entity.PayeeName, DbType.String));
                paramCollection.Add(new DBParameter("ChequeNumber", entity.ChequeNumber, DbType.String));
                paramCollection.Add(new DBParameter("ChequeBank", entity.ChequeBank, DbType.String));
                paramCollection.Add(new DBParameter("UpadtedBy", entity.CreatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("CreatedBy", entity.CreatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("CreatedDate", entity.CreatedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedDate", entity.CreatedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));

                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(BranchQueries.InsCashWithDrawal, paramCollection, CommandType.StoredProcedure, "WithdrawalID");
            }
            return iResult;
        }
    }
}
