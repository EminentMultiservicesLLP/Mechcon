using BISERPBusinessLayer.Entities.SM;
using BISERPBusinessLayer.QueryCollection.SM;
using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Classes
{
    public class SM_ReportsRepository : ISM_ReportsRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(SM_ReportsRepository));

        public IEnumerable<EnquiryRegisterReportEntities> GetEnquiryRegisterRpt(int? financialYearID)
        {
            List<EnquiryRegisterReportEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("FinancialYearID", financialYearID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetEnquiryRegisterRpt, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new EnquiryRegisterReportEntities
                            {
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                strEnquiryDate = row.Field<string>("strEnquiryDate"),
                                EnquiryYear = row.Field<int>("EnquiryYear"),
                                ClientName = row.Field<string>("ClientName"),
                                Location = row.Field<string>("Location"),
                                Source = row.Field<string>("Source"),
                                Type = row.Field<string>("Type"),
                                Sector = row.Field<string>("Sector"),
                                Product = row.Field<string>("Product"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactNo = row.Field<string>("ContactNo"),
                                EmailID = row.Field<string>("EmailID"),
                                POBaseValue = row.Field<decimal>("POBaseValue"),
                                strOfferDate = row.Field<string>("strOfferDate"),
                                OfferRemark = row.Field<string>("OfferRemark"),
                                Status = row.Field<string>("Status"),
                                AllocatedTo = row.Field<string>("AllocatedTo")
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<OrderBookReportEntities> GetOrderBookRpt(int? financialYearID)
        {
            List<OrderBookReportEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("financialYearID", financialYearID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOrderBookRpt, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new OrderBookReportEntities
                            {
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                strEnquiryDate = row.Field<string>("strEnquiryDate"),
                                EnquiryYear = row.Field<int>("EnquiryYear"),
                                ClientName = row.Field<string>("ClientName"),
                                Location = row.Field<string>("Location"),
                                Source = row.Field<string>("Source"),
                                Product = row.Field<string>("Product"),
                                Type = row.Field<string>("Type"),
                                Sector = row.Field<string>("Sector"),
                                ContactPerson = row.Field<string>("ContactPerson"),
                                ContactNo = row.Field<string>("ContactNo"),
                                EmailID = row.Field<string>("EmailID"),
                                Month = row.Field<string>("Month"),
                                strOrderBookDate = row.Field<string>("strOrderBookDate"),
                                strPODate = row.Field<string>("strPODate"),
                                PONo = row.Field<string>("PONo"),
                                POBaseValue = row.Field<decimal>("POBaseValue"),
                                strPIAdvSubmitDate = row.Field<string>("strPIAdvSubmitDate"),
                                strABGSubmitDate = row.Field<string>("strABGSubmitDate"),
                                strPIABGAdvSubmitDate = row.Field<string>("strPIABGAdvSubmitDate"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                Status = row.Field<string>("Status"),
                                AllocatedTo = row.Field<string>("AllocatedTo")
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<MonthlyTargetReportEntities> GetResourceWiseMonthlyStatusRpt(int? financialYearID)
        {
            List<MonthlyTargetReportEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("financialYearID", financialYearID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetResourceWiseMonthlyStatusRpt, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new MonthlyTargetReportEntities
                            {
                                Resource = row.Field<string>("Resource"),
                                Month = row.Field<string>("Month"),
                                TargetValue = row.Field<decimal>("TargetValue"),
                                OfferValue = row.Field<decimal>("OfferValue"),
                                WonValue = row.Field<decimal>("WonValue")
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<SectorWiseSalesReportEntities> GetSectorWiseSalesRpt(int? financialYearID)
        {
            List<SectorWiseSalesReportEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("financialYearID", financialYearID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetSectorWiseSalesRpt, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new SectorWiseSalesReportEntities
                            {
                                Sector = row.Field<string>("Sector"),
                                WonValue = row.Field<decimal>("WonValue"),
                                Percentage = row.Field<decimal>("Percentage")
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<LocationWiseSalesReportEntities> GetLocationWiseSalesRpt(int? financialYearID)
        {
            List<LocationWiseSalesReportEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("financialYearID", financialYearID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetLocationWiseSalesRpt, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new LocationWiseSalesReportEntities
                            {
                                Location = row.Field<string>("Location"),
                                WonValue = row.Field<decimal>("WonValue"),
                                Percentage = row.Field<decimal>("Percentage")
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<ProductWiseSalesReportEntities> GetProductWiseSalesRpt(int? financialYearID)
        {
            List<ProductWiseSalesReportEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("financialYearID", financialYearID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetProductWiseSalesRpt, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new ProductWiseSalesReportEntities
                            {
                                Product = row.Field<string>("Product"),
                                WonValue = row.Field<decimal>("WonValue"),
                                Percentage = row.Field<decimal>("Percentage")
                            }).ToList();
            }
            return data;
        }
    }
}
