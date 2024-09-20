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
    public class EnquiryRegisterRepository : IEnquiryRegisterRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(EnquiryRegisterRepository));
        public IEnumerable<SourceEntities> GetSources()
        {
            List<SourceEntities> sources = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetSources, CommandType.StoredProcedure);
                sources = dt.AsEnumerable()
                            .Select(row => new SourceEntities
                            {
                                SourceID = row.Field<int>("SourceID"),
                                Source = row.Field<string>("Source")
                            }).ToList();
            }
            return sources;
        }

        public IEnumerable<ProductEntities> GetProducts()
        {
            List<ProductEntities> products = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetProducts, CommandType.StoredProcedure);
                products = dt.AsEnumerable()
                            .Select(row => new ProductEntities
                            {
                                ProductID = row.Field<int>("ProductID"),
                                Product = row.Field<string>("Product")
                            }).ToList();
            }
            return products;
        }

        public IEnumerable<LocationEntities> GetLocations()
        {
            List<LocationEntities> locations = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetLocations, CommandType.StoredProcedure);
                locations = dt.AsEnumerable()
                              .Select(row => new LocationEntities
                              {
                                  LocationID = row.Field<int>("LocationID"),
                                  Location = row.Field<string>("Location")
                              }).ToList();
            }
            return locations;
        }

        public IEnumerable<TypeEntities> GetTypes()
        {
            List<TypeEntities> types = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetTypes, CommandType.StoredProcedure);
                types = dt.AsEnumerable()
                          .Select(row => new TypeEntities
                          {
                              TypeID = row.Field<int>("TypeID"),
                              Type = row.Field<string>("Type")
                          }).ToList();
            }
            return types;
        }

        public IEnumerable<SectorEntities> GetSectors()
        {
            List<SectorEntities> sectors = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetSectors, CommandType.StoredProcedure);
                sectors = dt.AsEnumerable()
                          .Select(row => new SectorEntities
                          {
                              SectorID = row.Field<int>("SectorID"),
                              Sector = row.Field<string>("Sector")
                          }).ToList();
            }
            return sectors;
        }

        public IEnumerable<ZoneEntities> GetZones()
        {
            List<ZoneEntities> zones = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetZones, CommandType.StoredProcedure);
                zones = dt.AsEnumerable()
                          .Select(row => new ZoneEntities
                          {
                              ZoneID = row.Field<int>("ZoneID"),
                              Zone = row.Field<string>("Zone")
                          }).ToList();
            }
            return zones;
        }

        public IEnumerable<StatusEntities> GetStatus()
        {
            List<StatusEntities> status = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetStatus, CommandType.StoredProcedure);
                status = dt.AsEnumerable()
                           .Select(row => new StatusEntities
                           {
                               StatusID = row.Field<int>("StatusID"),
                               Status = row.Field<string>("Status")
                           }).ToList();
            }
            return status;
        }

        public EnquiryRegisterEntities SaveEnquiry(EnquiryRegisterEntities entity)
        {
            int NewEnquiryID = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("EnquiryID", entity.EnquiryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("EnquiryDate", entity.EnquiryDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("SourceID", entity.SourceID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ProductID", entity.ProductID, DbType.Int32));
                    paramCollection.Add(new DBParameter("LocationID", entity.LocationID, DbType.Int32));
                    paramCollection.Add(new DBParameter("CustomerMailMsg", entity.CustomerMailMsg, DbType.String));
                    paramCollection.Add(new DBParameter("ClientID", entity.ClientID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Place", entity.Place, DbType.String));
                    paramCollection.Add(new DBParameter("ContactPerson", entity.ContactPerson, DbType.String));
                    paramCollection.Add(new DBParameter("ContactNo", entity.ContactNo, DbType.String));
                    paramCollection.Add(new DBParameter("EmailID", entity.EmailID, DbType.String));
                    paramCollection.Add(new DBParameter("StatusID", entity.StatusID, DbType.Int32));
                    paramCollection.Add(new DBParameter("TypeID", entity.TypeID, DbType.Int32));
                    paramCollection.Add(new DBParameter("SectorID", entity.SectorID, DbType.Int32));
                    paramCollection.Add(new DBParameter("ZoneID", entity.ZoneID, DbType.Int32));
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

                    paramCollection.Add(new DBParameter("NewEnquiryID", NewEnquiryID, DbType.Int32, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("EnquiryNo", entity.EnquiryNo, DbType.String, 50, ParameterDirection.Output));

                    var parameterList = dbHelper.ExecuteNonQueryForOutParameter(MarketingQueries.SaveEnquiry, paramCollection, CommandType.StoredProcedure);

                    entity.EnquiryID = Convert.ToInt32(parameterList["NewEnquiryID"]);
                    entity.EnquiryNo = parameterList["EnquiryNo"].ToString();

                    dbHelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in EnquiryRegisterRepository method of SaveEnquiry request Repository : parameter :" + Environment.NewLine + ex.StackTrace);
                }
            }
            return entity;
        }

        public IEnumerable<EnquiryRegisterEntities> GetEnquiry(int? statusID)
        {
            List<EnquiryRegisterEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StatusID", statusID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetEnquiry,param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new EnquiryRegisterEntities
                            {
                                EnquiryID = row.Field<int>("EnquiryID"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                strEnquiryDate = Convert.ToDateTime(row.Field<DateTime>("EnquiryDate")).ToString("dd-MMMM-yyyy"),
                                ClientName = row.Field<string>("ClientName")
                            }).ToList();
            }
            return data;
        }

        public EnquiryRegisterEntities GetEnquiryDetails(int enquiryId)
        {
            EnquiryRegisterEntities data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("EnquiryId", enquiryId, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetEnquiryDetails, param, CommandType.StoredProcedure);
                data = dt.AsEnumerable()
                    .Select(row => new EnquiryRegisterEntities
                    {
                        EnquiryID = row.Field<int>("EnquiryID"),
                        EnquiryNo = row.Field<string>("EnquiryNo"),
                        EnquiryDate = row.Field<DateTime?>("EnquiryDate"),
                        strEnquiryDate = row.Field<DateTime?>("EnquiryDate")?.ToString("dd-MMMM-yyyy"),
                        SourceID = row.Field<int?>("SourceID"),
                        Source = row.Field<string>("Source"),
                        ProductID = row.Field<int?>("ProductID"),
                        Product = row.Field<string>("Product"),
                        LocationID = row.Field<int?>("LocationID"),
                        Location = row.Field<string>("Location"),
                        CustomerMailMsg = row.Field<string>("CustomerMailMsg"),
                        ClientID = row.Field<int>("ClientID"),
                        ClientName = row.Field<string>("ClientName"),
                        Place = row.Field<string>("Place"),
                        ContactPerson = row.Field<string>("ContactPerson"),
                        ContactNo = row.Field<string>("ContactNo"),
                        EmailID = row.Field<string>("EmailID"),
                        StatusID = row.Field<int?>("StatusID"),
                        Status = row.Field<string>("Status"),
                        TypeID = row.Field<int?>("TypeID"),
                        Type = row.Field<string>("Type"),
                        SectorID = row.Field<int?>("SectorID"),
                        Sector = row.Field<string>("Sector"),
                        ZoneID = row.Field<int?>("ZoneID"),
                        Zone = row.Field<string>("Zone"),
                        InsertedBy = row.Field<int?>("InsertedBy"),
                        UpdatedBy = row.Field<int?>("UpdatedBy"),
                    }).FirstOrDefault();
            }
            return data;
        }

        public IEnumerable<EnqRegFollowUpDetails> GetEnqRegFollowUp(int? enquiryId)
        {
            List<EnqRegFollowUpDetails> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("EnquiryID", enquiryId, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetEnqRegFollowUp, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new EnqRegFollowUpDetails
                            {
                                FollowUpID = row.Field<int>("FollowUpID"),
                                strFollowUpDate = row.Field<DateTime>("FollowUpDate") != null ? Convert.ToDateTime(row.Field<DateTime>("FollowUpDate")).ToString("dd-MMMM-yyyy"): string.Empty,
                                EnquiryID = row.Field<int>("EnquiryID"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                StatusID = row.Field<int>("StatusID"),
                                Status = row.Field<string>("Status"),
                                CustomerMailMsg = row.Field<string>("CustomerMailMsg"),
                            }).ToList();
            }
            return data;
        }

    }
}
