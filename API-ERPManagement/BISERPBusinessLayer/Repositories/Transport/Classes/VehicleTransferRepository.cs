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
    public class VehicleTransferRepository : IVehicleTransferRepository
    {
        public int CreateVehicleTransfer(VehicleTransferEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TransferId", Entity.TransferId, DbType.Int32, ParameterDirection.Output));
                paramCollection.Add(new DBParameter("VehicleId", Entity.VehicleId, DbType.Int32));
                paramCollection.Add(new DBParameter("TransferSold", Entity.TransferSold, DbType.Boolean));
                paramCollection.Add(new DBParameter("TransferDate", Entity.TransferDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("SoldDate", Entity.SoldDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("OldBranchId", Entity.OldBranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("NewBranchId", Entity.NewBranchId, DbType.Int32));
                paramCollection.Add(new DBParameter("EMICompleted", Entity.EMICompleted, DbType.Double));
                paramCollection.Add(new DBParameter("EMIPending", Entity.EMIPending, DbType.Double));
                paramCollection.Add(new DBParameter("TransferReason", Entity.TransferReason, DbType.String));

                paramCollection.Add(new DBParameter("CustomerName", Entity.CustomerName, DbType.String));
                paramCollection.Add(new DBParameter("SoldAmount", Entity.SoldAmount, DbType.String));
                paramCollection.Add(new DBParameter("SoldReason", Entity.SoldReason, DbType.String));

                paramCollection.Add(new DBParameter("InsertedBy", Entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedON", Entity.InsertedON, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIPAddress", Entity.InsertedIPAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", Entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacID", Entity.InsertedMacID, DbType.String));
                iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(TransportQuery.InsertVehicleTransfer, paramCollection, CommandType.StoredProcedure, "TransferId");
            }
            return iResult;
        }

        public IEnumerable<VehicleTransferEntity> GetAllVehicleTransfer()
        {
            List<VehicleTransferEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.GetVehicleTransfer, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new VehicleTransferEntity
                            {
                                TransferId = row.Field<int>("TransferId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                TransferSold = row.Field<bool>("TransferSold"),
                                OldBranchId = row.Field<int>("OldBranchId"),
                                OldBranch = row.Field<string>("OldBranch"),
                                NewBranchId = row.Field<int>("NewBranchId"),
                                NewBranch = row.Field<string>("NewBranch"),
                                EMICompleted = row.Field<int>("EMICompleted"),
                                EMIPending = row.Field<int>("EMIPending"),
                                CustomerName = row.Field<string>("CustomerName"),
                                SoldAmount = row.Field<double?>("SoldAmount"),
                                SoldReason = row.Field<string>("SoldReason"),
                                TransferReason = row.Field<string>("TransferReason"),
                                strTransferDate = row.Field<DateTime?>("TransferDate").DateTimeFormat1(),
                                strSoldDate = row.Field<DateTime?>("TransferDate").DateTimeFormat1(),
                            }).ToList();
            }
            return tax;
        }
        public IEnumerable<VehicleTransferEntity> GetAllVehicleTransferRPT(DateTime fromdate, DateTime todate)
        {
            List<VehicleTransferEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.rptVehicleTransfer,paramCollection, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new VehicleTransferEntity
                            {
                                TransferId = row.Field<int>("TransferId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                TransferSold = row.Field<bool>("TransferSold"),
                                OldBranchId = row.Field<int>("OldBranchId"),
                                OldBranch = row.Field<string>("OldBranch"),
                                NewBranchId = row.Field<int>("NewBranchId"),
                                NewBranch = row.Field<string>("NewBranch"),
                                EMICompleted = row.Field<int>("EMICompleted"),
                                EMIPending = row.Field<int>("EMIPending"),
                                TransferReason = row.Field<string>("TransferReason"),
                                TransferDate = row.Field<DateTime>("TransferDate"),
                                strTransferDate = row.Field<DateTime?>("TransferDate").DateTimeFormat1(),
                                strSoldDate = row.Field<DateTime?>("TransferDate").DateTimeFormat1(),
                                SoldAmount = row.Field<double>("SoldAmount"),
                                SoldReason = row.Field<string>("SoldReason"),
                                CustomerName = row.Field<string>("CustomerName"),
                            }).ToList();
            }
            return tax;
        }

        public IEnumerable<VehicleTransferEntity> GetAllVehicleSoldRPT(DateTime fromdate, DateTime todate)
        {
            List<VehicleTransferEntity> tax = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("todate", todate, DbType.DateTime));
                DataTable dtUnits = dbHelper.ExecuteDataTable(TransportQuery.rptVehicleSold, paramCollection, CommandType.StoredProcedure);
                tax = dtUnits.AsEnumerable()
                            .Select(row => new VehicleTransferEntity
                            {
                                TransferId = row.Field<int>("TransferId"),
                                VehicleId = row.Field<int>("VehicleId"),
                                VehicleNo = row.Field<string>("VehicleNo"),
                                TransferSold = row.Field<bool>("TransferSold"),
                                OldBranchId = row.Field<int>("OldBranchId"),
                                OldBranch = row.Field<string>("OldBranch"),
                                NewBranchId = row.Field<int>("NewBranchId"),
                                NewBranch = row.Field<string>("NewBranch"),
                                EMICompleted = row.Field<int>("EMICompleted"),
                                EMIPending = row.Field<int>("EMIPending"),
                                TransferReason = row.Field<string>("TransferReason"),
                                TransferDate = row.Field<DateTime>("TransferDate"),
                                strTransferDate = row.Field<DateTime?>("TransferDate").DateTimeFormat1(),
                                strSoldDate = row.Field<DateTime?>("TransferDate").DateTimeFormat1(),
                                SoldAmount = row.Field<double>("SoldAmount"),
                                SoldReason = row.Field<string>("SoldReason"),
                                CustomerName = row.Field<string>("CustomerName"),
                            }).ToList();
            }
            return tax;
        }


        public bool AuthorizeCancel(VehicleTransferEntity Entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("TransferId", Entity.TransferId, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthorisedBy", Entity.AuthorisedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("AuthorisedDate", Entity.AuthorisedDate, DbType.DateTime));
                if (Entity.Authorised == true)
                {
                    paramCollection.Add(new DBParameter("Authorised", 1, DbType.Boolean));
                }
                else
                {
                    paramCollection.Add(new DBParameter("Authorised", 0, DbType.Boolean));
                }
                iResult = dbHelper.ExecuteNonQuery(TransportQuery.AuthorizeCancelTransfer, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult > 0)
                return true;
            else
                return false;
        }
    }
}
