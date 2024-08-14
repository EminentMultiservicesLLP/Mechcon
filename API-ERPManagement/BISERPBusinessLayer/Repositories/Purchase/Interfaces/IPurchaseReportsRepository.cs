using BISERPBusinessLayer.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
   public interface IPurchaseReportsRepository
    {
        IEnumerable<ProjectBudgetConclusion> grnVSpoitemcomparison(int StoreId,int SupplierId, DateTime? FromDate, DateTime? ToDate);
    }
}
