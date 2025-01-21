using BISERPBusinessLayer.Entities.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Interfaces
{
    public interface ISM_DashboardRepository
    {
        #region Location Wise
        #region Received Enquiries
        IEnumerable<BarChartModel> GetReceivedEnquiries(string ToDate);
        IEnumerable<PieChartModel> GetSourceEnquiries(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetEngineerEnquiries(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetSectorEnquiries(string FromDate, string ToDate);
        #endregion Received Enquiries

        #region Submitted Offers
        IEnumerable<BarChartModel> GetSubmittedOffers(string ToDate);
        IEnumerable<PieChartModel> GetSubmittedSourceOffers(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetSubmittedEngineerOffers(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetSubmittedSectorOffers(string FromDate, string ToDate);
        #endregion Submitted Offers

        #region Received Offers
        IEnumerable<BarChartModel> GetReceivedOffers(string ToDate);
        IEnumerable<PieChartModel> GetReceivedSourceOffers(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetReceivedEngineerOffers(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetReceivedSectorOffers(string FromDate, string ToDate);
        #endregion Received Offers
        #endregion Location  Wise



        #region Product Wise
        #region Received Enquiries
        IEnumerable<BarChartModel> GetReceivedEnquiriesPW(string ToDate);
        IEnumerable<PieChartModel> GetSourceEnquiriesPW(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetEngineerEnquiriesPW(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetSectorEnquiriesPW(string FromDate, string ToDate);
        #endregion Received Enquiries

        #region Submitted Offers
        IEnumerable<BarChartModel> GetSubmittedOffersPW(string ToDate);
        IEnumerable<PieChartModel> GetSubmittedSourceOffersPW(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetSubmittedEngineerOffersPW(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetSubmittedSectorOffersPW(string FromDate, string ToDate);
        #endregion Submitted Offers

        #region Received Offers
        IEnumerable<BarChartModel> GetReceivedOffersPW(string ToDate);
        IEnumerable<PieChartModel> GetReceivedSourceOffersPW(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetReceivedEngineerOffersPW(string FromDate, string ToDate);
        IEnumerable<PieChartModel> GetReceivedSectorOffersPW(string FromDate, string ToDate);
        #endregion Received Offers
        #endregion Product  Wise
    }
}
