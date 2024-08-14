using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Entities.Store;
using BISERPBusinessLayer.Entities.Transport;
using BISERPBusinessLayer.QueryCollection.Store;
using BISERPBusinessLayer.QueryCollection.Transport;
using BISERPBusinessLayer.Repositories.Store.Interfaces;
using BISERPBusinessLayer.Repositories.Transport.Interfaces;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Classes
{
    public class SupplierBillListpository : ISupplierBillListRepository
    {
        public IEnumerable<SupplierMasterEntities> GetAllSupplierBillList()
        {
            List<SupplierMasterEntities> units = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dtUnits = dbHelper.ExecuteDataTable(StoreQuery.GetallSupplierListGrn, CommandType.StoredProcedure);
                units = dtUnits.AsEnumerable()
                            .Select(row => new SupplierMasterEntities
                            {
                                ID = row.Field<int>("supplierid"),
                                Code = row.Field<string>("Code"),
                                Name = row.Field<string>("Name"),
                                EligableForAdv = row.Field<bool>("EligableForAdv"),
                                Deactive = row.Field<bool>("Deactive"),

                            }).ToList();
                if (units == null || units.Count == 0)
                    units.Add(new SupplierMasterEntities
                    {
                        ID = 0,
                        Code = "",
                        Name = "",
                        Deactive = false,
                        EligableForAdv = false
                    });
            }
            return units;
        }

       public IEnumerable<BillEntity> GetAllSupplierBillListdt(int SupplierId)
        {
            List<BillEntity> bill = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("supplierid", SupplierId, DbType.Int32);
                DataTable dtgrn = dbHelper.ExecuteDataTable(StoreQuery.GetallSupplierListGrnDT, param, CommandType.StoredProcedure);
                bill = dtgrn.AsEnumerable()
                            .Select(row => new BillEntity
                            {
                                BillId = row.Field<int>("BillId"),
                                BillNo = row.Field<string>("BillNo"),
                                BillDate = row.Field<DateTime>("BillDate"),
                                BillAmount = row.Field<double>("BillAmount"),
                                SuppilerId = row.Field<int>("SuppilerId"),
                                strBillDate = Convert.ToDateTime(row.Field<DateTime>("BillDate")).ToString("dd-MMMM-yyyy"),
                            }).ToList();
            }
            return bill;
        }
      
  
    
    }
}
