using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IBillCreationRepository
    {
        IEnumerable<BillEntity> GetSupplierBill(int SupplierID, DateTime fromdate, DateTime todate);
        IEnumerable<GRNEntity> GetSupplierBill(int SupplierID);
       // IEnumerable<GRNDetailEntity> GetSupplierBillDT(int GRNID);
       // IEnumerable<BillEntity> GetSupplierBill(int SupplierID, DateTime fromdate, DateTime todate);
        IEnumerable<GRNDetailEntity> GetSupplierBillDT(int BillId);

    }
}
