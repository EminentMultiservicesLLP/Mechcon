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

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class AssetDeletionRequestRepository : IAssetDeletionRequestRepository
    {
        public DeactivationDetailEntity CreateAssetDeletionRequest(DeactivationDetailEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", Entity.Id, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", Entity.Code, DbType.String));
                paramCollection.Add(new DBParameter("AssetId", Entity.AssetId, DbType.Int32));
                paramCollection.Add(new DBParameter("RequestId", Entity.RequestId, DbType.Int32));
                paramCollection.Add(new DBParameter("DeactiveReason", Entity.DeactiveReason, DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierId", Entity.SupplierId, DbType.Int32));
                paramCollection.Add(new DBParameter("Receiver", Entity.Receiver, DbType.String));
                paramCollection.Add(new DBParameter("Address", Entity.Address, DbType.String));
                paramCollection.Add(new DBParameter("ContactPerson", Entity.ContactPerson, DbType.String));
                paramCollection.Add(new DBParameter("ContactNo", Entity.ContactNo, DbType.String));
                paramCollection.Add(new DBParameter("Amount", Entity.Amount, DbType.Double));
                paramCollection.Add(new DBParameter("LogDate", Entity.LogDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("NewSupplier", Entity.NewSupplier, DbType.String));
                paramCollection.Add(new DBParameter("ApprovedBy", Entity.ApprovedBy, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsAssetDeletionRequest, paramCollection, CommandType.StoredProcedure, "Id");
            }
            return Entity;
        }

        public IEnumerable<DeactivationDetailEntity> GetAssetdeactivation(int BranchId)
        {
            List<DeactivationDetailEntity> room = new List<DeactivationDetailEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                DataTable dtRoom = dbHelper.ExecuteDataTable(AssetQueries.RptAssetdeactivation, paramCollection, CommandType.StoredProcedure);
                room = dtRoom.AsEnumerable()
                            .Select(row => new DeactivationDetailEntity
                            {
                                Id = row.Field<int>("Id"),
                                AssetId = row.Field<int>("AssetId"),
                                AssetCode = row.Field<string>("AssetCode"),
                                ItemName = row.Field<string>("ItemName"),
                                DeactiveReason = row.Field<int>("DeactiveReason"),
                                ApprovedBy = row.Field<string>("ApprovedBy"),
                                Receiver = row.Field<string>("Receiver"),
                                Amount = row.Field<double>("Amount"),
                                Address = row.Field<string>("Address"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                NewSupplier = row.Field<string>("Supplier"),
                            }).ToList();
            }
            return room;
        }
  
    }
}
