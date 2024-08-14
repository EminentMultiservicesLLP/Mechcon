using BISERPBusinessLayer.Entities.Asset;
using BISERPBusinessLayer.QueryCollection.Asset;
using BISERPBusinessLayer.Repositories.Asset.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class InHouseRepository : IInHouseRepository
    {
        public InHouseEntity CreateInHouseMaintenance(InHouseEntity Entity, DBHelper dbhelper)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", Entity.InHouseId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", Entity.Code, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("RequisitionId", Entity.RequisitionId, DbType.Int32));
                paramCollection.Add(new DBParameter("VendorId", Entity.VendorId, DbType.Int32));
                paramCollection.Add(new DBParameter("TimeSpent", Entity.TimeSpent, DbType.String));
                paramCollection.Add(new DBParameter("CompletedDate", Entity.CompletedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("CompletedTime", Entity.CompletedTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("TransferDate", Entity.TransferDate, DbType.DateTime));                
                paramCollection.Add(new DBParameter("Feedback", Entity.Feedback, DbType.String));
                paramCollection.Add(new DBParameter("TransferReason", Entity.TransferReason, DbType.String));
                paramCollection.Add(new DBParameter("IsCompleted", Entity.IsCompleted, DbType.Boolean));
                paramCollection.Add(new DBParameter("IsTransfer", Entity.IsTransfer, DbType.Boolean));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(AssetQueries.InsUpdInHouseMaintenance, paramCollection,  CommandType.StoredProcedure);
                Entity.InHouseId = Convert.ToInt32(parameterList["Id"].ToString());
                Entity.Code = parameterList["Code"].ToString();
            }
            return Entity;
        }

        public bool UpdateInHouseMaintenance(InHouseEntity Entity, DBHelper dbhelper)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", Entity.InHouseId, DbType.Int32));
                paramCollection.Add(new DBParameter("Code", Entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("RequisitionId", Entity.RequisitionId, DbType.Int32));
                paramCollection.Add(new DBParameter("VendorId", Entity.VendorId, DbType.Int32));
                paramCollection.Add(new DBParameter("TimeSpent", Entity.TimeSpent, DbType.String));
                paramCollection.Add(new DBParameter("CompletedDate", Entity.CompletedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("CompletedTime", Entity.CompletedTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("TransferDate", Entity.TransferDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Feedback", Entity.Feedback, DbType.String));
                paramCollection.Add(new DBParameter("TransferReason", Entity.TransferReason, DbType.String));
                paramCollection.Add(new DBParameter("IsCompleted", Entity.IsCompleted, DbType.Boolean));
                paramCollection.Add(new DBParameter("IsTransfer", Entity.IsTransfer, DbType.Boolean));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdInHouseMaintenance, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }        
    }
}
