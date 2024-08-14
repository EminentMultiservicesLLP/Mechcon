using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class DiscrepancyDtRepository : IDiscrepancyDtRepository
    {

        public int CreateDiscrepancyDt(DiscrepancyEntity mientity, DiscrepancyDtEntity entity,DBHelper dbHelper )
        {
            int iResult = 0;
            
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("DiscrepancyDetailId", entity.DiscrepancyDetailId, DbType.Int32,
                    ParameterDirection.Output));
                paramCollection.Add(new DBParameter("DiscrepancyId", mientity.DiscrepancyId, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemID", entity.ItemID, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchID", entity.BatchID, DbType.Int32));
                paramCollection.Add(new DBParameter("Quantity", entity.Quantity, DbType.Double));
                paramCollection.Add(new DBParameter("PhysicalQty", entity.PhysicalQty, DbType.Double));
                paramCollection.Add(new DBParameter("ShortQty", entity.ShortQuantity, DbType.Double));
                paramCollection.Add(new DBParameter("SurplusQty", entity.SurPlusQuantity, DbType.Double));
                paramCollection.Add(new DBParameter("Reason", entity.Reason, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", mientity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", mientity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", mientity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", mientity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", mientity.InsertedIPAddress, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertDiscrepancyDt, paramCollection,CommandType.StoredProcedure, "DiscrepancyDetailId");
            }
            return iResult;
        }

        public IEnumerable<DiscrepancyDtEntity> GetDetailByDiscrepancyId(int DiscrepancyId)
        {
            List<DiscrepancyDtEntity> discrepancy = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("DiscrepancyId", DiscrepancyId, DbType.Int32);
                DataTable dtDiscrepancy = dbHelper.ExecuteDataTable(StoreQuery.GetDiscrepancyDtlsById, param, CommandType.StoredProcedure);
                discrepancy = dtDiscrepancy.AsEnumerable()
                            .Select(row => new DiscrepancyDtEntity
                            {
                                DiscrepancyDetailId = row.Field<int>("DiscrepancyDetailId"),
                                DiscrepancyId = row.Field<int>("DiscrepancyId"),
                                ItemID = row.Field<int>("ItemId"),
                                BatchID = row.Field<int>("BatchId"),
                                ItemName = row.Field<string>("ItemName"),
                                BatchName = row.Field<string>("BatchName"),
                                Quantity = row.Field<double>("Quantity"),
                                PhysicalQty = row.Field<double?>("PhysicalQty"),
                                ShortQuantity = row.Field<double?>("ShortQty"),
                                SurPlusQuantity = row.Field<double?>("SurplusQty"),
                                Reason = row.Field<string>("Reason")
                            }).ToList();
            }
            return discrepancy;
        }

        public bool UpdateDiscrepancyAuthQty(DiscrepancyEntity mientity, DiscrepancyDtEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
           
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("DiscrepancyDetailId", entity.DiscrepancyDetailId, DbType.Int32));
                paramCollection.Add(new DBParameter("DiscrepancyId", mientity.DiscrepancyId, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreId", mientity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("BatchId", entity.BatchID, DbType.Int32));
                paramCollection.Add(new DBParameter("ItemId", entity.ItemID, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthorisedBy", mientity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthorisedOn", mientity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("Quantity", entity.Quantity, DbType.Double));
                paramCollection.Add(new DBParameter("InsertedBy", mientity.InsertedBy, DbType.Int32));
               // paramCollection.Add(new DBParameter("InsertedON", mientity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", mientity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", mientity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", mientity.InsertedIPAddress, DbType.String));

                if (entity.Authorized == true)
                {
                    paramCollection.Add(new DBParameter("Authorized", 1, DbType.Boolean));
                }
                else
                {
                    paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
                }
                iResult = dbHelper.ExecuteNonQuery(StoreQuery.UpdDiscrepancyAuthQty, paramCollection,
                    CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
