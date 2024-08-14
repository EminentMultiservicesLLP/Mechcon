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
    public class SubDivisionRepository : ISubDivisionRepository
    {
        public IEnumerable<SubDivisionMasterEntity> GetAllSubDivision()
        {
            List<SubDivisionMasterEntity> SubDivision = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.TRA_MST_GetAllSubDivision, CommandType.StoredProcedure);
                SubDivision = dtUnits.AsEnumerable()
                            .Select(row => new SubDivisionMasterEntity
                            {
                                SubDivisionId = row.Field<int>("SubDivisionId"),
                                SubDivisionName = row.Field<string>("SubDivisionName"),
                                DivisionId = row.Field<int>("DivisionId"),
                                Deactive = row.Field<Boolean>("Deactive")
                            }).ToList();
                if (SubDivision == null || SubDivision.Count == 0)
                    SubDivision.Add(new SubDivisionMasterEntity
                    {
                        SubDivisionId = 0,
                        DivisionId = 0,
                        SubDivisionName = ""
                    });
            }
            return SubDivision;
        }
        public int CreateSubDivision(SubDivisionMasterEntity Entity)
       {
           int iResult = 0;
           using (DBHelper dbHelper = new DBHelper())
           {
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("SubDivisionId", Entity.SubDivisionId, DbType.Int32, ParameterDirection.Output));
               paramCollection.Add(new DBParameter("SubDivisionName", Entity.SubDivisionName, DbType.String));
               paramCollection.Add(new DBParameter("DivisionId", Entity.DivisionId, DbType.Int32));
               paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
               paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
               paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
               paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
               paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
               paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));

               iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsUpd_TRA_MST_SubDivision, paramCollection, CommandType.StoredProcedure, "SubDivisionId");
           }
           return iResult;
       }
        public bool UpdateSubDivision(SubDivisionMasterEntity Entity)
       {
           int iResult = 0;
           using (DBHelper dbHelper = new DBHelper())
           {
               DBParameterCollection paramCollection = new DBParameterCollection();
               paramCollection.Add(new DBParameter("SubDivisionId", Entity.SubDivisionId, DbType.Int32));
               paramCollection.Add(new DBParameter("SubDivisionName", Entity.SubDivisionName, DbType.String));
               paramCollection.Add(new DBParameter("DivisionId", Entity.DivisionId, DbType.Int32));
               paramCollection.Add(new DBParameter("UpdatedBy", Entity.UpdatedBy, DbType.Int32));
               paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
               paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
               paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
               paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
               paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
               iResult = dbHelper.ExecuteNonQuery(TransportQuery.InsUpd_TRA_MST_SubDivision, paramCollection, CommandType.StoredProcedure);
           }
           if (iResult > 0)
               return true;
           else
               return false;
       }

        public IEnumerable<SubDivisionMasterEntity> GetActiveSubDivision(int UserId)
        {
            List<SubDivisionMasterEntity> division = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                DataTable dtManufacturer = dbHelper.ExecuteDataTable(TransportQuery.GetActiveSubDivision, paramCollection, CommandType.StoredProcedure);
                division = dtManufacturer.AsEnumerable()
                            .Select(row => new SubDivisionMasterEntity
                            {
                                DivisionId = row.Field<int>("DivisionId"),
                                SubDivisionId = row.Field<int>("SubDivisionId"),
                                SubDivisionName = row.Field<string>("SubDivisionName"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
                if (division == null || division.Count == 0)
                    division.Add(new SubDivisionMasterEntity
                    {
                        DivisionId = 0,
                        SubDivisionName = "",
                        Deactive = false
                    });
            }
            return division;
        }
    }
}
