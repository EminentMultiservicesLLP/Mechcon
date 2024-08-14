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
    public class VehicleInfoRepository : IVehicleInfoRepository
    {
        public IEnumerable<VehicleInfoEntity> GetAllVehicle(int UserId)
        {
            List<VehicleInfoEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserId", UserId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllVehicleInfo, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new VehicleInfoEntity
                            {
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                BranchId = row.Field<int>("BranchId"),
                                DivisionId = row.Field<int>("DivisionId"),
                                VehicleTypeId = row.Field<int>("VehicleTypeId"),
                                VehicleMakeId = row.Field<int>("VehicleMakeId"),
                                PurchaseDate = row.Field<DateTime>("PurchaseDate"),
                                strPurchaseDate = row.Field<DateTime?>("PurchaseDate").DateTimeFormat1(),
                                UsageId = row.Field<int>("UsageId"),
                                PurchaseAmount = row.Field<double?>("PurchaseAmount"),
                                EMIMonths = row.Field<int?>("EMIMonths"),
                                EMIAmount = row.Field<double?>("EMIAmount"),
                                ModelId = row.Field<int>("ModelId"),
                                ModelYear = row.Field<string>("ModelYear"),
                                ChasisNo = row.Field<string>("ChasisNo"),
                                EngineNo = row.Field<string>("EngineNo"),
                                RCBookName = row.Field<string>("RCBookName"),
                                BranchName = row.Field<string>("BranchName"),
                                DivisionName = row.Field<string>("DivisionName"),
                                TypeName = row.Field<string>("TypeName"),
                                UsedFor = row.Field<string>("UsedFor"),
                                ModelName = row.Field<string>("ModelName"),
                                Deactive = row.Field<bool>("Deactive"),
                                DriverId = row.Field<int>("DriverId"),
                                DriverName = row.Field<string>("EmpName"),
                                UsedBy = row.Field<string>("UsedBy"),
                                EMICompleted = row.Field<double?>("EMICompleted"),
                                EMIPending = row.Field<double?>("EMIPending")
                            }).ToList();
            }
            return tax;
        }
         public  IEnumerable<VehicleInfoEntity> GetAllVehicleNo(int branchId)
         {
          List<VehicleInfoEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("BranchId", branchId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllVehicleNo, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new VehicleInfoEntity
                            {
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                BranchId = row.Field<int>("BranchId")
                            }).ToList();
            }
            return tax;
         }

        public IEnumerable<VehicleInfoEntity> GetAllVehicleSchedule(int BranchId)
        {
            List<VehicleInfoEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("BranchId", BranchId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllVehicleInfoSchedule, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new VehicleInfoEntity
                            {
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                BranchId = row.Field<int>("BranchId"),
                                DivisionId = row.Field<int>("DivisionId"),
                                VehicleTypeId = row.Field<int>("VehicleTypeId"),
                                PurchaseDate = row.Field<DateTime?>("PurchaseDate"),
                                UsageId = row.Field<int>("UsageId"),
                                ModelId = row.Field<int>("ModelId"),
                                ModelYear = row.Field<string>("ModelYear"),
                                ChasisNo = row.Field<string>("ChasisNo"),
                                EngineNo = row.Field<string>("EngineNo"),
                                RCBookName = row.Field<string>("RCBookName"),
                                BranchName = row.Field<string>("BranchName"),
                                DivisionName = row.Field<string>("DivisionName"),
                                TypeName = row.Field<string>("TypeName"),
                                UsedFor = row.Field<string>("UsedFor"),
                                ModelName = row.Field<string>("ModelName"),
                                Deactive = row.Field<bool>("Deactive"),
                                DriverId = row.Field<int>("DriverId"),
                                DriverName = row.Field<string>("EmpName"),
                                UsedBy = row.Field<string>("UsedBy")
                            }).ToList();
            }
            return tax;
        }

        public int SaveVehicleInfo(VehicleInfoEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VehicleNo", Entity.VehicleNo, DbType.String));
                paramCollection.Add(new DBParameter("BranchId", Entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("DivisionId", Entity.DivisionId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleTypeId", Entity.VehicleTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleMakeId", Entity.VehicleMakeId, DbType.Int32));
                paramCollection.Add(new DBParameter("UsageId", Entity.UsageId, DbType.Int32));
                paramCollection.Add(new DBParameter("ModelId", Entity.ModelId, DbType.Int32));
                paramCollection.Add(new DBParameter("ModelYear", Entity.ModelYear, DbType.String));
                paramCollection.Add(new DBParameter("ChasisNo", Entity.ChasisNo, DbType.String));
                paramCollection.Add(new DBParameter("EngineNo", Entity.EngineNo, DbType.String));
                paramCollection.Add(new DBParameter("PurchaseDate", Entity.PurchaseDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("PurchaseAmount", Entity.PurchaseAmount, DbType.Double));
                paramCollection.Add(new DBParameter("EMIMonths", Entity.EMIMonths, DbType.Int32));
                paramCollection.Add(new DBParameter("EMIAmount", Entity.EMIAmount, DbType.Double));
                paramCollection.Add(new DBParameter("RCBookName", Entity.RCBookName, DbType.String));
                paramCollection.Add(new DBParameter("DriverId", Entity.DriverId, DbType.Int32));
                paramCollection.Add(new DBParameter("UsedBy", Entity.UsedBy, DbType.String));
                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsertVehicleInfo, paramCollection, CommandType.StoredProcedure, "VehicleId");
            }
            return iResult;
        }

        public bool UpdateVehicleInfo(VehicleInfoEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleNo", Entity.VehicleNo, DbType.String));
                paramCollection.Add(new DBParameter("BranchId", Entity.BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("DivisionId", Entity.DivisionId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleTypeId", Entity.VehicleTypeId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleMakeId", Entity.VehicleMakeId, DbType.Int32));
                paramCollection.Add(new DBParameter("UsageId", Entity.UsageId, DbType.Int32));
                paramCollection.Add(new DBParameter("ModelId", Entity.ModelId, DbType.Int32));
                paramCollection.Add(new DBParameter("ModelYear", Entity.ModelYear, DbType.String));
                paramCollection.Add(new DBParameter("ChasisNo", Entity.ChasisNo, DbType.String));
                paramCollection.Add(new DBParameter("EngineNo", Entity.EngineNo, DbType.String));
                paramCollection.Add(new DBParameter("PurchaseDate", Entity.PurchaseDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("PurchaseAmount", Entity.PurchaseAmount, DbType.Double));
                paramCollection.Add(new DBParameter("EMIMonths", Entity.EMIMonths, DbType.Int32));
                paramCollection.Add(new DBParameter("EMIAmount", Entity.EMIAmount, DbType.Double));
                paramCollection.Add(new DBParameter("RCBookName", Entity.RCBookName, DbType.String));
                paramCollection.Add(new DBParameter("DriverId", Entity.DriverId, DbType.Int32));
                paramCollection.Add(new DBParameter("UsedBy", Entity.UsedBy, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("UpdatedOn", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("UpdatedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("UpdatedMacID", Entity.InsertedMacID, DbType.String));
                paramCollection.Add(new DBParameter("Deactive", Entity.Deactive, DbType.Boolean));
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.UpdateVehicleInfo, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public bool CheckDuplicateVehicle(VehicleInfoEntity Entity)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("VehicleNo", Entity.VehicleNo, DbType.String));
                paramCollection.Add(new DBParameter("ChasisNo", Entity.ChasisNo, DbType.String));
                paramCollection.Add(new DBParameter("EngineNo", Entity.EngineNo, DbType.String));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(TransportQuery.CheckDpVehicle, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public IEnumerable<VehicleInfoEntity> VehicleInfoReport(int BranchId,int VehicleId)
        {
            List<VehicleInfoEntity> vehicle = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("BranchId", BranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("VehicleId", VehicleId, DbType.Int32));
                DataTable dtvehicle = dbHelper.ExecuteDataTable(TransportQuery.VehicleInfoReport, paramCollection, CommandType.StoredProcedure);
                
                vehicle = dtvehicle.AsEnumerable()
                            .Select(row => new VehicleInfoEntity
                            {
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                PurchaseDate = row.Field<DateTime?>("PurchaseDate"),
                                PurchaseAmount = row.Field<double?>("PurchaseAmount"),
                                EMIMonths = row.Field<int?>("EMIMonths"),
                                EMIAmount = row.Field<double?>("EMIAmount"),
                                ModelYear = row.Field<string>("ModelYear"),
                                ChasisNo = row.Field<string>("ChasisNo"),
                                EngineNo = row.Field<string>("EngineNo"),
                                RCBookName = row.Field<string>("RCBookName"),
                                BranchName = row.Field<string>("BranchName"),
                                DivisionName = row.Field<string>("DivisionName"),
                                TypeName = row.Field<string>("TypeName"),
                                UsedFor = row.Field<string>("UsedFor"),
                                ModelName = row.Field<string>("ModelName"),
                                Deactive = row.Field<bool>("Deactive"),
                                DriverName = row.Field<string>("EmpName"),
                                UsedBy = row.Field<string>("UsedBy")
                            }).ToList();
                foreach (var V in vehicle)
                {
                    var insuranceDataRow = dtvehicle.Select("InsuranceId is not null");
                    if (insuranceDataRow.Length > 0)
                    {
                        var insuranceData = insuranceDataRow.CopyToDataTable();

                        V.insurancemodel = insuranceData.AsEnumerable()
                                .Select(row => new InsuranceEntity
                                {
                                    InsuranceId = row.Field<int>("InsuranceId"),
                                    VehicleId = row.Field<int>("VehicleId"),
                                    PolicyNo = row.Field<string>("PolicyNo"),
                                    CompanyId = row.Field<int>("CompanyId"),
                                    CompanyName = row.Field<string>("CompanyName"),
                                    IssueDate = row.Field<DateTime?>("I_IssueDate"),
                                    ExpiryDate = row.Field<DateTime>("I_ExpiryDate"),
                                    strIssueDate = row.Field<DateTime?>("I_IssueDate").DateTimeFormat1(),
                                    strExpiryDate = row.Field<DateTime?>("I_ExpiryDate").DateTimeFormat1(),
                                    Amount = row.Field<double>("I_Amount"),
                                    ReminderDays = row.Field<int>("I_ReminderDays"),
                                    Agent = row.Field<string>("Agent"),
                                    CovertNote = row.Field<string>("CovertNote"),
                                   InsuranceType = row.Field<int?>("InsuranceType")
                                }).Where(mo => mo.VehicleId == V.VehicleId).FirstOrDefault();
                    }

                    var greentaxDataRow = dtvehicle.Select("GreenTaxId is not null");
                    if (greentaxDataRow.Length > 0)
                    {
                        var greentaxData = greentaxDataRow.CopyToDataTable();
                        V.gtmodel = greentaxData.AsEnumerable()
                            .Select(row => new GreenTaxEntity
                            {
                                GreenTaxId = row.Field<int>("GreenTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                GreenTaxNo = row.Field<string>("GreenTaxNo"),
                                Amount = row.Field<double>("G_Amount"),
                                IssueDate = row.Field<DateTime?>("G_IssueDate"),
                                ExpiryDate = row.Field<DateTime?>("G_ExpiryDate"),
                                strIssueDate = row.Field<DateTime?>("G_IssueDate").DateTimeFormat1(),
                                strExpiryDate = row.Field<DateTime?>("G_ExpiryDate").DateTimeFormat1(),
                                ReminderDays = row.Field<int>("G_ReminderDays")
                            }).Where(mo => mo.VehicleId == V.VehicleId).FirstOrDefault();
                    }

                    var roadtaxDataRow = dtvehicle.Select("RoadTaxId is not null");
                    if (roadtaxDataRow.Length > 0)
                    {
                        var roadtaxData = roadtaxDataRow.CopyToDataTable();
                        V.rtmodel = roadtaxData.AsEnumerable()
                            .Select(row => new RoadTaxEntity
                            {
                                RoadTaxId = row.Field<int>("RoadTaxId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                TaxNo = row.Field<string>("TaxNo"),
                                Amount = row.Field<double>("R_Amount"),
                                IssueDate = row.Field<DateTime>("R_IssueDate"),
                                ExpiryDate = row.Field<DateTime>("R_ExpiryDate"),
                                strIssueDate = row.Field<DateTime?>("R_IssueDate").DateTimeFormat1(),
                                strExpiryDate = row.Field<DateTime?>("R_ExpiryDate").DateTimeFormat1(),
                                ReminderDays = row.Field<int>("R_ReminderDays")
                            }).Where(mo => mo.VehicleId == V.VehicleId).FirstOrDefault();
                    }
                    var pucDataRow = dtvehicle.Select("PUCId is not null");
                    if (pucDataRow.Length > 0)
                    {
                        var pucData = pucDataRow.CopyToDataTable();
                        V.pucmodel = pucData.AsEnumerable()
                            .Select(row => new PUCDetailsEntity
                            {
                                PUCId = row.Field<int>("PUCId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                PUCNo = row.Field<string>("PUCNo"),
                                Amount = row.Field<double>("PU_Amount"),
                                IssueDate = row.Field<DateTime?>("PU_IssueDate"),
                                ExpiryDate = row.Field<DateTime?>("PU_ExpiryDate"),
                                strIssueDate = row.Field<DateTime?>("PU_IssueDate").DateTimeFormat1(),
                                strExpiryDate = row.Field<DateTime?>("PU_ExpiryDate").DateTimeFormat1(),
                                ReminderDays = row.Field<int>("PU_ReminderDays")
                            }).Where(mo => mo.VehicleId == V.VehicleId).FirstOrDefault();
                    }
                    var permitDataRow = dtvehicle.Select("PermitId is not null");
                    if (permitDataRow.Length > 0)
                    {
                        var permitData = permitDataRow.CopyToDataTable();
                        V.prmtmodel = permitData.AsEnumerable()
                                .Select(row => new PermitDetailsEntity
                                {
                                    PermitId = row.Field<int>("PermitId"),
                                    VehicleId = row.Field<int>("VehicleId"),
                                    PermitNo = row.Field<string>("PermitNo"),
                                    Amount = row.Field<double>("PR_Amount"),
                                    IssueDate = row.Field<DateTime?>("PR_IssueDate"),
                                    ExpiryDate = row.Field<DateTime?>("PR_ExpiryDate"),
                                    strIssueDate = row.Field<DateTime?>("PR_IssueDate").DateTimeFormat1(),
                                    strExpiryDate = row.Field<DateTime?>("PR_ExpiryDate").DateTimeFormat1(),
                                    ReminderDays = row.Field<int>("PR_ReminderDays")
                                }).Where(mo => mo.VehicleId == V.VehicleId).FirstOrDefault();
                    }
                }
            }
        
            return vehicle;
        }

        public IEnumerable<VehicleInfoEntity> GetAllVehicleListRpt(int BranchId)
        {
            List<VehicleInfoEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("BranchId", BranchId, DbType.Int32);
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetAllVehicleListRpt, param, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new VehicleInfoEntity
                            {
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                BranchId = row.Field<int>("BranchId"),
                                BranchName = row.Field<string>("BranchName"),
                                DivisionName = row.Field<string>("DivisionName"),
                                TypeName = row.Field<string>("TypeName"),
                                ModelYear = row.Field<string>("ModelYear"),
                                ModelName = row.Field<string>("ModelName"),
                                ChasisNo = row.Field<string>("ChasisNo"),
                                EngineNo = row.Field<string>("EngineNo"),
                                Deactive = row.Field<bool>("Deactive"),
                            }).ToList();
            }
          
            return tax;
        }
    }
}
