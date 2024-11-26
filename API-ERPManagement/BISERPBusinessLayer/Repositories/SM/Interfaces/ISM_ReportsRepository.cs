using BISERPBusinessLayer.Entities.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Interfaces
{
    public interface ISM_ReportsRepository
    {
        IEnumerable<EnquiryRegisterReportEntities> GetEnquiryRegisterRpt(int? financialYearID);
        IEnumerable<OrderBookReportEntities> GetOrderBookRpt(int? financialYearID);
        IEnumerable<MonthlyTargetReportEntities> GetResourceWiseMonthlyStatusRpt(int? financialYearID);
        IEnumerable<SectorWiseSalesReportEntities> GetSectorWiseSalesRpt(int? financialYearID);
        IEnumerable<LocationWiseSalesReportEntities> GetLocationWiseSalesRpt(int? financialYearID);
        IEnumerable<ProductWiseSalesReportEntities> GetProductWiseSalesRpt(int? financialYearID);
        WorkOrderRptEntities GetWorkOrderReport(int? orderBookID);
        List<WORptPaymentTermDetails> GetOrderBookPaymentTerms(int? orderBookID);
        List<WORptDeliveryTerm> GetOrderBookDeliveryTerms(int? orderBookID);
    }
}
