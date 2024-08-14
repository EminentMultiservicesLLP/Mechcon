using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class PODeliveryTerm : IPODeliveryTerm
    {

        public List<PODeliveryTermEntities> GetDetailByPOID(int Id)
        {
            List<PODeliveryTermEntities> podelivery = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("POID", Id, DbType.Int32);
                DataTable dtPODelivery = dbHelper.ExecuteDataTable(PurchaseQueries.GetPODeliveryTermByPOID, param, CommandType.StoredProcedure);

                podelivery = dtPODelivery.AsEnumerable()
                            .Select(row => new PODeliveryTermEntities
                            {
                                DelTermID = row.Field<int>("DelTermID"),
                                DeliveryTermDesc = row.Field<string>("DeliveryTermDesc"),
                                DeliveryTermCode = row.Field<string>("DeliveryTermCode")
                            }).ToList();
            }
            return podelivery;
        }

        public int CreateNewEntry(PurchaseOrderEntities poEntity, PODeliveryTermEntities entity, DBHelper dbHelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("POID", poEntity.ID, DbType.Int32, ParameterDirection.InputOutput));
            paramCollection.Add(new DBParameter("DelTermID", entity.DelTermID, DbType.Int32));
            paramCollection.Add(new DBParameter("EnteredOn", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("EnteredBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", poEntity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", poEntity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", poEntity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", poEntity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", poEntity.InsertedMacID, DbType.String));
            //iResult = dbHelper.ExecuteNonQuery(PurchaseQueries.InsertPODeliveryTerm, paramCollection, CommandType.StoredProcedure);

            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(PurchaseQueries.InsertPODeliveryTerm, paramCollection, CommandType.StoredProcedure, "POID");
            return iResult;
        }
    }
}
