using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.SM
{
    class SM_DashboardQueries
    {
        #region Location Wise
        //Received Enquiries
        public const string GetReceivedEnquiries = "sp_SM_GetReceivedEnquiries";
        public const string GetSourceEnquiries = "sp_SM_GetSourceEnquiries";
        public const string GetEngineerEnquiries = "sp_SM_GetEngineerEnquiries";
        public const string GetSectorEnquiries = "sp_SM_GetSectorEnquiries";

        //Submitted Offers
        public const string GetSubmittedOffers = "sp_SM_GetSubmittedOffers";
        public const string GetSubmittedSourceOffers = "sp_SM_GetSubmittedSourceOffers";
        public const string GetSubmittedEngineerOffers = "sp_SM_GetSubmittedEngineerOffers";
        public const string GetSubmittedSectorOffers = "sp_SM_GetSubmittedSectorOffers";

        //Received Offers
        public const string GetReceivedOffers = "sp_SM_GetReceivedOffers";
        public const string GetReceivedSourceOffers = "sp_SM_GetReceivedSourceOffers";
        public const string GetReceivedEngineerOffers = "sp_SM_GetReceivedEngineerOffers";
        public const string GetReceivedSectorOffers = "sp_SM_GetReceivedSectorOffers";
        #endregion Location Wise



        #region Product Wise
        //Received Enquiries
        public const string GetReceivedEnquiriesPW = "sp_SM_GetReceivedEnquiriesPW";
        public const string GetSourceEnquiriesPW = "sp_SM_GetSourceEnquiriesPW";
        public const string GetEngineerEnquiriesPW = "sp_SM_GetEngineerEnquiriesPW";
        public const string GetSectorEnquiriesPW = "sp_SM_GetSectorEnquiriesPW";

        //Submitted Offers
        public const string GetSubmittedOffersPW = "sp_SM_GetSubmittedOffersPW";
        public const string GetSubmittedSourceOffersPW = "sp_SM_GetSubmittedSourceOffersPW";
        public const string GetSubmittedEngineerOffersPW = "sp_SM_GetSubmittedEngineerOffersPW";
        public const string GetSubmittedSectorOffersPW = "sp_SM_GetSubmittedSectorOffersPW";

        //Received Offers
        public const string GetReceivedOffersPW = "sp_SM_GetReceivedOffersPW";
        public const string GetReceivedSourceOffersPW = "sp_SM_GetReceivedSourceOffersPW";
        public const string GetReceivedEngineerOffersPW = "sp_SM_GetReceivedEngineerOffersPW";
        public const string GetReceivedSectorOffersPW = "sp_SM_GetReceivedSectorOffersPW";
        #endregion Product Wise

    }
}
