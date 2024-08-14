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
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Asset.Classes
{
    public class TechnicianEntryRepository : ITechnicianEntryRepository
    {
        public int CreateTechnicianEntry(InHouseEntity MainEntity, TechnicianEntryEntity Entity, DBHelper dbhelper)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", Entity.Id, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("FromDate", Entity.FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", Entity.ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("InHouseId", MainEntity.InHouseId, DbType.Int32));
                paramCollection.Add(new DBParameter("WorkedDate", Entity.WorkedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Remark", Entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", MainEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", MainEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", MainEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", MainEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", MainEntity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(AssetQueries.InsUpdTechnicianEntry, paramCollection, CommandType.StoredProcedure, "Id");
            }
            return iResult;
        }

        public bool UpdateTechnicianEntry(InHouseEntity MainEntity, TechnicianEntryEntity Entity, DBHelper dbhelper)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", Entity.Id, DbType.Int32));
                paramCollection.Add(new DBParameter("FromDate", Entity.FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", Entity.ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("InHouseId", MainEntity.InHouseId, DbType.Int32));
                paramCollection.Add(new DBParameter("WorkedDate", Entity.WorkedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Remark", Entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", MainEntity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", MainEntity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", MainEntity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", MainEntity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", MainEntity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(AssetQueries.InsUpdTechnicianEntry, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public TechnicianEntryEntity GetTechnicianEntry(int InHouseId)
        {
            TechnicianEntryEntity type = new TechnicianEntryEntity();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("InHouseId", InHouseId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetTechnicianEntryDetail, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new TechnicianEntryEntity
                            {
                                Id = row.Field<int>("Id"),
                                TimeSpent = row.Field<string>("TimeSpent"),
                                strFromDate = row.Field<DateTime?>("FromTime").TimeFormat(),
                                strToDate = row.Field<DateTime?>("ToTime").TimeFormat(),
                                InHouseId = row.Field<int>("InHouseId"),
                                strWorkedDate = row.Field<DateTime?>("WorkedDate").DateTimeFormat1(),
                                Remark = row.Field<string>("Remark")
                            }).FirstOrDefault();
            }
            return type;
        }
    }
}
