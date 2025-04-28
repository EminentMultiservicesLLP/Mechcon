using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IGRNRepository
    {
        GRNEntity GetByID(int Id);
        IEnumerable<GRNEntity> GetAllList(int StoreId, int SuppId, DateTime fromdate, DateTime todate);
        IEnumerable<GRNEntity> GRNForAuthorization(int StoreId);
        GRNEntity CreateNewEntry(GRNEntity entity, DBHelper dbHelper);
        bool UpdateGRNEntry(GRNEntity entity, DBHelper dbHelper);
        bool UpdateEntry(GRNEntity entity, DBHelper dbHelper);
        bool AuthCancelGRN(GRNEntity entity, DBHelper dbHelper);
        IEnumerable<GRNEntity> GRNforReport();
        IEnumerable<GRNSummarizedDetailRptModel> GetGRNSummarizedDetailsRpt(DateTime Fromdate, DateTime todate);
    }
}
