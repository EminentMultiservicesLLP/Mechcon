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
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Transport.Classes
{
    public class DriverScheduleRepository : IDriverScheduleRepository
    {
        public IEnumerable<DriverScheduleEntity> GetAllDriverSchedule()
        {
            List<DriverScheduleEntity> schedule = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtschedule = dbHelper.ExecuteDataTable(TransportQuery.GetDriverSchedule, CommandType.StoredProcedure);
                schedule = dtschedule.AsEnumerable()
                            .Select(row => new DriverScheduleEntity
                            {
                                ScheduleId = row.Field<int>("GreenTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                DriverId = row.Field<int?>("DriverId"),
                                FromPlace = row.Field<int?>("FromPlace"),
                                ToPlace = row.Field<string>("ToPlace"),
                                FromDate = row.Field<DateTime>("FromDate"),
                                StartReading = row.Field<double>("StartReading"),
                                Advance = row.Field<double>("Advance"),
                                Remark = row.Field<string>("Remark")

                            }).ToList();
            }
            return schedule;
        }
        public IEnumerable<DriverScheduleEntity> GetAllSchedule()
        {
            List<DriverScheduleEntity> schedule = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtschedule = dbHelper.ExecuteDataTable(TransportQuery.GetAllSchedule, CommandType.StoredProcedure);
                schedule = dtschedule.AsEnumerable()
                            .Select(row => new DriverScheduleEntity
                            {
                                ScheduleId = row.Field<int>("ScheduleId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                DriverId = row.Field<int?>("DriverId"),
                                DriverName = row.Field<string>("EmpName"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                FromDate = row.Field<DateTime>("FromDate"),
                                StrFromDate = Convert.ToDateTime(row.Field<DateTime>("FromDate")).ToString("dd-MMMM-yyyy"),
                                FromPlace = row.Field<int?>("FromPlace"),
                                ToPlace = row.Field<string>("ToPlace"),
                                Advance = row.Field<double>("Advance"),
                                StartReading = row.Field<double>("StartReading"),
                                Remark = row.Field<string>("Remark"),
                                StrToDate = Convert.ToDateTime(row.Field<DateTime?>("ToDate")).ToString("dd-MMMM-yyyy"),
                                StrFromTime = row.Field<DateTime?>("FromTime").TimeFormat(),
                                StrToTime= row.Field<DateTime?>("ToTime").TimeFormat(),
                                ApprovedBy = row.Field<string>("ApprovedBy"),
                                AssignTo = row.Field<string>("AssignTo"),
                                BranchId = row.Field<int>("BranchId"),
                                TypeName = row.Field<string>("TypeName"),
                            }).ToList();
            }
            return schedule;
        }

        public int CreateDriverSchedule(DriverScheduleEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ScheduleId", Entity.ScheduleId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("DriverId", Entity.DriverId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("FromPlace", Entity.FromPlace, DbType.Int32));
                paramCollection.Add(new DBParameter("ToPlace", Entity.ToPlace, DbType.String));
                paramCollection.Add(new DBParameter("AssignTo", Entity.AssignTo, DbType.String));
                paramCollection.Add(new DBParameter("FromDate", Entity.FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("StartReading", Entity.StartReading, DbType.Double));
                paramCollection.Add(new DBParameter("Remark", Entity.Remark, DbType.String));
                paramCollection.Add(new DBParameter("Advance", Entity.Advance, DbType.Double));
                paramCollection.Add(new DBParameter("ToDate", Entity.ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("FromTime", Entity.FromTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToTime", Entity.ToTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("ApprovedBy", Entity.ApprovedBy, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsertDriverSchedule, paramCollection, CommandType.StoredProcedure, "ScheduleId");
            }
            return iResult;
        }
        public IEnumerable<DriverScheduleEntity> GetAllScheduleRpt(int VehicleId, int branchId, DateTime fromdate, DateTime todate)
        {
            List<DriverScheduleEntity> schedule = new List<DriverScheduleEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("VehicleId", VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("branchId", branchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Todate", todate, DbType.DateTime));
                DataTable dtgrn = dbHelper.ExecuteDataTable(TransportQuery.GetAllScheduleRpt, paramCollection, CommandType.StoredProcedure);
                schedule = dtgrn.AsEnumerable()
                            .Select(row => new DriverScheduleEntity
                            {
                                DriverId = row.Field<int?>("DriverId"),
                                DriverName = row.Field<string>("DriverName"),
                                FromDate = row.Field<DateTime>("FromDate"),
                                StrFromDate = row.Field<DateTime?>("FromDate").DateTimeFormat1(),
                                FromPlace = row.Field<int?>("FromPlace"),
                                ScheduleId = row.Field<int>("ScheduleId"),
                                StartReading = row.Field<double>("StartReading"),
                                ToPlace = row.Field<string>("ToPlace"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                BranchName = row.Field<string>("BranchName"),
                                Advance = row.Field<double>("Advance"),
                                FromBranchName = row.Field<string>("FromBranchName")

                            }).GroupBy(test => test.ScheduleId).Select(grp => grp.First()).ToList();
                foreach (var M in schedule)
                {
                    M.fuelDetail = dtgrn.AsEnumerable().Select(dtrow => new FuelDetailsEntity
                    {
                        ScheduleId = dtrow.Field<int>("ScheduleId"),
                        BalanceAmount = dtrow.Field<double>("BalanceAmount"),
                        Completed = dtrow.Field<bool>("Completed"),
                        CompletedDate = dtrow.Field<DateTime?>("CompletedDate"),
                        StrCompletedDate = dtrow.Field<DateTime?>("CompletedDate").DateTimeFormat1(),
                        EndReading = dtrow.Field<double>("EndReading"),
                        FuelAmount = dtrow.Field<double>("FuelAmount"),
                        FuelPrice = dtrow.Field<double>("FuelPrice"),
                        FuelPump = dtrow.Field<int>("FuelPump"),
                        FuelQuantity = dtrow.Field<double>("FuelQuantity"),
                        FuelType = dtrow.Field<int>("FuelType"),
                        OtherExpense = dtrow.Field<double>("OtherExpense"),
                        PayCardNo = dtrow.Field<string>("PayCardNo"),
                        Paymode = dtrow.Field<int>("Paymode"),
                    }).Where(mo => mo.ScheduleId == M.ScheduleId).FirstOrDefault();
                }
            } 
            return schedule;
        }
        public IEnumerable<DriverScheduleEntity> GetFuelconsumptionvehiclewiserpt(int branchId, DateTime fromdate, DateTime todate, int vehicletypeId)
        {
            List<DriverScheduleEntity> schedule = new List<DriverScheduleEntity>();
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", branchId, DbType.Int32));
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Todate", todate, DbType.DateTime));
                paramCollection.Add(new DBParameter("VehicletypeId", vehicletypeId, DbType.Int32));
                DataTable dtgrn = dbHelper.ExecuteDataTable(TransportQuery.Fuelconsumptionvehiclewiserpt, paramCollection, CommandType.StoredProcedure);
                schedule = dtgrn.AsEnumerable()
                            .Select(row => new DriverScheduleEntity
                            {
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                ScheduleId = row.Field<int>("ScheduleId"),
                                BranchName = row.Field<string>("BranchName"),
                                FromDate = row.Field<DateTime>("FromDate"),
                                StrFromDate = row.Field<DateTime?>("FromDate").DateTimeFormat1(),
                                StartReading = row.Field<double>("StartReading"),
                            }).GroupBy(test => test.ScheduleId).Select(grp => grp.First()).ToList();
                foreach (var M in schedule)
                {
                    M.fuelDetail = dtgrn.AsEnumerable().Select(dtrow => new FuelDetailsEntity
                    {
                        ScheduleId = dtrow.Field<int>("ScheduleId"),
                        FuelAmount = dtrow.Field<double>("FuelAmount"),
                        FuelQuantity = dtrow.Field<double>("FuelQuantity"),
                        FuelType = dtrow.Field<int>("FuelType"),
                        StrFuelType = dtrow.Field<string>("StrFuelType"),
                    }).FirstOrDefault(mo => mo.ScheduleId == M.ScheduleId);
                }
            }
            return schedule;
        }
        public bool UpdateDriverSchedule(DriverScheduleEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ScheduleId", Entity.ScheduleId, DbType.Int32));
                paramCollection.Add(new DBParameter("ToDate", Entity.ToDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToTime", Entity.ToTime, DbType.DateTime));
                paramCollection.Add(new DBParameter("FuelDetailId", Entity.fuelDetail.FuelDetailId, DbType.Int32));
                paramCollection.Add(new DBParameter("FuelType", Entity.fuelDetail.FuelType, DbType.Int32));
                paramCollection.Add(new DBParameter("FuelPump", Entity.fuelDetail.FuelPump, DbType.Int32));
                paramCollection.Add(new DBParameter("FuelPrice", Entity.fuelDetail.FuelPrice, DbType.Double));
                paramCollection.Add(new DBParameter("FuelQuantity", Entity.fuelDetail.FuelQuantity, DbType.Double));
                paramCollection.Add(new DBParameter("FuelAmount", Entity.fuelDetail.FuelAmount, DbType.Double));
                paramCollection.Add(new DBParameter("Paymode", Entity.fuelDetail.Paymode, DbType.Int32));
                paramCollection.Add(new DBParameter("PayCardNo", Entity.fuelDetail.PayCardNo, DbType.String));
                paramCollection.Add(new DBParameter("OtherExpense", Entity.fuelDetail.OtherExpense, DbType.Double));
                paramCollection.Add(new DBParameter("EndReading", Entity.fuelDetail.EndReading, DbType.Double));
                paramCollection.Add(new DBParameter("BalanceAmount", Entity.fuelDetail.BalanceAmount, DbType.Double));
                paramCollection.Add(new DBParameter("Completed", Entity.fuelDetail.Completed, DbType.Boolean));
                paramCollection.Add(new DBParameter("CompletedDate", Entity.fuelDetail.CompletedDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.UpdateFuelDetails, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
