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
    public class OutsideMaintenanceRepository : IOutsideMaintenanceRepository
    {
        public OutsideMaintenanceEntity CreateOutsideMaintenance(OutsideMaintenanceEntity Entity, DBHelper dbhelper)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("Id", Entity.Id, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("Code", Entity.Code, DbType.String, 50, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("RequisitionId", Entity.RequisitionId, DbType.Int32));
                paramCollection.Add(new DBParameter("VendorId", Entity.VendorId, DbType.Int32));
                paramCollection.Add(new DBParameter("MaterialCost", Entity.MaterialCost, DbType.Double));
                paramCollection.Add(new DBParameter("ManPowerCost", Entity.ManPowerCost, DbType.Double));
                paramCollection.Add(new DBParameter("ExternalOrderCost", Entity.ExternalOrderCost, DbType.Double));
                paramCollection.Add(new DBParameter("ScheduledFromDate", Entity.ScheduledFromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ScheduledToDate", Entity.ScheduledToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ApprovedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("Remark", Entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("JobDescription", Entity.JobDescription, DbType.String));
                paramCollection.Add(new DBParameter("InsuranceClaim", Entity.InsuranceClaim, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                var parameterList = dbHelper.ExecuteNonQueryForOutParameter(AssetQueries.InsOutsideMaintenance, paramCollection,  CommandType.StoredProcedure);
                Entity.Id = Convert.ToInt32(parameterList["Id"].ToString());
                Entity.Code = parameterList["Code"].ToString();                
            }
            return Entity;
        }
       
        public OutsideMaintenanceEntity GetOutsideMaintenanceNodt(int MaintenanceId)
        {
            OutsideMaintenanceEntity type = new OutsideMaintenanceEntity();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("MaintenanceId", MaintenanceId, DbType.Int32));
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetOutsideMaintenancedt, paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new OutsideMaintenanceEntity
                            {
                                Id = row.Field<int>("Id"),
                                MaterialCost = row.Field<double>("MaterialCost"),
                                ManPowerCost = row.Field<double>("ManPowerCost"),
                                ExternalOrderCost = row.Field<double>("ExternalOrderCost"),
                                ScheduledFromDate = row.Field<DateTime>("ScheduledFromDate"),
                                strScheduledFromDate = Convert.ToDateTime(row.Field<DateTime>("ScheduledFromDate")).ToString("dd-MMMM-yyyy"),
                                ScheduledToDate = row.Field<DateTime>("ScheduledToDate"),
                                strScheduledToDate = Convert.ToDateTime(row.Field<DateTime>("ScheduledToDate")).ToString("dd-MMMM-yyyy"),
                                JobDescription = row.Field<string>("JobDescription"),
                                InsuranceClaim = row.Field<string>("InsuranceClaim"),
                                Remark = row.Field<string>("Remark"),
                            }).FirstOrDefault();
            }
            return type;
        }

        public IEnumerable<SparePartsOuthouseUtilEntity> GetOutsideMaintenanceNodtItem(int OutHouseId)
        {
            List<SparePartsOuthouseUtilEntity> type = new List<SparePartsOuthouseUtilEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("OutHouseId", OutHouseId, DbType.Int32));
                DataTable dtType = dbHelper.ExecuteDataTable(AssetQueries.GetOutsideMaintenancedtitem, paramCollection, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new SparePartsOuthouseUtilEntity
                            {
                                //ITEMID = row.Field<int>("ITEMID"),
                                Qty = row.Field<double>("QTY"),
                                ItemName = row.Field<string>("ItemName"),
                                Cost = row.Field<double>("Cost"),
                            }).ToList();
            }
            return type;
        }
    }
}
