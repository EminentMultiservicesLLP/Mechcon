using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        public WorkOrderEntities CreateWorkOrder(WorkOrderEntities entity, DBHelper dbhelper)
        {
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentId", entity.IndentId, DbType.Int32, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IndentNumber", entity.IndentNumber, DbType.String, 50, ParameterDirection.Output));
            paramCollection.Add(new DBParameter("IndentDate", entity.IndentDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("QuotationDeadLine", entity.QuotationDeadLine, DbType.DateTime));
            paramCollection.Add(new DBParameter("IndentNature", entity.IndentNature, DbType.String));
            paramCollection.Add(new DBParameter("PurchaseIndentId", entity.PurchaseIndentId, DbType.Int32));
            paramCollection.Add(new DBParameter("Storeid", entity.Storeid, DbType.Int32));
            //paramCollection.Add(new DBParameter("VendorID", entity.VendorID, DbType.Int32));
            //paramCollection.Add(new DBParameter("VendorName", entity.VendorName, DbType.String));
            paramCollection.Add(new DBParameter("ItemCategoryId", entity.ItemCategoryId, DbType.Int32));
            paramCollection.Add(new DBParameter("RequiredDate", entity.RequiredDate, DbType.String));
            paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
            paramCollection.Add(new DBParameter("DepartmentId", entity.DepartmentId, DbType.Int32));
            paramCollection.Add(new DBParameter("BudgetId", entity.BudgetId, DbType.Int32));
            paramCollection.Add(new DBParameter("DeliveryStartDate", entity.DeliveryStartDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("DeliveryEndDate", entity.DeliveryEndDate, DbType.DateTime));
            paramCollection.Add(new DBParameter("Status", entity.Status, DbType.String));
            paramCollection.Add(new DBParameter("ProcurementTypeID", entity.ProcurementTypeID, DbType.Int32));
            paramCollection.Add(new DBParameter("BranchID", entity.BranchID, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
            var parameterList = dbhelper.ExecuteNonQueryForOutParameter(PurchaseQueries.InsertWorkOrder, paramCollection, CommandType.StoredProcedure);
            entity.IndentId = Convert.ToInt32(parameterList["IndentId"].ToString());
            entity.IndentNumber = parameterList["IndentNumber"].ToString();
            return entity;
        }

        public bool UpdateWorkOrder(WorkOrderEntities entity, DBHelper dbhelper)
        {
            int Result = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentId", entity.IndentId, DbType.Int32));
            paramCollection.Add(new DBParameter("Remarks", entity.Remarks, DbType.String));
            paramCollection.Add(new DBParameter("IndentNature", entity.IndentNature, DbType.String));
            paramCollection.Add(new DBParameter("QuotationDeadLine", entity.QuotationDeadLine, DbType.DateTime));
            paramCollection.Add(new DBParameter("RequiredDate", entity.RequiredDate, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("UpdatedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.InsertedIPAddress, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacName", entity.InsertedMacName, DbType.String));
            paramCollection.Add(new DBParameter("UpdatedMacID", entity.InsertedMacID, DbType.String));
            Result = dbhelper.ExecuteNonQuery(PurchaseQueries.UpdateWorkOrder, paramCollection, CommandType.StoredProcedure);
            if (Result > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<WorkOrderEntities> GetWorkOrder(int AuthorizationStatusId)
        {
            List<WorkOrderEntities> WO = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("AuthorizationStatusId", AuthorizationStatusId, DbType.Int32);
                DataTable dtWO = dbHelper.ExecuteDataTable(PurchaseQueries.GetWorkOrder, param, CommandType.StoredProcedure);

                WO = dtWO.AsEnumerable()
                            .Select(row => new WorkOrderEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("IndentDate")).ToString("dd-MMM-yyyy"),
                                QuotationDeadLine = row.Field<DateTime>("QuotationDeadLine"),
                                strQuotationDeadLine = Convert.ToDateTime(row.Field<DateTime>("QuotationDeadLine")).ToString("dd-MMM-yyyy"),
                                strIndentNature = row.Field<string>("strIndentNature"),
                                DepartmentId = row.Field<int?>("DepartmentId"),
                                BudgetId = row.Field<int?>("BudgetId"),
                                Remarks = row.Field<string>("Remarks"),
                                IndentNature = row.Field<string>("IndentNature"),
                                DeliveryStartDate = row.Field<DateTime?>("DeliveryStartDate"),
                                DeliveryEndDate = row.Field<DateTime?>("DeliveryEndDate"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                ItemCategory = row.Field<string>("ItemCategory"),
                                Status = row.Field<string>("Status"),
                                AuthorisedRemarks = row.Field<string>("AuthorisedRemarks"),
                                AuthorisedBy = row.Field<int?>("AuthorisedBy"),
                                AuthorisedOn = row.Field<DateTime?>("AuthorisedOn"),
                                ProcurementTypeID = row.Field<int?>("ProcurementTypeID"),
                                Storeid = row.Field<int?>("Storeid"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchID = row.Field<int?>("BranchID"),
                                CreatedBy = row.Field<string>("UserName"),
                                RequiredDate = row.Field<string>("RequiredDate"),
                                InsertedByName = row.Field<string>("InsertedByName"),
                                UpdatedByName = row.Field<string>("UpdatedByName"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName"),
                                CancelledByName = row.Field<string>("CancelledByName")
                            }).ToList();
            }
            return WO;
        }

        public WorkOrderEntities GetWorkOrderById(int Id)
        {
            WorkOrderEntities WO = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("IndentId", Id, DbType.Int32);
                DataTable dtWO = dbHelper.ExecuteDataTable(PurchaseQueries.GetWorkOrderById, param, CommandType.StoredProcedure);

                WO = dtWO.AsEnumerable()
                            .Select(row => new WorkOrderEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime?>("IndentDate")).ToString("dd-MMM-yyyy"),
                                QuotationDeadLine = row.Field<DateTime?>("QuotationDeadLine"),
                                strQuotationDeadLine = Convert.ToDateTime(row.Field<DateTime?>("QuotationDeadLine")).ToString("dd-MMM-yyyy"),
                                DepartmentId = row.Field<int?>("DepartmentId"),
                                BudgetId = row.Field<int?>("BudgetId"),
                                Remarks = row.Field<string>("Remarks"),
                                IndentNature = row.Field<string>("IndentNature"),
                                DeliveryStartDate = row.Field<DateTime?>("DeliveryStartDate"),
                                DeliveryEndDate = row.Field<DateTime?>("DeliveryEndDate"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                Status = row.Field<string>("Status"),
                                AuthorisedRemarks = row.Field<string>("AuthorisedRemarks"),
                                AuthorisedBy = row.Field<int?>("AuthorisedBy"),
                                AuthorisedOn = row.Field<DateTime?>("AuthorisedOn"),
                                strAuthorisedOn = Convert.ToDateTime(row.Field<DateTime?>("AuthorisedOn")).ToString("dd-MMM-yyyy"),
                                ProcurementTypeID = row.Field<int?>("ProcurementTypeID"),
                                Storeid = row.Field<int?>("Storeid"),
                                StoreName = row.Field<string>("StoreName"),
                                VendorID = row.Field<int?>("VendorID"),
                                VendorName = row.Field<string>("VendorName"),
                                BranchID = row.Field<int?>("BranchID"),
                                RequiredDate = row.Field<string>("RequiredDate"),
                                VendorAddress = row.Field<string>("VendorAddress"),
                                ShippingAddress = row.Field<string>("ShippingAddress"),
                                GrandTotal= row.Field<double?>("GrandTotal"),
                                Preparedby = row.Field<string>("InsertedBy"),
                                AuthorizedByName = row.Field<string>("AuthorizedByName")

                            }).FirstOrDefault();
            }
            return WO;
        }

        public bool AuthCancelWorkOrder(WorkOrderEntities entity, DBHelper dbhelper)
        {
            int iResult = 0;
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("IndentId", entity.IndentId, DbType.Int32));
            paramCollection.Add(new DBParameter("VendorID", entity.VendorID, DbType.Int32));
            paramCollection.Add(new DBParameter("VendorName", entity.VendorName, DbType.String));
            paramCollection.Add(new DBParameter("AuthorisedRemarks", entity.AuthorisedRemarks, DbType.String));
            paramCollection.Add(new DBParameter("ShippingAddress", entity.ShippingAddress, DbType.String));
            paramCollection.Add(new DBParameter("AuthorizationStatusId", entity.AuthorizationStatusId, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedBy", entity.InsertedBy, DbType.Int32));
            paramCollection.Add(new DBParameter("AuthorisedOn", entity.InsertedON, DbType.DateTime));
            paramCollection.Add(new DBParameter("Amount", entity.Amount, DbType.Double));
            paramCollection.Add(new DBParameter("Tax", entity.Tax, DbType.Double));
            paramCollection.Add(new DBParameter("GrandTotal", entity.GrandTotal, DbType.Double));
            iResult = dbhelper.ExecuteNonQuery(PurchaseQueries.AuthCancelWorkOrder, paramCollection, CommandType.StoredProcedure);
            if (iResult > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<WorkOrderEntities> WOforReport()
        {
            List<WorkOrderEntities> WO = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtWO = dbHelper.ExecuteDataTable(PurchaseQueries.GetWOforReport, CommandType.StoredProcedure);
                WO = dtWO.AsEnumerable()
                            .Select(row => new WorkOrderEntities
                            {
                                IndentId = row.Field<int>("IndentId"),
                                IndentNumber = row.Field<string>("IndentNumber"),
                                IndentDate = row.Field<DateTime>("IndentDate"),
                                strIndentDate = Convert.ToDateTime(row.Field<DateTime>("IndentDate")).ToString("dd-MMM-yyyy"),
                                QuotationDeadLine = row.Field<DateTime>("QuotationDeadLine"),
                                strQuotationDeadLine = Convert.ToDateTime(row.Field<DateTime>("QuotationDeadLine")).ToString("dd-MMM-yyyy"),
                                DepartmentId = row.Field<int?>("DepartmentId"),
                                BudgetId = row.Field<int?>("BudgetId"),
                                Remarks = row.Field<string>("Remarks"),
                                IndentNature = row.Field<string>("IndentNature"),
                                DeliveryStartDate = row.Field<DateTime?>("DeliveryStartDate"),
                                DeliveryEndDate = row.Field<DateTime?>("DeliveryEndDate"),
                                ItemCategoryId = row.Field<int?>("ItemCategoryId"),
                                Status = row.Field<string>("Status"),
                                AuthorisedRemarks = row.Field<string>("AuthorisedRemarks"),
                                AuthorisedBy = row.Field<int?>("AuthorisedBy"),
                                AuthorisedOn = row.Field<DateTime?>("AuthorisedOn"),
                                ProcurementTypeID = row.Field<int?>("ProcurementTypeID"),
                                Storeid = row.Field<int?>("Storeid"),
                                StoreName = row.Field<string>("StoreName"),
                                BranchID = row.Field<int?>("BranchID"),
                                CreatedBy = row.Field<string>("UserName"),
                                RequiredDate = row.Field<string>("RequiredDate"),
                            }).ToList();
            }
            return WO;
        }

    }
}
