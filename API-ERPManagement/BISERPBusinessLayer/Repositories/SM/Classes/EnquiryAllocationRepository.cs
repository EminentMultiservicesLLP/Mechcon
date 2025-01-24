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
    public class EnquiryAllocationRepository : IEnquiryAllocationRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(EnquiryAllocationRepository));

        public IEnumerable<EnquiryRegisterEntities> GetEnqForAllocation(int? statusID)
        {
            List<EnquiryRegisterEntities> enquires = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StatusID", statusID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetEnqForAllocation, param, CommandType.StoredProcedure);

                enquires = dt.AsEnumerable()
                            .Select(row => new EnquiryRegisterEntities
                            {
                                EnquiryID = row.Field<int>("EnquiryID"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                strEnquiryDate = Convert.ToDateTime(row.Field<DateTime>("EnquiryDate")).ToString("dd-MMM-yyyy"),
                                ClientName = row.Field<string>("ClientName")
                            }).ToList();
            }
            return enquires;
        }

        public EnquiryAllocationEntities SaveAllocation(EnquiryAllocationEntities entity)
        {
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                TryCatch.Run(() =>
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("AllocationID", entity.AllocationID, DbType.Int32));
                    paramCollection.Add(new DBParameter("EnquiryID", entity.EnquiryID, DbType.Int32));
                    paramCollection.Add(new DBParameter("FeasibilityStudy", entity.FeasibilityStudy, DbType.Boolean));
                    paramCollection.Add(new DBParameter("AllocatedTo", entity.AllocatedTo, DbType.Int32));
                    paramCollection.Add(new DBParameter("AllocationDate", entity.AllocationDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("QRDate", entity.QRDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("Comment", entity.Comment, DbType.String));
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
                    var parameterList = dbHelper.ExecuteScalar(MarketingQueries.SaveAllocation, paramCollection, CommandType.StoredProcedure);
                    entity.AllocationID = Convert.ToInt32(parameterList.ToString());
                    dbHelper.CommitTransaction(transaction);
                }).IfNotNull(ex =>
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in StoreMasterRepository method of storemaster request Repository : parameter :" + Environment.NewLine + ex.StackTrace);
                });
            }
            return entity;
        }

        public IEnumerable<EnquiryAllocationEntities> GetAllocation()
        {
            List<EnquiryAllocationEntities> enquires = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(MarketingQueries.GetAllocation, CommandType.StoredProcedure);

                enquires = dt.AsEnumerable()
                            .Select(row => new EnquiryAllocationEntities
                            {
                                AllocationID = row.Field<int>("AllocationID"),
                                EnquiryID = row.Field<int?>("EnquiryID"),
                                EnquiryNo = row.Field<string>("EnquiryNo"),
                                FeasibilityStudy = row.Field<Boolean>("FeasibilityStudy"),
                                AllocatedTo = row.Field<int?>("AllocatedTo"),
                                AllocatedToName = row.Field<string>("AllocatedToName"),
                                strAllocationDate = Convert.ToDateTime(row.Field<DateTime>("AllocationDate")).ToString("dd-MMM-yyyy"),
                                strQRDate = row.Field<DateTime?>("QRDate") != null ? Convert.ToDateTime(row.Field<DateTime?>("QRDate")).ToString("dd-MMM-yyyy"): string.Empty,
                                Comment = row.Field<string>("Comment")
                            }).ToList();
            }
            return enquires;
        }

    }
}
