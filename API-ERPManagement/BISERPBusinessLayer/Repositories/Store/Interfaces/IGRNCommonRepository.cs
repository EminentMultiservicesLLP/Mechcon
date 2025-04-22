using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IGRNCommonRepository
    {
        GRNEntity SaveGRNDetails(GRNEntity entity);
        bool UpdateGRN(GRNEntity entity);
        bool AuthCancelGRN(GRNEntity grnentity);

        IEnumerable<GRNEntity> GRNSummaryReport(DateTime fromdate, DateTime todate, int StoreId, int SupplierId);
        IEnumerable<GRNEntity> GRNDetailReport(DateTime fromdate, DateTime todate, int StoreId, int SupplierId, int GRNId, string ReportType);
        IEnumerable<GRNEntity> GRNCancelledReport(DateTime fromdate, DateTime todate);
        IEnumerable<GRNEntity> GRNItemWise(DateTime fromdate, DateTime todate, int ItemId);
        IEnumerable<GRNEntity> PendingGrnItemWise(int storeid);
    }
}
