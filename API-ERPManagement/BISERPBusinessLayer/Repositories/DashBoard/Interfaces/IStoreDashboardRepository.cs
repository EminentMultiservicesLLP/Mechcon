using BISERPBusinessLayer.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.DashBoard.Interfaces
{
   public interface IStoreDashboardRepository
    {
       List<DashboardRequestCounts> GetGRNCounts(DateTime? fromDt, DateTime? toDt);
       List<DashboardRequestCounts> GetMIndentCount(DateTime? fromDt, DateTime? toDt);
       List<DashboardRequestCounts> GetMIssueCount(DateTime? fromDt, DateTime? toDt);
       List<DashboardRequestCounts> GetVIssueCount(DateTime? fromDt, DateTime? toDt);
       List<DashboardRequestCounts> GetMReturnCount(DateTime? fromDt, DateTime? toDt,int type);
       List<DashboardRequestCounts> GetStockAdjustmentCount(DateTime? fromDt, DateTime? toDt, int type);
       List<DashboardRequestCounts> SendMailStatusOfDashBoard(DateTime? fromDt, DateTime? toDt);
    }
}
