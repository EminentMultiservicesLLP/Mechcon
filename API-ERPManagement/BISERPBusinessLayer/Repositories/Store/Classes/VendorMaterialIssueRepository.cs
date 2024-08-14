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
    public class VendorMaterialIssueRepository : IVendorMaterialIssueRepository
    {

        public IEnumerable<VendorMaterialIssueEntity> GetVendorMaterialIssue(int UserId)
        {
            List<VendorMaterialIssueEntity> materialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                paramCollection.Add(new DBParameter("Authorised", 0, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetVendorMaterialIssue, paramCollection, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new VendorMaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                StoreId = row.Field<int>("StoreId"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                VendorId = row.Field<int>("ManufactureId"),
                                Vendor = row.Field<string>("Manufacturer"),
                                //Nature = row.Field<int>("Nature"),
                                StoreName = row.Field<string>("StoreName")
                            }).ToList();
            }
            return materialIssue;
        }

        public VendorMaterialIssueEntity CreateNewEntry(VendorMaterialIssueEntity entity, BISERPDataLayer.DataAccess.DBHelper dbhelper)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IssueNo", entity.IssueNo, DbType.String, 50, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IssueDate", entity.IssueDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("StoreId", entity.StoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("Nature", entity.Nature, DbType.Int32));
            paramCollection.Add(new DBParameter("ManufactureId", entity.VendorId, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            var parameterList = dbhelper.ExecuteNonQueryForOutParameter(StoreQuery.InsertVendorMaterialIssue, paramCollection,  CommandType.StoredProcedure);
            entity.IssueId = Convert.ToInt32(parameterList["IssueId"].ToString());
            entity.IssueNo = parameterList["IssueNo"].ToString();
            return entity;
        }

        public bool AuthCancelVendorMaterialIssue(VendorMaterialIssueEntity entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueId", entity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedDate", entity.InsertedON, DbType.DateTime));
            if (entity.Authorised == true)
            {
                paramCollection.Add(new DBParameter("Authorised", 1, DbType.Boolean));
            }
            else
            {
                paramCollection.Add(new DBParameter("Cancelled", 1, DbType.Boolean));
            }
            iResult = dbhelper.ExecuteNonQuery(StoreQuery.AuthCancelVendorMI, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }
        public IEnumerable<VendorMaterialIssueEntity> GetVendorMaterialIssuestore(int UserId ,int StoreId)
        {
            List<VendorMaterialIssueEntity> materialIssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("UserId", UserId, DbType.Int32));
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("Authorised", 1, DbType.Int32));
                DataTable dtMaterialIndent = dbHelper.ExecuteDataTable(StoreQuery.GetAuthVendorMaterialForGRNVendor, paramCollection, CommandType.StoredProcedure);
                materialIssue = dtMaterialIndent.AsEnumerable()
                            .Select(row => new VendorMaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                               // StoreId = row.Field<int>("StoreId"),
                                VendorId = row.Field<int>("ManufactureId"),
                                Vendor = row.Field<string>("Manufacturer"),
                            }).ToList();
            }
            return materialIssue;
        }


        public VendorMaterialIssueEntity GetByIssueId(int IssueId)
        {
            VendorMaterialIssueEntity Materialssue = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IssueId", IssueId, DbType.Int32);
                DataTable dtMaterialIssue = dbHelper.ExecuteDataTable(StoreQuery.GetVendorMaterialIssueById, param, CommandType.StoredProcedure);

                Materialssue = dtMaterialIssue.AsEnumerable()
                            .Select(row => new VendorMaterialIssueEntity
                            {
                                IssueId = row.Field<int>("IssueId"),
                                IssueNo = row.Field<string>("IssueNo"),
                                StoreName = row.Field<string>("StoreName"),
                                Vendor = row.Field<string>("Manufacturer"),
                                StoreId = row.Field<int>("StoreId"),
                                VendorId = row.Field<int>("ManufactureId"),
                                IssueDate = row.Field<DateTime>("IssueDate"),
                                strIssueDate = Convert.ToDateTime(row.Field<DateTime>("IssueDate")).ToString("dd-MMMM-yyyy"),
                                Nature = row.Field<int>("Nature")
                            }).FirstOrDefault();
            }
            return Materialssue;
        }
    }
}
