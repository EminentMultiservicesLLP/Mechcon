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
    public class CashDepositeRepository : ICashDepositeRepository
    {
        public int CreateCashDeposite(CashDepositeEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
             
                DBParameterCollection paramCollection = new DBParameterCollection();
                 paramCollection.Add(new DBParameter("BranchId", entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("DepositeID", entity.DepositeID, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ByCash", entity.ByCash, DbType.Boolean));
                paramCollection.Add(new DBParameter("DepositeDate", entity.Date, DbType.DateTime));
                paramCollection.Add(new DBParameter("ChequeNumber", entity.ChequeNumber, DbType.String));
                paramCollection.Add(new DBParameter("ChequeBank", entity.ChequeBank, DbType.String));
                paramCollection.Add(new DBParameter("CreatedBy", entity.CreatedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("CreatedDate", entity.CreatedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(BranchQueries.InsCashDeposite, paramCollection, CommandType.StoredProcedure, "DepositeID");
            }
            return iResult;
        }
    }
}
