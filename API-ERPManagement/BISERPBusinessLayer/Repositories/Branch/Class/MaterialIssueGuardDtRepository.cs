using BISERPBusinessLayer.Entities.Branch;
using BISERPBusinessLayer.QueryCollection.Branch;
using BISERPBusinessLayer.Repositories.Branch.Interface;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPCommon.Extensions;

namespace BISERPBusinessLayer.Repositories.Branch.Class
{
    public class MaterialIssueGuardDtRepository : IMaterialIssueGuardDtRepository
    {
        public int CreateEntity(MaterialIssueGuardEntity mientity, MaterialIssueGuardDtEntity entity, DBHelper dbHelper)
        {
            int iResult = 0;
            string Renewdate = "";
            string[] arrRenewdate;
            if (entity.strRenewDate == null)
            {
                Renewdate = DateTime.Now.AddYears(1).ToString("dd-MMM-yyyy");
            }
            else
            {
                arrRenewdate = entity.strRenewDate.Split('/');
                Renewdate = arrRenewdate[1].ToString() + "-" + arrRenewdate[0].ToString() + "-" + arrRenewdate[2].ToString();
            }
            DateTime RenewDate = Convert.ToDateTime(Renewdate);
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IssueDetailsId", entity.IssueDetailsId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IssueId", mientity.IssueId, DbType.Int32));
            paramCollection.Add(new DBParameter("ItemId", entity.ItemId, DbType.Int32));
            paramCollection.Add(new DBParameter("BatchId", entity.BatchId, DbType.Int32));
            paramCollection.Add(new DBParameter("IssuedQuantity", entity.IssuedQuantity, DbType.Double));
            paramCollection.Add(new DBParameter("MRP", entity.MRP, DbType.Double));
            paramCollection.Add(new DBParameter("Discount", entity.Discount, DbType.Double));
            paramCollection.Add(new DBParameter("StoreId", mientity.StoreId, DbType.Int32));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("RenewDate", RenewDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedBy", mientity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", mientity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedMacName", mientity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", mientity.InsertedMacID, DbType.String));
            paramCollection.Add(new DBParameter("InsertedIPAddress", mientity.InsertedIPAddress, DbType.String));
            iResult = dbHelper.ExecuteNonQueryForOutParameter<int>(BranchQueries.InsertMaterialIssueDetails, paramCollection, CommandType.StoredProcedure, "IssueDetailsId");
            return iResult;
        }

        public IEnumerable<MaterialIssueGuardDtEntity> GetIssueDetails(int IssueId)
        {
            List<MaterialIssueGuardDtEntity> issuelist = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IssueId", IssueId, DbType.Int32);
                DataTable dtIssueDt = dbHelper.ExecuteDataTable(BranchQueries.GetIssueGuardDetails, param, CommandType.StoredProcedure);
                issuelist = dtIssueDt.AsEnumerable()
                            .Select(row => new MaterialIssueGuardDtEntity
                            {
                                IssueDetailsId = row.Field<int>("IssueDetailsId"),
                                IssueId = row.Field<int>("IssueId"),
                                ItemId = row.Field<int>("ItemId"),
                                ItemName = row.Field<string>("ItemName"),
                                BatchName = row.Field<string>("BatchName"),
                                BatchId = row.Field<int>("BatchId"),
                                IssuedQuantity = row.Field<double>("IssuedQuantity"),
                                MRP = row.Field<double?>("MRP"),
                                Discount = row.Field<double?>("Discount"),
                                Amount = row.Field<double?>("Amount"),
                                //RenewDate = row.Field<DateTime>("RenewDate"),
                                strRenewDate = row.Field<DateTime?>("RenewDate").DateTimeFormat2()
                            }).ToList();
            }
            return issuelist;
        }
        
    }
}
