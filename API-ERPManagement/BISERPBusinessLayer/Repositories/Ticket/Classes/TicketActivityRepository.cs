using BISERPBusinessLayer.Entities.Ticket;
using BISERPBusinessLayer.QueryCollection.Ticket;
using BISERPBusinessLayer.Repositories.Ticket.Interfaces;
using BISERPCommon;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Ticket.Classes
{
    public class TicketActivityRepository : ITicketActivityRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(TicketActivityRepository));

        public IEnumerable<TicketRegisterEntities> GetTicketForActivity(int UserID, int? TicketType)
        {
            List<TicketRegisterEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("UserID", UserID, DbType.Int32));
                param.Add(new DBParameter("TicketType", TicketType, DbType.Int32));

                DataTable dt = dbHelper.ExecuteDataTable(TicketActivityQueries.GetTicketForActivity, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TicketRegisterEntities
                            {
                                TicketID = row.Field<int>("TicketID"),
                                TicketNo = row.Field<string>("TicketNo"),
                                strTicketDate = row.Field<DateTime?>("TicketDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                                Subject = row.Field<string>("Subject"),
                                Description = row.Field<string>("Description"),
                                PriorityID = row.Field<int?>("PriorityID"),
                                Priority = row.Field<string>("Priority"),
                                ClientID = row.Field<int?>("ClientID"),
                                ClientName = row.Field<string>("ClientName"),
                                ProjectID = row.Field<int?>("ProjectID"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                AllocatedTo = row.Field<int?>("AllocatedTo"),
                                AllocatedToName = row.Field<string>("AllocatedToName"),
                                StatusID = row.Field<int?>("StatusID"),
                            }).ToList();
            }
            return data;
        }

        public TicketActivityEntities GetActivityID(int TicketID , int InsertedBy)
        {
            TicketActivityEntities enquires = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();

                param.Add(new DBParameter("TicketID", TicketID, DbType.Int32));
                param.Add(new DBParameter("InsertedBy", InsertedBy, DbType.Int32));

                DataTable dt = dbHelper.ExecuteDataTable(TicketActivityQueries.GetActivityID, param, CommandType.StoredProcedure);

                enquires = dt.AsEnumerable()
                            .Select(row => new TicketActivityEntities
                            {
                                ActivityID = row.Field<int>("ActivityID")
                            }).FirstOrDefault();
            }
            return enquires;
        }

        public ActivityListEntities SaveActivity(ActivityListEntities entity)
        {
            var activityList = Commons.ToXML(entity.ActivityList);

            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();
                    paramCollection.Add(new DBParameter("ActivityList", activityList, DbType.Xml));
                    paramCollection.Add(new DBParameter("InsertedBy", entity.InsertedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("InsertedOn", entity.InsertedOn, DbType.DateTime));
                    paramCollection.Add(new DBParameter("InsertedMacID", entity.InsertedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedMacName", entity.InsertedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("InsertedIPAddress", entity.InsertedIPAddress, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedBy", entity.UpdatedBy, DbType.Int32));
                    paramCollection.Add(new DBParameter("UpdatedOn", entity.UpdatedOn, DbType.DateTime));
                    paramCollection.Add(new DBParameter("UpdatedMacID", entity.UpdatedMacID, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedMacName", entity.UpdatedMacName, DbType.String));
                    paramCollection.Add(new DBParameter("UpdatedIPAddress", entity.UpdatedIPAddress, DbType.String));

                    var parameterList = dbHelper.ExecuteScalar(TicketActivityQueries.SaveActivity, paramCollection, CommandType.StoredProcedure);

                    dbHelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in SaveActivity method: " + Environment.NewLine + ex.StackTrace);
                }
            }
            return entity;
        }

        public IEnumerable<TicketActivityEntities> GetActivity(int? ticketID)
        {
            List<TicketActivityEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameterCollection param = new DBParameterCollection();
                param.Add(new DBParameter("TicketID", ticketID, DbType.Int32));

                DataTable dt = dbHelper.ExecuteDataTable(TicketActivityQueries.GetActivity, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TicketActivityEntities
                            {
                                TicketID = row.Field<int>("TicketID"),
                                ActivityID = row.Field<int>("ActivityID"),
                                strActivityDate = row.Field<DateTime?>("ActivityDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                                Activity = row.Field<string>("Activity"),
                                strResponseDate = row.Field<DateTime?>("ResponseDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                                Response = row.Field<string>("Response"),
                                StatusID = row.Field<int?>("StatusID"),
                                Status = row.Field<string>("Status"),
                                Deactive = row.Field<bool?>("Deactive"),
                            }).ToList();
            }
            return data;
        }

    }
}
