using BISERPBusinessLayer.Repositories.Purchase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System.Data;
using BISERPBusinessLayer.QueryCollection.Purchase;

namespace BISERPBusinessLayer.Repositories.Purchase.Classes
{
    public class PurchaseReportsRepository : IPurchaseReportsRepository
    {
        public IEnumerable<ProjectBudgetConclusion> grnVSpoitemcomparison(int StoreId, int SupplierId, DateTime? FromDate, DateTime? ToDate)
        {
            List<ProjectBudgetConclusion> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("StoreId", StoreId, DbType.Int32));
                paramCollection.Add(new DBParameter("SupplierId", SupplierId, DbType.Int32));
                paramCollection.Add(new DBParameter("FromDate", FromDate, DbType.DateTime));
                paramCollection.Add(new DBParameter("ToDate", ToDate, DbType.DateTime));
                DataTable dtUnits = dbHelper.ExecuteDataTable(PurchaseQueries.grnVSpoitemcomparison, paramCollection, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new ProjectBudgetConclusion
                            {
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ProjectName = row.Field<string>("ProjectName"),
                                ProjectId = row.Field<int>("ProjectId"),
                                PONo = row.Field<string>("PONo"),
                                SupplierName = row.Field<string>("SupplierName"),
                                VendorName = row.Field<string>("VendorName"),
                                ItemCategory = row.Field<string>("ItemCategory"),
                                PoDate = row.Field<string>("PoDate"),
                                BasicAmount = row.Field<double>("BasicAmount"),
                                GSTAmount = row.Field<double>("GSTAmount"),
                                GrandTotal = row.Field<double>("GrandTotal"),
                                PoQty = row.Field<double>("PoQty"),
                                GRNQty = row.Field<double>("GRNQty"),
                                PendingQty = row.Field<double>("PendingQty"),
                                ItemName = row.Field<string>("ITEMNAME"),
                                ItemDescription = row.Field<string>("DescriptiveName"),
                            }).ToList();

            }
            return units;
        }
    }
}
