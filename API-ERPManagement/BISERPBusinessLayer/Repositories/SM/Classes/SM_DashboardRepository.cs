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
    public class SM_DashboardRepository : ISM_DashboardRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(SM_DashboardRepository));

        #region Location Wise
        #region Received Enquiries
        public IEnumerable<BarChartModel> GetReceivedEnquiries(string ToDate)
        {
            List<BarChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedEnquiries, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new BarChartModel
                            {
                                MonthNo = row.Field<int>("MonthNo"),
                                MonthName = row.Field<string>("MonthName"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSourceEnquiries(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSourceEnquiries, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetEngineerEnquiries(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetEngineerEnquiries, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSectorEnquiries(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSectorEnquiries, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                            }).ToList();
            }
            return data;
        }
        #endregion Received Enquiries

        #region Submitted Offers
        public IEnumerable<BarChartModel> GetSubmittedOffers(string ToDate)
        {
            List<BarChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSubmittedOffers, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new BarChartModel
                            {
                                MonthNo = row.Field<int>("MonthNo"),
                                MonthName = row.Field<string>("MonthName"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                                TotalValue = row.Field< decimal?>("TotalValue"),
                                DomesticValue = row.Field< decimal?>("DomesticValue"),
                                ExportValue = row.Field< decimal?>("ExportValue"),
                                SEZValue = row.Field< decimal?>("SEZValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSubmittedSourceOffers(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSubmittedSourceOffers, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                                TotalValue = row.Field< decimal?>("TotalValue"),
                                DomesticValue = row.Field< decimal?>("DomesticValue"),
                                ExportValue = row.Field< decimal?>("ExportValue"),
                                SEZValue = row.Field< decimal?>("SEZValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSubmittedEngineerOffers(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSubmittedEngineerOffers, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                                TotalValue = row.Field< decimal?>("TotalValue"),
                                DomesticValue = row.Field< decimal?>("DomesticValue"),
                                ExportValue = row.Field< decimal?>("ExportValue"),
                                SEZValue = row.Field< decimal?>("SEZValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSubmittedSectorOffers(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSubmittedSectorOffers, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                                TotalValue = row.Field< decimal?>("TotalValue"),
                                DomesticValue = row.Field< decimal?>("DomesticValue"),
                                ExportValue = row.Field< decimal?>("ExportValue"),
                                SEZValue = row.Field< decimal?>("SEZValue"),
                            }).ToList();
            }
            return data;
        }
        #endregion Submitted Offers

        #region Received Offers
        public IEnumerable<BarChartModel> GetReceivedOffers(string ToDate)
        {
            List<BarChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedOffers, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new BarChartModel
                            {
                                MonthNo = row.Field<int>("MonthNo"),
                                MonthName = row.Field<string>("MonthName"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                                TotalValue = row.Field< decimal?>("TotalValue"),
                                DomesticValue = row.Field< decimal?>("DomesticValue"),
                                ExportValue = row.Field< decimal?>("ExportValue"),
                                SEZValue = row.Field< decimal?>("SEZValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetReceivedSourceOffers(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedSourceOffers, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                                TotalValue = row.Field< decimal?>("TotalValue"),
                                DomesticValue = row.Field< decimal?>("DomesticValue"),
                                ExportValue = row.Field< decimal?>("ExportValue"),
                                SEZValue = row.Field< decimal?>("SEZValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetReceivedEngineerOffers(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedEngineerOffers, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                                TotalValue = row.Field< decimal?>("TotalValue"),
                                DomesticValue = row.Field< decimal?>("DomesticValue"),
                                ExportValue = row.Field< decimal?>("ExportValue"),
                                SEZValue = row.Field< decimal?>("SEZValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetReceivedSectorOffers(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedSectorOffers, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                DomesticCount = row.Field<int>("DomesticCount"),
                                ExportCount = row.Field<int>("ExportCount"),
                                SEZCount = row.Field<int>("SEZCount"),
                                TotalValue = row.Field< decimal?>("TotalValue"),
                                DomesticValue = row.Field< decimal?>("DomesticValue"),
                                ExportValue = row.Field< decimal?>("ExportValue"),
                                SEZValue = row.Field< decimal?>("SEZValue"),
                            }).ToList();
            }
            return data;
        }
        #endregion Received Offers
        #endregion Location  Wise



        #region Product Wise
        #region Received Enquiries
        public IEnumerable<BarChartModel> GetReceivedEnquiriesPW(string ToDate)
        {
            List<BarChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedEnquiriesPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new BarChartModel
                            {
                                MonthNo = row.Field<int>("MonthNo"),
                                MonthName = row.Field<string>("MonthName"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSourceEnquiriesPW(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSourceEnquiriesPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetEngineerEnquiriesPW(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetEngineerEnquiriesPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSectorEnquiriesPW(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSectorEnquiriesPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                            }).ToList();
            }
            return data;
        }
        #endregion Received Enquiries

        #region Submitted Offers
        public IEnumerable<BarChartModel> GetSubmittedOffersPW(string ToDate)
        {
            List<BarChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSubmittedOffersPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new BarChartModel
                            {
                                MonthNo = row.Field<int>("MonthNo"),
                                MonthName = row.Field<string>("MonthName"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                                TotalValue = row.Field<decimal?>("TotalValue"),
                                SystemsValue = row.Field<decimal?>("SystemsValue"),
                                ProductsValue = row.Field<decimal?>("ProductsValue"),
                                SparesValue = row.Field<decimal?>("SparesValue"),
                                ServicesValue = row.Field<decimal?>("ServicesValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSubmittedSourceOffersPW(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSubmittedSourceOffersPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                                TotalValue = row.Field<decimal?>("TotalValue"),
                                SystemsValue = row.Field<decimal?>("SystemsValue"),
                                ProductsValue = row.Field<decimal?>("ProductsValue"),
                                SparesValue = row.Field<decimal?>("SparesValue"),
                                ServicesValue = row.Field<decimal?>("ServicesValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSubmittedEngineerOffersPW(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSubmittedEngineerOffersPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                                TotalValue = row.Field<decimal?>("TotalValue"),
                                SystemsValue = row.Field<decimal?>("SystemsValue"),
                                ProductsValue = row.Field<decimal?>("ProductsValue"),
                                SparesValue = row.Field<decimal?>("SparesValue"),
                                ServicesValue = row.Field<decimal?>("ServicesValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetSubmittedSectorOffersPW(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetSubmittedSectorOffersPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                                TotalValue = row.Field<decimal?>("TotalValue"),
                                SystemsValue = row.Field<decimal?>("SystemsValue"),
                                ProductsValue = row.Field<decimal?>("ProductsValue"),
                                SparesValue = row.Field<decimal?>("SparesValue"),
                                ServicesValue = row.Field<decimal?>("ServicesValue"),
                            }).ToList();
            }
            return data;
        }
        #endregion Submitted Offers

        #region Received Offers
        public IEnumerable<BarChartModel> GetReceivedOffersPW(string ToDate)
        {
            List<BarChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedOffersPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new BarChartModel
                            {
                                MonthNo = row.Field<int>("MonthNo"),
                                MonthName = row.Field<string>("MonthName"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                                TotalValue = row.Field<decimal?>("TotalValue"),
                                SystemsValue = row.Field<decimal?>("SystemsValue"),
                                ProductsValue = row.Field<decimal?>("ProductsValue"),
                                SparesValue = row.Field<decimal?>("SparesValue"),
                                ServicesValue = row.Field<decimal?>("ServicesValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetReceivedSourceOffersPW(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedSourceOffersPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                                TotalValue = row.Field<decimal?>("TotalValue"),
                                SystemsValue = row.Field<decimal?>("SystemsValue"),
                                ProductsValue = row.Field<decimal?>("ProductsValue"),
                                SparesValue = row.Field<decimal?>("SparesValue"),
                                ServicesValue = row.Field<decimal?>("ServicesValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetReceivedEngineerOffersPW(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedEngineerOffersPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                                TotalValue = row.Field<decimal?>("TotalValue"),
                                SystemsValue = row.Field<decimal?>("SystemsValue"),
                                ProductsValue = row.Field<decimal?>("ProductsValue"),
                                SparesValue = row.Field<decimal?>("SparesValue"),
                                ServicesValue = row.Field<decimal?>("ServicesValue"),
                            }).ToList();
            }
            return data;
        }
        public IEnumerable<PieChartModel> GetReceivedSectorOffersPW(string FromDate, string ToDate)
        {
            List<PieChartModel> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("FromDate", FromDate, DbType.String));
                param.Add(new DBParameter("ToDate", ToDate, DbType.String));

                DataTable dt = dbHelper.ExecuteDataTable(SM_DashboardQueries.GetReceivedSectorOffersPW, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new PieChartModel
                            {
                                ID = row.Field<int>("ID"),
                                Name = row.Field<string>("Name"),
                                TotalCount = row.Field<int>("TotalCount"),
                                SystemsCount = row.Field<int>("SystemsCount"),
                                ProductsCount = row.Field<int>("ProductsCount"),
                                SparesCount = row.Field<int>("SparesCount"),
                                ServicesCount = row.Field<int>("ServicesCount"),
                                TotalValue = row.Field<decimal?>("TotalValue"),
                                SystemsValue = row.Field<decimal?>("SystemsValue"),
                                ProductsValue = row.Field<decimal?>("ProductsValue"),
                                SparesValue = row.Field<decimal?>("SparesValue"),
                                ServicesValue = row.Field<decimal?>("ServicesValue"),
                            }).ToList();
            }
            return data;
        }
        #endregion Received Offers
        #endregion Product  Wise
    }
}
