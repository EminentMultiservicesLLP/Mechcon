using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.QueryCollection.Masters;
using BISERPBusinessLayer.QueryCollection.Purchase;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Store;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class BillCreationRepository : IBillCreationRepository
    {

        public IEnumerable<BillEntity> GetSupplierBill(int SupplierID, DateTime fromdate, DateTime todate)
        {

            IEnumerable<BillEntity> SupplierBill = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("SupplierID", SupplierID, DbType.Int32));
                paramCollection.Add(new DBParameter("Fromdate", fromdate, DbType.DateTime));
                paramCollection.Add(new DBParameter("Todate", todate, DbType.DateTime));

                DataTable dtCancelmi = dbHelper.ExecuteDataTable(StoreQuery.GetAllSupplierBill, paramCollection, CommandType.StoredProcedure);

                SupplierBill = dtCancelmi.AsEnumerable()
                            .Select(row => new BillEntity
                            {
                                BillId = row.Field<int>("BillId"),
                                BillNo = row.Field<string>("BillNo"),
                                BillDate = row.Field<DateTime>("BillDate"),
                                BillAmount = row.Field<double>("BillAmount"),
                                PartyPayable = row.Field<double>("PartyPayable"),
                                SupplierName = row.Field<string>("SupplierName"),
                                strBillDate = Convert.ToDateTime(row.Field<DateTime>("BillDate")).ToString("dd-MMM-yyyy"),
                            }).ToList();
            }
            return SupplierBill;
        }

        public IEnumerable<GRNEntity> GetSupplierBill(int SupplierID)
        {

            IEnumerable<GRNEntity> SupplierBill = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();
                paramCollection.Add(new DBParameter("SupplierID", SupplierID, DbType.Int32));
                DataTable dtCancelmi = dbHelper.ExecuteDataTable(StoreQuery.GetAllSupplierBill, paramCollection, CommandType.StoredProcedure);

                SupplierBill = dtCancelmi.AsEnumerable()
                            .Select(row => new GRNEntity
                            {
                                SupplierID = row.Field<int>("SupplierID"),
                                ID = row.Field<int>("ID"),
                                GRNNo = row.Field<string>("GRNNo"),
                                TotalAmount = row.Field<double?>("TotalAmount")
                            }).ToList();
            }
            return SupplierBill;
        }
      //  public IEnumerable<GRNDetailEntity> GetSupplierBillDT(int GRNID)
        public IEnumerable<GRNDetailEntity> GetSupplierBillDT(int BillId)
        {

            IEnumerable<GRNDetailEntity> SupplierBill = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection paramCollection = new DBParameterCollection();

                paramCollection.Add(new DBParameter("BillId", BillId, DbType.Int32));

                DataTable dtCancelmi = dbHelper.ExecuteDataTable(StoreQuery.GetAllSupplierBillDetails, paramCollection, CommandType.StoredProcedure);

                SupplierBill = dtCancelmi.AsEnumerable()
                            .Select(row => new GRNDetailEntity
                            {
                                ItemID = row.Field<int>("ItemID"),
                                ItemName = row.Field<string>("ItemName"),
                                Rate = row.Field<double?>("Rate"),
                                Qty = row.Field<double?>("Qty"),
                                Amount = row.Field<double?>("Amount")
                            }).ToList();
            }
            return SupplierBill;
        }
    }

}
