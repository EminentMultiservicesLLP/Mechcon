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
    public class MaterialReturnGuardRepository : IMaterialReturnGuardRepository
    {
        public MaterialReturnGuardEntity CreateEntity(MaterialReturnGuardEntity entity)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ReturnId", entity.ReturnId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ReturnNo", entity.ReturnNo, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ReturnDate", entity.ReturnDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("BranchID", entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("EmpId", entity.EmpId, DbType.Int32));
                paramCollection.Add(new DBParameter("NetAmount", entity.NetAmount, DbType.Double));
                paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
                paramCollection.Add(new DBParameter("GrossAmount", entity.GrossAmount, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(BranchQueries.InsertMaterialReturnGuard, paramCollection,  CommandType.StoredProcedure);
                entity.ReturnId = Convert.ToInt32(parameterList["ReturnId"].ToString());
                entity.ReturnNo = parameterList["ReturnNo"].ToString();
            }
            return entity;
        }
    }
}
