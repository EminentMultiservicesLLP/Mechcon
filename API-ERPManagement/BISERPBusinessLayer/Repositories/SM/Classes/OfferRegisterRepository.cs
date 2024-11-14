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
    public class OfferRegisterRepository : IOfferRegisterRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(OfferRegisterRepository));

        public IEnumerable<EnquiryRegisterEntities> GetEnqForOffer(int UserID)
        {
            List<EnquiryRegisterEntities> enquires = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserID", UserID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetEnqForOffer, param, CommandType.StoredProcedure);

                enquires = dt.AsEnumerable()
                            .Select(row => new EnquiryRegisterEntities
                            {
                                EnquiryID = row.Field<int>("EnquiryID"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                strEnquiryDate = row.Field<DateTime?>("EnquiryDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("EnquiryDate")).ToString("dd-MMMM-yyyy") : string.Empty,
                                ClientName = row.Field<string>("ClientName"),
                                AllocatedToName = row.Field<string>("AllocatedToName"),
                            }).ToList();
            }
            return enquires;
        }

        public OfferDetailEntities GetOfferID(int EnquiryID)
        {
            OfferDetailEntities enquires = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("EnquiryID", EnquiryID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOfferID, param, CommandType.StoredProcedure);

                enquires = dt.AsEnumerable()
                            .Select(row => new OfferDetailEntities
                            {
                                OfferID = row.Field<int>("OfferID"),
                                OfferNo = row.Field<string>("OfferNo")
                            }).FirstOrDefault();
            }
            return enquires;
        }

        public OfferRegisterEntities SaveOffer(OfferRegisterEntities entity)
        {
            var offerDetails = Commons.ToXML(entity.OfferDetails);

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("OfferRegisterID", entity.OfferRegisterID, DbType.Int32));
                    paramCollection.Add(new DBParameter("EnquiryID", entity.EnquiryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("QRDate", entity.QRDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("OfferRegisterComment", entity.OfferRegisterComment, DbType.String));
                    paramCollection.Add(new DBParameter("ProductID", entity.ProductID, DbType.Int32));
                    paramCollection.Add(new DBParameter("StatusID", entity.StatusID, DbType.Int32));
                    paramCollection.Add(new DBParameter("IncoTermID", entity.IncoTermID, DbType.Int32));
                    paramCollection.Add(new DBParameter("OfferDetails", offerDetails, DbType.Xml));
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

                    var parameterList = dbHelper.ExecuteScalar(MarketingQueries.SaveOffer, paramCollection, CommandType.StoredProcedure);
                    entity.OfferRegisterID = Convert.ToInt32(parameterList.ToString());

                    dbHelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in SaveOffer method: " + Environment.NewLine + ex.StackTrace);
                }
            }
            return entity;
        }

        public IEnumerable<OfferRegisterEntities> GetOffer(int UserID)
        {
            List<OfferRegisterEntities> enquires = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("UserID", UserID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOffer, param, CommandType.StoredProcedure);

                enquires = dt.AsEnumerable()
                            .Select(row => new OfferRegisterEntities
                            {
                                OfferRegisterID = row.Field<int>("OfferRegisterID"),
                                strQRDate = row.Field<DateTime?>("QRDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("QRDate")).ToString("dd-MMMM-yyyy"): string.Empty,
                                OfferRegisterComment = row.Field<string>("OfferRegisterComment"),
                                IncoTermID = row.Field<int?>("IncoTermID"),
                                StatusID = row.Field<int?>("StatusID"),
                                EnquiryID = row.Field<int?>("EnquiryID"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                strEnquiryDate = row.Field<DateTime?>("EnquiryDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("EnquiryDate")).ToString("dd-MMMM-yyyy") : string.Empty,
                                ClientName = row.Field<string>("ClientName")
                            }).ToList();
            }
            return enquires;
        }

        public IEnumerable<OfferDetailEntities> GetOfferDetails(int OfferRegisterID)
        {
            List<OfferDetailEntities> offers = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("OfferRegisterID", OfferRegisterID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetOfferDetails, param, CommandType.StoredProcedure);

                offers = dt.AsEnumerable()
                            .Select(row => new OfferDetailEntities
                            {
                                OfferID = row.Field<int>("OfferID"),
                                OfferNo = row.Field<string>("OfferNo"),
                                strOfferDate = row.Field<DateTime?>("OfferDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("OfferDate")).ToString("dd-MMMM-yyyy") : string.Empty,
                                OfferRemark = row.Field<string>("OfferRemark"),
                                POBaseValue = row.Field<double>("POBaseValue"),
                                GSTAmount = row.Field<double>("GSTAmount"),
                                strCustRespDate = row.Field<string>("strCustRespDate"),
                                CustRemark = row.Field<string>("CustRemark"),
                                Deactive = row.Field<bool>("Deactive")
                            }).ToList();
            }
            return offers;
        }

    }
}
