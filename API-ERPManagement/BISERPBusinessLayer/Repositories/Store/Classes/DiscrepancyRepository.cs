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
    public class DiscrepancyRepository : IDiscrepancyRepository
    {
        public DiscrepancyEntity CreateNewEntry(DiscrepancyEntity entity, DBHelper dbHelper)
        {
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("DiscrepancyId", entity.DiscrepancyId, DbType.Int32,
                    ParameterDirection.Output));
                paramCollection.Add(new DBParameter("DiscrepancyNo", entity.DiscrepancyNo, DbType.String, 50,
                    ParameterDirection.Output));
                paramCollection.Add(new DBParameter("DiscrepancyDate", entity.DiscrepancyDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(StoreQuery.InsertDiscrepancy,paramCollection, CommandType.StoredProcedure);
                entity.DiscrepancyId = Convert.ToInt32(parameterList["DiscrepancyId"].ToString());
                entity.DiscrepancyNo = parameterList["DiscrepancyNo"].ToString();
            }
            return entity;
        }

        public IEnumerable<DiscrepancyEntity> GetAllDiscrepancy(int UserId)
        {
            List<DiscrepancyEntity> discrepancy = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtdiscrepancy = dbHelper.ExecuteDataTable(StoreQuery.GetAllDiscrepancy, param, CommandType.StoredProcedure);
                discrepancy = dtdiscrepancy.AsEnumerable()
                            .Select(row => new DiscrepancyEntity
                            {
                                DiscrepancyId = row.Field<int>("DiscrepancyId"),
                                DiscrepancyNo = row.Field<string>("DiscrepancyNo"),
                                DiscrepancyDate = row.Field<DateTime>("DiscrepancyDate"),
                                strDiscrepancyDate = Convert.ToDateTime(row.Field<DateTime>("DiscrepancyDate")).ToString("dd-MMM-yyyy"),
                                StoreName = row.Field<string>("StoreName"),
                                StoreId = row.Field<int?>("StoreId"),
                                Remarks = row.Field<string>("Remarks")
                            }).ToList();
            }
            return discrepancy;
        }
    }
}
