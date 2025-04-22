using BISERPBusinessLayer.Entities.DashBoard;
using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IDashBoardRepository
    {
        StoreDashBoardEntity GetItemSummary();
		IEnumerable<StoreDSBDStockSummaryEntity> GetStoreDSBDStockSummary(int byValue = 1);
        IEnumerable<StoreDSBDGuardIssueSummaryEntity> GetStoreDSBDGuardIssueSummary();
        IEnumerable<DashBoardCountSummuryModel> GetDashBoardCountSummury(string FromDate, string ToDate, int? ProjectID);
    }
}
