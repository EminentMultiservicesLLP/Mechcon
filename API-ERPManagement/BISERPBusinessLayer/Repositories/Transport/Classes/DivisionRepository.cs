using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.QueryCollection.Transport;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Transport.Classes
{
    public class DivisionRepository : IDivisionRepository
    {
        public IEnumerable<DivisionMaster> GetAllDivision()
        {
            List<DivisionMaster> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.TRA_MST_GetAllDivision, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new DivisionMaster
                            {
                                DivisionId = row.Field<int>("DivisionId"),
                                DivisionName = row.Field<string>("DivisionName"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
                if (units == null || units.Count == 0)
                    units.Add(new DivisionMaster
                    {
                        DivisionId = 0,
                        DivisionName = ""
                    });
            }
            return units;
        }
        public IEnumerable<DivisionMaster> GetActiveDivision(int UserId)
        {
            List<DivisionMaster> division = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(TransportQuery.sp_GetActiveDivision, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new DivisionMaster
                            {
                                DivisionId = row.Field<int>("DivisionId"),
                                DivisionName = row.Field<string>("DivisionName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (division == null || division.Count == 0)
                    division.Add(new DivisionMaster
                    {
                        DivisionId = 0,
                        DivisionName = "",
                        Deactive = false
                    });
            }
            return division;
        }
       public int CreateDivision(DivisionMaster Entity)
       {
           int iResult = 0;
           using (DBHelper dbHelper = new DBHelper())
           {
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("DivisionId", Entity.DivisionId, DbType.Int32, ParameterDirection.Output));
               paramCollection.Add(new DBParameter("DivisionName", Entity.DivisionName, DbType.String));
               paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
               paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
               paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
               paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
               paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
               paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));

               iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsUpd_TRA_MST_Division, paramCollection, CommandType.StoredProcedure, "DivisionId");
           }
           return iResult;
       }
       public bool UpdateDivision(DivisionMaster Entity)
       {
           int iResult = 0;
           using (DBHelper dbHelper = new DBHelper())
           {
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("DivisionId", Entity.DivisionId, DbType.Int32));
               paramCollection.Add(new DBParameter("DivisionName", Entity.DivisionName, DbType.String));
               paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
               paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
               paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
               paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
               paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
               paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
               iResult = dbHelper.ExecuteNonQuery(TransportQuery.InsUpd_TRA_MST_Division, paramCollection, CommandType.StoredProcedure);
           }
           if (iResult > 0)
               return true;
           else
               return false;
       }
    }
}
