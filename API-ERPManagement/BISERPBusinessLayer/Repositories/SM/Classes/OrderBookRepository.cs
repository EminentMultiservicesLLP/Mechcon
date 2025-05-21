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
                                strEnquiryDate = Convert.ToDateTime(row.Field<DateTime>("EnquiryDate")).ToString("dd-MMM-yyyy"),
                                ClientName = row.Field<string>("ClientName"),
                                AllocatedToName = row.Field<string>("AllocatedToName")
                            }).ToList();
            }
            return record;
        }

        public IEnumerable<ConsigneeEntities> GetConsignee(int? enquiryId)
        {
            List<ConsigneeEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("EnquiryID", enquiryId, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetConsignee, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new ConsigneeEntities
                            {
                                ConsigneeID = row.Field<int?>("ConsigneeID"),
                                Consignee = row.Field<string>("Consignee"),
                                Address = row.Field<string>("Address")
                            }).ToList();
            }
            return data;
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
                                strOfferDate = Convert.ToDateTime(row.Field<DateTime>("OfferDate")).ToString("dd-MMM-yyyy"),
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
            var OBOtherDetails = entity.OBOtherDetails != null ? Commons.ToXML(entity.OBOtherDetails) : null;
            var ProjectTCList = entity.ProjectTCList != null ? Commons.ToXML(entity.ProjectTCList) : null;
            var PaymentTermList = entity.PaymentTermList != null ? Commons.ToXML(entity.PaymentTermList) : null;
            var DeliveryTermList = entity.DeliveryTermList != null ? Commons.ToXML(entity.DeliveryTermList) : null;
            var OtherTermList = entity.OtherTermList != null ? Commons.ToXML(entity.OtherTermList) : null;
            var BasisTermList = entity.BasisTermList != null ? Commons.ToXML(entity.BasisTermList) : null;
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
                    paramCollection.Add(new DBParameter("BudgetValue", entity.BudgetValue, DbType.Double));
                    paramCollection.Add(new DBParameter("MaterialValue", entity.MaterialValue, DbType.Double));
                    paramCollection.Add(new DBParameter("ConversionValue", entity.ConversionValue, DbType.Double));
                    paramCollection.Add(new DBParameter("TransValue", entity.TransValue, DbType.Double));
                    paramCollection.Add(new DBParameter("ECValue", entity.ECValue, DbType.Double));
                    paramCollection.Add(new DBParameter("PIAdvSubmitDate", entity.PIAdvSubmitDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("ABGSubmitDate", entity.ABGSubmitDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("PIABGAdvSubmitDate", entity.PIABGAdvSubmitDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("ProjectDescription", entity.ProjectDescription, DbType.String));
                    paramCollection.Add(new DBParameter("MaterialOfConstruction", entity.MaterialOfConstruction, DbType.String));
                    paramCollection.Add(new DBParameter("AreaOfInstallation", entity.AreaOfInstallation, DbType.String));
                    paramCollection.Add(new DBParameter("ConsigneeID", entity.ConsigneeID, DbType.String));
                    paramCollection.Add(new DBParameter("TechnicalSpecification", entity.TechnicalSpecification, DbType.String));
                    paramCollection.Add(new DBParameter("ScopeOfSupplyID", entity.ScopeOfSupplyID, DbType.Int16));
                    paramCollection.Add(new DBParameter("Packaging", entity.Packaging, DbType.String));
                    paramCollection.Add(new DBParameter("Insurance", entity.Insurance, DbType.String));
                    paramCollection.Add(new DBParameter("Supervision", entity.Supervision, DbType.String));
                    paramCollection.Add(new DBParameter("LDCharges", entity.LDCharges, DbType.Single));
                    paramCollection.Add(new DBParameter("ContactAtSite", entity.ContactAtSite, DbType.String));
                    paramCollection.Add(new DBParameter("ContactAtPurchase", entity.ContactAtPurchase, DbType.String));
                    paramCollection.Add(new DBParameter("DeliveryDate", entity.DeliveryDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("Transport", entity.Transport, DbType.String));
                    paramCollection.Add(new DBParameter("DispatchDate1", entity.DispatchDate1, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DispatchDate2", entity.DispatchDate2, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DispatchDate3", entity.DispatchDate3, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DispatchDate4", entity.DispatchDate4, DbType.DateTime));
                    paramCollection.Add(new DBParameter("DispatchDate5", entity.DispatchDate5, DbType.DateTime));
                    paramCollection.Add(new DBParameter("BriefTechnicalSpecification", entity.BriefTechnicalSpecification, DbType.String));
                    paramCollection.Add(new DBParameter("Quantity", entity.Quantity, DbType.Int16));
                    paramCollection.Add(new DBParameter("InstAndComm", entity.InstAndComm, DbType.String));
                    paramCollection.Add(new DBParameter("GuaranteeType", entity.GuaranteeType, DbType.String));
                    paramCollection.Add(new DBParameter("SBG", entity.SBG, DbType.Double));
                    paramCollection.Add(new DBParameter("ABG", entity.ABG, DbType.Double));
                    paramCollection.Add(new DBParameter("PBG", entity.PBG, DbType.Double));
                    paramCollection.Add(new DBParameter("AdditionalContact", entity.AdditionalContact, DbType.String));
                    paramCollection.Add(new DBParameter("OBOtherDetails", OBOtherDetails, DbType.Xml));
                    paramCollection.Add(new DBParameter("ProjectTCList", ProjectTCList, DbType.Xml));
                    paramCollection.Add(new DBParameter("PaymentTermList", PaymentTermList, DbType.Xml));
                    paramCollection.Add(new DBParameter("DeliveryTermList", DeliveryTermList, DbType.Xml));
                    paramCollection.Add(new DBParameter("OtherTermList", OtherTermList, DbType.Xml));
                    paramCollection.Add(new DBParameter("BasisTermList", BasisTermList, DbType.Xml));
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
                                   strEnquiryDate = Convert.ToDateTime(row.Field<DateTime>("EnquiryDate")).ToString("dd-MMM-yyyy"),
                                   ClientName = row.Field<string>("ClientName"),
                                   OrderBookID = row.Field<int>("OrderBookID"),
                                   OrderBookNo = row.Field<string>("OrderBookNo"),
                                   strWODate = row.Field<string>("strWODate"),
                                   OfferID = row.Field<int?>("OfferID"),
                                   PONo = row.Field<string>("PONo"),
                                   strPODate = row.Field<string>("strPODate"),
                                   POBaseValue = row.Field<double?>("POBaseValue"),
                                   BudgetValue = row.Field<decimal?>("BudgetValue"),
                                   MaterialValue = row.Field<decimal?>("MaterialValue"),
                                   ConversionValue = row.Field<decimal?>("ConversionValue"),
                                   TransValue = row.Field<decimal?>("TransValue"),
                                   ECValue = row.Field<decimal?>("ECValue"),
                                   strPIAdvSubmitDate = row.Field<string>("strPIAdvSubmitDate"),
                                   strABGSubmitDate = row.Field<string>("strABGSubmitDate"),
                                   strPIABGAdvSubmitDate = row.Field<string>("strPIABGAdvSubmitDate"),
                                   ProjectID = row.Field<int>("ProjectID"),
                                   ProjectCode = row.Field<string>("ProjectCode"),
                                   ProjectDescription = row.Field<string>("ProjectDescription"),
                                   MaterialOfConstruction = row.Field<string>("MaterialOfConstruction"),
                                   AreaOfInstallation = row.Field<string>("AreaOfInstallation"),
                                   ConsigneeID = row.Field<int?>("ConsigneeID"),
                                   TechnicalSpecification = row.Field<string>("TechnicalSpecification"),
                                   ScopeOfSupplyID = row.Field<int?>("ScopeOfSupplyID"),
                                   ScopeOfSupplyName = row.Field<string>("ScopeOfSupplyName"),
                                   Packaging = row.Field<string>("Packaging"),
                                   Insurance = row.Field<string>("Insurance"),
                                   Supervision = row.Field<string>("Supervision"),
                                   LDCharges = row.Field<double?>("LDCharges"),
                                   ContactAtSite = row.Field<string>("ContactAtSite"),
                                   ContactAtPurchase = row.Field<string>("ContactAtPurchase"),
                                   strDeliveryDate = row.Field<string>("strDeliveryDate"),
                                   Transport = row.Field<string>("Transport"),
                                   strDispatchDate1 = row.Field<string>("strDispatchDate1"),
                                   strDispatchDate2 = row.Field<string>("strDispatchDate2"),
                                   strDispatchDate3 = row.Field<string>("strDispatchDate3"),
                                   strDispatchDate4 = row.Field<string>("strDispatchDate4"),
                                   strDispatchDate5 = row.Field<string>("strDispatchDate5"),
                                   BriefTechnicalSpecification = row.Field<string>("BriefTechnicalSpecification"),
                                   Quantity = row.Field<int?>("Quantity"),
                                   InstAndComm = row.Field<string>("InstAndComm"),
                                   GuaranteeType = row.Field<string>("GuaranteeType"),
                                   SBG = row.Field<decimal?>("SBG"),
                                   ABG = row.Field<decimal?>("ABG"),
                                   PBG = row.Field<decimal?>("PBG"),
                                   AdditionalContact = row.Field<string>("AdditionalContact"),
                                   Deactive = row.Field<bool>("Deactive"),
                               }).ToList();
            }
            return orderBooks;
        }

        public IEnumerable<OrderBookOtherDetail> GetOBOtherDetails(int OrderBookID)
        {
            List<OrderBookOtherDetail> others = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("OrderBookID", OrderBookID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOBOtherDetails, param, CommandType.StoredProcedure);

                others = dt.AsEnumerable()
                            .Select(row => new OrderBookOtherDetail
                            {
                                Name = row.Field<string>("Name"),
                                Value = row.Field<string>("Value")
                            }).ToList();
            }
            return others;
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


        public IEnumerable<OtherTermDetails> GetOrderBookOtherTerms(int orderBookID)
        {
            List<OtherTermDetails> record = null;

            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("OrderBookID", orderBookID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOrderBookOtherTerms, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new OtherTermDetails
                            {
                                OtherTermID = row.Field<int>("OtherTermID"),
                                OthersTermCode = row.Field<string>("OthersTermCode"),
                                OthersTermDesc = row.Field<string>("OthersTermDesc"),
                                State = row.Field<bool>("State")
                            }).ToList();
            }

            return record;
        }


        public IEnumerable<BasisTermDetails> GetOrderBookBasisTerms(int orderBookID)
        {
            List<BasisTermDetails> record = null;

            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("OrderBookID", orderBookID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOrderBookBasisTerms, param, CommandType.StoredProcedure);

                record = dt.AsEnumerable()
                            .Select(row => new BasisTermDetails
                            {
                                BasisId = row.Field<int>("BasisId"),
                                BasisCode = row.Field<string>("BasisCode"),
                                BasisDesc = row.Field<string>("BasisDesc"),
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

        public IEnumerable<OrderBookEntities> GetOrderBookForRpt(int UserID)
        {
            List<OrderBookEntities> orderBooks = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserID", UserID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOrderBookForRpt, param, CommandType.StoredProcedure);


                orderBooks = dt.AsEnumerable()
                               .Select(row => new OrderBookEntities
                               {
                                   EnquiryID = row.Field<int?>("EnquiryID"),
                                   EnquiryNo = row.Field<string>("EnquiryNo"),
                                   strEnquiryDate = Convert.ToDateTime(row.Field<DateTime>("EnquiryDate")).ToString("dd-MMM-yyyy"),
                                   ClientName = row.Field<string>("ClientName"),
                                   OrderBookID = row.Field<int>("OrderBookID"),
                                   OrderBookNo = row.Field<string>("OrderBookNo"),
                                   OfferID = row.Field<int?>("OfferID"),
                                   PONo = row.Field<string>("PONo"),
                                   strPODate = row.Field<string>("strPODate"),
                                   POBaseValue = row.Field<double?>("POBaseValue"),
                                   BudgetValue = row.Field<decimal?>("BudgetValue"),
                                   MaterialValue = row.Field<decimal?>("MaterialValue"),
                                   ConversionValue = row.Field<decimal?>("ConversionValue"),
                                   TransValue = row.Field<decimal?>("TransValue"),
                                   ECValue = row.Field<decimal?>("ECValue"),
                                   strPIAdvSubmitDate = row.Field<string>("strPIAdvSubmitDate"),
                                   strABGSubmitDate = row.Field<string>("strABGSubmitDate"),
                                   strPIABGAdvSubmitDate = row.Field<string>("strPIABGAdvSubmitDate"),
                                   ProjectID = row.Field<int>("ProjectID"),
                                   ProjectCode = row.Field<string>("ProjectCode"),
                                   ProjectDescription = row.Field<string>("ProjectDescription"),
                                   MaterialOfConstruction = row.Field<string>("MaterialOfConstruction"),
                                   AreaOfInstallation = row.Field<string>("AreaOfInstallation"),
                                   ConsigneeID = row.Field<int?>("ConsigneeID"),
                                   TechnicalSpecification = row.Field<string>("TechnicalSpecification"),
                                   ScopeOfSupplyID = row.Field<int?>("ScopeOfSupplyID"),
                                   ScopeOfSupplyName = row.Field<string>("ScopeOfSupplyName"),
                                   Packaging = row.Field<string>("Packaging"),
                                   Insurance = row.Field<string>("Insurance"),
                                   Supervision = row.Field<string>("Supervision"),
                                   LDCharges = row.Field<double?>("LDCharges"),
                                   ContactAtSite = row.Field<string>("ContactAtSite"),
                                   ContactAtPurchase = row.Field<string>("ContactAtPurchase"),
                                   strDeliveryDate = row.Field<string>("strDeliveryDate"),
                                   Transport = row.Field<string>("Transport"),
                                   strDispatchDate1 = row.Field<string>("strDispatchDate1"),
                                   strDispatchDate2 = row.Field<string>("strDispatchDate2"),
                                   strDispatchDate3 = row.Field<string>("strDispatchDate3"),
                                   strDispatchDate4 = row.Field<string>("strDispatchDate4"),
                                   strDispatchDate5 = row.Field<string>("strDispatchDate5"),
                                   BriefTechnicalSpecification = row.Field<string>("BriefTechnicalSpecification"),
                                   Quantity = row.Field<int?>("Quantity"),
                                   InstAndComm = row.Field<string>("InstAndComm"),
                                   GuaranteeType = row.Field<string>("GuaranteeType"),
                                   SBG = row.Field<decimal?>("SBG"),
                                   ABG = row.Field<decimal?>("ABG"),
                                   PBG = row.Field<decimal?>("PBG"),
                                   AdditionalContact = row.Field<string>("AdditionalContact"),
                                   Deactive = row.Field<bool>("Deactive"),
                               }).ToList();
            }
            return orderBooks;
        }

    }
}
