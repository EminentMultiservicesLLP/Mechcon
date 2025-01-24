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
    public class VendorMaterialIssueDtRepository : IVendorMaterialIssueDtRepository
    {

        public int CreateVMIDetails(VendorMaterialIssueEntity mientity, VendorMaterialIssueDtEntity entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueDetailsId", entity.IssueDetailsId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IssueId", mientity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
            paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
            paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Int32));
            paramCollection.Add(new DBParameter("TotalAmt", entity.TotalAmt, DbType.Int32));
            paramCollection.Add(new DBParameter("IssuedQuantity", entity.IssuedQuantity, DbType.Double));
            paramCollection.Add(new DBParameter("AuthorisedQuantity", entity.AuthorisedQuantity, DbType.Double));
            paramCollection.Add(new DBParameter("StoreId", mientity.StoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("ManufactureId", mientity.ManufactureId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", mientity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", mientity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedMacName", mientity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", mientity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("InsertedIPAddress", mientity.InsertedIPAddress, DbType.String));
            iResult = dbhelper.ExecuteNonQueryForOutParameter<int>(StoreQuery.InsertVendorMaterialIssueDetails, paramCollection, CommandType.StoredProcedure, "IssueDetailsId");
            return iResult;
        }

        public IEnumerable<VendorMaterialIssueDtEntity> GetDetailByIssueID(int IssueId)
        {
            List<VendorMaterialIssueDtEntity> MaterialIssueDt = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IssueId", IssueId, DbType.Int32);
                DataTable dtssueDt = dbHelper.ExecuteDataTable(StoreQuery.GetVendorMIDetailsById, param, CommandType.StoredProcedure);
                MaterialIssueDt = dtssueDt.AsEnumerable()
                            .Select(row => new VendorMaterialIssueDtEntity
                            {
                                IssueDetailsId = row.Field<int>("IssueDetailsId"),
                                IssueId = row.Field<int>("IssueId"),
                                ItemId = row.Field<int>("ItemId"),
                                IssuedQuantity = row.Field<double?>("IssuedQuantity"),
                                MRP = row.Field<double?>("MRP"),
                                TotalAmt = row.Field<double?>("TotalAmt"),
                                ItemName = row.Field<string>("ItemName"),
                                BatchName = row.Field<string>("BatchName"),
                                ExpiryDate = Convert.ToDateTime(row.Field<DateTime>("ExpiryDate")).ToString("dd-MMM-yyyy"),
                                BatchId = row.Field<int>("BatchId"),
                                UnitName = row.Field<string>("UnitName"),
                                HSNCode = row.Field<string>("HSNCode"),
                                Item_Stock = row.Field<double>("Qty")
                            }).ToList();
            }
            return MaterialIssueDt;
        }

        public bool UpdateVendorMaterialIssueAuthQty(VendorMaterialIssueEntity mientity, VendorMaterialIssueDtEntity entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueDetailsId", entity.IssueDetailsId, DbType.Int32));
            paramCollection.Add(new DBParameter("IssueId", mientity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
            paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
            paramCollection.Add(new DBParameter("StoreId", mientity.StoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("ManufactureId", mientity.ManufactureId, DbType.Int32));
            paramCollection.Add(new DBParameter("TotalAmt", entity.TotalAmt, DbType.Double));
            paramCollection.Add(new DBParameter("AuthorisedQuantity", entity.AuthorisedQuantity, DbType.Double));
            paramCollection.Add(new DBParameter("AuthorisedBy", mientity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedOn", mientity.InsertedON, DbType.DateTime));

            //paramCollection.Add(new DBParameter("InsertedIPAddress", mientity.InsertedIPAddress, DbType.String));
            //paramCollection.Add(new DBParameter("InsertedMacName", mientity.InsertedMacName, DbType.String));
            //paramCollection.Add(new DBParameter("InsertedMacID", mientity.InsertedMacID, DbType.String));
            if (mientity.Authorised == true)
            {
                if (entity.AuthorisedQuantity > 0)
                {
                    paramCollection.Add(new DBParameter("Authorised", 1, DbType.Boolean));
                }
                else
                {
                    paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
                }
            }
            else
            {
                paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
            }
            iResult = dbhelper.ExecuteNonQuery(StoreQuery.UpdVendorMIAuthQty, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public List<VendorMaterialIssueDtEntity> GetDetailByIssueIDGRN(int IssueId)
        {
            List<VendorMaterialIssueDtEntity> MaterialIssueDt = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IssueId", IssueId, DbType.Int32);
                DataTable dtssueDt = dbHelper.ExecuteDataTable(StoreQuery.GetAllVendorMaterialIssueAuth, param, CommandType.StoredProcedure);
                MaterialIssueDt = dtssueDt.AsEnumerable()
                            .Select(row => new VendorMaterialIssueDtEntity
                            {
                                IssueDetailsId = row.Field<int>("IssueDetailsId"),
                                IssueId = row.Field<int>("IssueId"),
                                ItemId = row.Field<int>("ItemId"),
                                AuthorisedQuantity = row.Field<double?>("AuthorisedQuantity"),
                                IssuedQuantity = row.Field<double?>("IssuedQuantity"),
                                ItemName = row.Field<string>("ItemName"),
                                BatchName = row.Field<string>("BatchName"),
                                ExpiryDate = Convert.ToDateTime(row.Field<DateTime>("ExpiryDate")).ToString("dd-MMM-yyyy"),
                                BatchId = row.Field<int>("BatchId"),
                                UnitName = row.Field<string>("UnitName"),
                                TotalAmt = row.Field<double>("TotalAmt"),
                            }).ToList();
            }
            return MaterialIssueDt;
        }

    }
}
