using BISERPBusinessLayer.Repositories.Billing.Interface;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Billing;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.QueryCollection.Billing;
using System.Data;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPBusinessLayer.Repositories.Billing.Class
{
    public class ClientBillCancelRepository : IClientBillCancelRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(ClientBillCancelRepository));

        public bool CancelGeneratedBill(ClientBillingEntity entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ClientBillId", entity.ClientBillId, DbType.Int32));
                paramCollection.Add(new DBParameter("Cancel", 1, DbType.Boolean));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(BillingQuery.CancelGeneratedBill, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult != 0)
                return true;
            else
                return false;
        }

        public bool CancelRecieptBill(ClientRecieptEntiy entity)
        {
            int iResult = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("RecieptId", entity.RecieptId, DbType.Int32));
                paramCollection.Add(new DBParameter("Cancel", 1, DbType.Boolean));
                paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                paramCollection.Add(new DBParameter("InsertedIpAddress", entity.InsertedIpAddress, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                paramCollection.Add(new DBParameter("InsertedMacId", entity.InsertedMacId, DbType.String));
                iResult = dbHelper.ExecuteNonQuery(BillingQuery.CancelRecieptBill, paramCollection, CommandType.StoredProcedure);
            }
            if (iResult != 0)
                return true;
            else
                return false;
        }

        public bool CheckReciept(ClientBillingEntity entity)
        {
            bool bResult = false;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("ClientBillId", entity.ClientBillId));
                paramCollection.Add(new DBParameter("IsExist", true, DbType.Boolean, ParameterDirection.Output));

                bResult = dbHelper.ExecuteNonQueryForOutParameter<bool>(BillingQuery.CheckReciept, paramCollection, CommandType.StoredProcedure, "IsExist");
            }
            return bResult;
        }

        public IEnumerable<GRNEntity> GetTaxCredited(int projectId)
        {
            List<GRNEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("projectId", projectId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetTaxCredited, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new GRNEntity
                            {
                                Storeid = row.Field<int>("Storeid"),
                               ID = row.Field<int>("GRNId"),
                                StoreName = row.Field<string>("StoreName"),
                                GRNNo = row.Field<string>("GRNNo"),
                                strGRNDate = row.Field<string>("strGRNDate"),
                                TotalAmount = row.Field<double>("TotalAmount"),
                                BillPaid = row.Field<double>("BillPaid"),
                                BillBalance = row.Field<double>("BillBalance"),
                                TotalTaxamt = row.Field<double>("TotalTaxamt")
                            }).ToList();
            }
            return type;
        }
        public IEnumerable<ClientBillingEntity> GetTaxPaid(int projectId)
        {
            List<ClientBillingEntity> type = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("projectId", projectId, DbType.Int32);
                DataTable dtType = dbHelper.ExecuteDataTable(BillingQuery.GetTaxPaid, param, CommandType.StoredProcedure);
                type = dtType.AsEnumerable()
                            .Select(row => new ClientBillingEntity
                            {
                                ClientBillId = row.Field<int>("ClientBillId"),
                                BillNo = row.Field<string>("BillNo"),
                                StoreName = row.Field<string>("StoreName"),
                                StrBillDate = row.Field<string>("StrBillDate"),
                                NetAmt = row.Field<double>("NetAmt"),
                                TotalRecieved = row.Field<double>("TotalRecieved"),
                                TaxAmt = row.Field<double>("TotalTaxamt")
                            }).ToList();
            }
            return type;
        }
       
    }
}
