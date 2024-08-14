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
    public class FuelDetailsRepository : IFuelDetailsRepository
    {

        public IEnumerable<FuelDetailsEntity> GetAllFuelDetails(int ScheduleId)
        {
            List<FuelDetailsEntity> schedule = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ScheduleId", ScheduleId, DbType.Int32);
                DataTable dtschedule = dbHelper.ExecuteDataTable(TransportQuery.GetFuelDetail, CommandType.StoredProcedure);
                schedule = dtschedule.AsEnumerable()
                            .Select(row => new FuelDetailsEntity
                            {
                                FuelDetailId = row.Field<int>("FuelDetailId"),
                                ScheduleId = row.Field<int>("ScheduleId"),
                                FuelType = row.Field<int>("FuelType"),
                                FuelPump = row.Field<int>("FuelPump"),
                                FuelPrice = row.Field<double>("FuelPrice"),
                                FuelQuantity = row.Field<double>("FuelQuantity"),
                                FuelAmount = row.Field<double>("FuelAmount"),
                                Paymode = row.Field<int>("Paymode"),
                                PayCardNo = row.Field<string>("PayCardNo"),
                                OtherExpense = row.Field<double>("OtherExpense"),
                                EndReading = row.Field<double>("EndReading"),
                                BalanceAmount = row.Field<double>("BalanceAmount"),
                                CompletedDate = row.Field<DateTime>("CompletedDate"),
                                Completed = row.Field<bool>("Completed")
                            }).ToList();
            }
            return schedule;
        }

        public IEnumerable<FuelDetailsEntity> GetAllFuelSchedule(int ScheduleId)
        {
            List<FuelDetailsEntity> schedule = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ScheduleId", ScheduleId, DbType.Int32);
                DataTable dtschedule = dbHelper.ExecuteDataTable(TransportQuery.GetAllSchedulefuel,param, CommandType.StoredProcedure);
                schedule = dtschedule.AsEnumerable()
                            .Select(row => new FuelDetailsEntity
                            {
                                FuelDetailId = row.Field<int>("FuelDetailId"),
                                ScheduleId = row.Field<int>("ScheduleId"),
                                FuelType = row.Field<int>("FuelType"),
                                FuelPump = row.Field<int>("FuelPump"),
                                FuelPrice = row.Field<double>("FuelPrice"),
                                FuelQuantity = row.Field<double>("FuelQuantity"),
                                FuelAmount = row.Field<double>("FuelAmount"),
                                Paymode = row.Field<int>("Paymode"),
                                PayCardNo = row.Field<string>("PayCardNo"),
                                OtherExpense = row.Field<double>("OtherExpense"),
                                EndReading = row.Field<double>("EndReading"),
                                BalanceAmount = row.Field<double>("BalanceAmount"),
                                CompletedDate = row.Field<DateTime?>("CompletedDate"),
                                StrCompletedDate = Convert.ToDateTime(row.Field<DateTime?>("CompletedDate")).ToString("dd-MMMM-yyyy"),
                                Completed = row.Field<bool>("Completed")
                            }).ToList();
            }
            return schedule;
        }

        public int CreateFuelDetails(FuelDetailsEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("FuelDetailId", Entity.FuelDetailId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("ScheduleId", Entity.ScheduleId, DbType.Int32));
                paramCollection.Add(new DBParameter("FuelType", Entity.FuelType, DbType.Int32));
                paramCollection.Add(new DBParameter("FuelPump", Entity.FuelPump, DbType.Int32));
                paramCollection.Add(new DBParameter("FuelPrice", Entity.FuelPrice, DbType.Double));
                paramCollection.Add(new DBParameter("FuelQuantity", Entity.FuelQuantity, DbType.Double));
                paramCollection.Add(new DBParameter("FuelAmount", Entity.FuelAmount, DbType.Double));
                paramCollection.Add(new DBParameter("Paymode", Entity.Paymode, DbType.Int32));
                paramCollection.Add(new DBParameter("PayCardNo", Entity.PayCardNo, DbType.String));
                paramCollection.Add(new DBParameter("OtherExpense", Entity.OtherExpense, DbType.Double));
                paramCollection.Add(new DBParameter("EndReading", Entity.EndReading, DbType.Double));
                paramCollection.Add(new DBParameter("BalanceAmount", Entity.BalanceAmount, DbType.Double));
                paramCollection.Add(new DBParameter("Completed", Entity.Completed, DbType.Boolean));
                paramCollection.Add(new DBParameter("CompletedDate", Entity.CompletedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsertFuelDetails, paramCollection, CommandType.StoredProcedure, "FuelDetailId");
            }
            return iResult;
        }

        //public int UpdateFuelDetails(FuelDetailsEntity Entity)
        //{
        //    int iResult = 0;
        //    using (DBHelper dbHelper = new DBHelper())
        //    {
        //        DBParameterCollection paramCollection = new DBParameterCollection();
        //        paramCollection.Add(new DBParameter("FuelDetailId", Entity.FuelDetailId, DbType.Int32));
        //        paramCollection.Add(new DBParameter("ScheduleId", Entity.ScheduleId, DbType.Int32));
        //        paramCollection.Add(new DBParameter("FuelType", Entity.FuelType, DbType.Int32));
        //        paramCollection.Add(new DBParameter("FuelPump", Entity.FuelPump, DbType.Int32));
        //        paramCollection.Add(new DBParameter("FuelPrice", Entity.FuelPrice, DbType.Double));
        //        paramCollection.Add(new DBParameter("FuelQuantity", Entity.FuelQuantity, DbType.Double));
        //        paramCollection.Add(new DBParameter("FuelAmount", Entity.FuelAmount, DbType.Double));
        //        paramCollection.Add(new DBParameter("Paymode", Entity.Paymode, DbType.Int32));
        //        paramCollection.Add(new DBParameter("PayCardNo", Entity.PayCardNo, DbType.String));
        //        paramCollection.Add(new DBParameter("OtherExpense", Entity.OtherExpense, DbType.Double));
        //        paramCollection.Add(new DBParameter("EndReading", Entity.EndReading, DbType.Double));
        //        paramCollection.Add(new DBParameter("BalanceAmount", Entity.BalanceAmount, DbType.Double));
        //        paramCollection.Add(new DBParameter("Completed", Entity.Completed, DbType.Boolean));
        //        paramCollection.Add(new DBParameter("CompletedDate", Entity.CompletedDate, DbType.DateTime));
        //        paramCollection.Add(new DBParameter("UpdatedBy", Entity.InsertedBy, DbType.Int32));
        //        paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
        //        paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
        //        paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
        //        paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
        //        iResult = dbHelper.ExecuteNonQuery(TransportQuery.UpdateFuelDetails, paramCollection, CommandType.StoredProcedure);
        //    }
        //    return iResult;
           
        //}
    }
}
