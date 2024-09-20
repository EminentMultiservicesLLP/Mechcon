using BISERPBusinessLayer.Entities.SM;
using BISERPBusinessLayer.QueryCollection.SM;
using BISERPBusinessLayer.Repositories.SM.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.SM.Classes
{
    public class OrderBookRepository : IOrderBookRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(OrderBookRepository));

        public IEnumerable<EnquiryRegisterEntities> GetEnqForOrderBook(int UserID)
        {
            List<EnquiryRegisterEntities> record = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserID", UserID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetEnqForOrderBook, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new EnquiryRegisterEntities
                            {
                                EnquiryID = row.Field<int>("EnquiryID"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                strEnquiryDate = Convert.ToDateTime(row.Field<DateTime>("EnquiryDate")).ToString("dd-MMMM-yyyy"),
                                ClientName = row.Field<string>("ClientName"),
                                AllocatedToName = row.Field<string>("AllocatedToName")
                            }).ToList();
            }
            return record;
        }

        public OfferDetailEntities GetFinalOffer(int EnquiryID)
        {
            OfferDetailEntities record = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("EnquiryID", EnquiryID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetFinalOffer, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new OfferDetailEntities
                            {
                                OfferRegisterID = row.Field<int>("OfferRegisterID"),
                                OfferID = row.Field<int>("OfferID"),
                                OfferNo = row.Field<string>("OfferNo"),
                                strOfferDate = Convert.ToDateTime(row.Field<DateTime>("OfferDate")).ToString("dd-MMMM-yyyy"),
                                OfferRemark = row.Field<string>("OfferRemark"),
                                POBaseValue = row.Field<double>("POBaseValue"),
                                GSTAmount = row.Field<double>("GSTAmount"),
                                strCustRespDate = row.Field<string>("strCustRespDate"),
                                CustRemark = row.Field<string>("CustRemark"),
                                Deactive = row.Field<bool>("Deactive")
                            }).FirstOrDefault();
            }
            return record;
        }

        public OrderBookEntities SaveOrderBook(OrderBookEntities entity)
        {
            var ProjectTCList = entity.ProjectTCList != null ? Commons.ToXML(entity.ProjectTCList) : null;
            var PaymentTermList = entity.PaymentTermList != null ? Commons.ToXML(entity.PaymentTermList) : null;
            var DeliveryTermList = entity.DeliveryTermList != null ? Commons.ToXML(entity.DeliveryTermList) : null;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("OrderBookID", entity.OrderBookID, DbType.Int32));
                    paramCollection.Add(new DBParameter("EnquiryID", entity.EnquiryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("OfferID", entity.OfferID, DbType.Int32));
                    paramCollection.Add(new DBParameter("PONo", entity.PONo, DbType.String));
                    paramCollection.Add(new DBParameter("PODate", entity.PODate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("POBaseValue", entity.POBaseValue, DbType.Double));
                    paramCollection.Add(new DBParameter("PIAdvSubmitDate", entity.PIAdvSubmitDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("ABGSubmitDate", entity.ABGSubmitDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("PIABGAdvSubmitDate", entity.PIABGAdvSubmitDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("IncoTermID", entity.IncoTermID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProjectTCList", ProjectTCList, DbType.Xml));
                    paramCollection.Add(new DBParameter("PaymentTermList", PaymentTermList, DbType.Xml));
                    paramCollection.Add(new DBParameter("DeliveryTermList", DeliveryTermList, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedON", entity.InsertedON, DbType.DateTime));
                    paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("UpdatedOn", entity.UpdatedOn, DbType.DateTime));
                    paramCollection.Add(new DBParameter("UpdatedMacID", entity.UpdatedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedMacName", entity.UpdatedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.UpdatedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("Deactive", entity.Deactive, DbType.Boolean));

                    paramCollection.Add(new DBParameter("ProjectID", entity.ProjectID, DbType.Int32, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("ProjectCode", entity.ProjectCode, DbType.String, 50, ParameterDirection.Output));

                    var parameterList = dbHelper.ExecuteNonQueryForOutParameter(MarketingQueries.SaveOrderBook, paramCollection, CommandType.StoredProcedure);

                    entity.ProjectID = Convert.ToInt32(parameterList["ProjectID"]);
                    entity.ProjectCode = parameterList["ProjectCode"].ToString();

                    dbHelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in SaveOrderBook method: " + Environment.NewLine + ex.StackTrace);
                }
            }
            return entity;
        }

        public IEnumerable<OrderBookEntities> GetOrderBook(int UserID)
        {
            List<OrderBookEntities> orderBooks = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserID", UserID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOrderBook, param, CommandType.StoredProcedure);


                orderBooks = dt.AsEnumerable()
                               .Select(row => new OrderBookEntities
                               {
                                   EnquiryID = row.Field<int?>("EnquiryID"),
                                   EnquiryNo = row.Field<string>("EnquiryNo"),
                                   strEnquiryDate = Convert.ToDateTime(row.Field<DateTime>("EnquiryDate")).ToString("dd-MMMM-yyyy"),
                                   ClientName = row.Field<string>("ClientName"),
                                   OrderBookID = row.Field<int>("OrderBookID"),
                                   OfferID = row.Field<int?>("OfferID"),
                                   PONo = row.Field<string>("PONo"),
                                   strPODate = row.Field<string>("strPODate"),
                                   POBaseValue = row.Field<double?>("POBaseValue"),
                                   strPIAdvSubmitDate = row.Field<string>("strPIAdvSubmitDate"),
                                   strABGSubmitDate = row.Field<string>("strABGSubmitDate"),
                                   strPIABGAdvSubmitDate = row.Field<string>("strPIABGAdvSubmitDate"),
                                   IncoTermID = row.Field<int>("IncoTermID"),
                                   Deactive = row.Field<bool>("Deactive"),
                                   ProjectID = row.Field<int>("ProjectID"),
                                   ProjectCode = row.Field<string>("ProjectCode"),
                               }).ToList();
            }
            return orderBooks;
        }

        public IEnumerable<ProjectTCDetails> GetOrderBookProjectTC(int orderBookID)
        {
            List<ProjectTCDetails> record = null;

            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("OrderBookID", orderBookID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOrderBookProjectTC, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new ProjectTCDetails
                            {
                                ProjectTCID = row.Field<int>("ProjectTCID"),
                                ProjectTCCode = row.Field<string>("ProjectTCCode"),
                                ProjectTCDesc = row.Field<string>("ProjectTCDesc"),
                                State = row.Field<bool>("State")
                            }).ToList();
            }

            return record;
        }

        public IEnumerable<PaymentTermDetails> GetOrderBookPaymentTerms(int orderBookID)
        {
            List<PaymentTermDetails> record = null;

            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("OrderBookID", orderBookID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOrderBookPaymentTerms, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new PaymentTermDetails
                            {
                                PayTermID = row.Field<int>("PayTermID"),
                                PaymentTermCode = row.Field<string>("PaymentTermCode"),
                                PaymentTermDesc = row.Field<string>("PaymentTermDesc"),
                                State = row.Field<bool>("State")
                            }).ToList();
            }

            return record;
        }

        public IEnumerable<DeliveryTermDetails> GetOrderBookDeliveryTerms(int orderBookID)
        {
            List<DeliveryTermDetails> record = null;

            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("OrderBookID", orderBookID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOrderBookDeliveryTerms, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new DeliveryTermDetails
                            {
                                DelTermID = row.Field<int>("DelTermID"),
                                DeliveryTermCode = row.Field<string>("DeliveryTermCode"),
                                DeliveryTermDesc = row.Field<string>("DeliveryTermDesc"),
                                State = row.Field<bool>("State")
                            }).ToList();
            }

            return record;
        }

        public IEnumerable<IncoTermEntities> GetIncoTerm()
        {
            List<IncoTermEntities> incoTerms = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetIncoTerm, CommandType.StoredProcedure);
                incoTerms = dt.AsEnumerable()
                           .Select(row => new IncoTermEntities
                           {
                               IncoTermID = row.Field<int>("IncoTermID"),
                               IncoTermCode = row.Field<string>("IncoTermCode"),
                               IncoTermName = row.Field<string>("IncoTermName")
                           }).ToList();
            }
            return incoTerms;
        }
    }
}
