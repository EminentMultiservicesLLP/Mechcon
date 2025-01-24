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
    public class TicketRegisterRepository : ITicketRegisterRepository
    {
        private static readonly ILogger Loggger = Logger.Register(typeof(TicketRegisterRepository));

        public IEnumerable<ProjectEntities> GetProject(int? ClientID)
        {
            List<ProjectEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("ClientID", ClientID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(TicketRegisterQueries.GetProject, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new ProjectEntities
                            {
                                ProjectID = row.Field<int?>("ProjectID"),
                                ProjectCode = row.Field<string>("ProjectCode"),
                                ProjectName = row.Field<string>("ProjectName")
                            }).ToList();
            }
            return data;
        }

        public IEnumerable<PriorityEntities> GetPriority()
        {
            List<PriorityEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(TicketRegisterQueries.GetPriority, CommandType.StoredProcedure);
                data = dt.AsEnumerable()
                           .Select(row => new PriorityEntities
                           {
                               PriorityID = row.Field<int>("PriorityID"),
                               Priority = row.Field<string>("Priority"),
                           }).ToList();
            }
            return data;
        }

        public IEnumerable<StatusEntities> GetStatus()
        {
            List<StatusEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DataTable dt = dbHelper.ExecuteDataTable(TicketRegisterQueries.GetStatus, CommandType.StoredProcedure);
                data = dt.AsEnumerable()
                           .Select(row => new StatusEntities
                           {
                               StatusID = row.Field<int>("StatusID"),
                               Status = row.Field<string>("Status"),
                               Description = row.Field<string>("Description"),
                           }).ToList();
            }
            return data;
        }

        public TicketRegisterEntities SaveTicket(TicketRegisterEntities entity)
        {
            int NewTicketID = 0;
            using (DBHelper dbHelper = new DBHelper())
            {
                IDbTransaction transaction = dbHelper.BeginTransaction();
                try
                {
                    DBParameterCollection paramCollection = new DBParameterCollection();

                    paramCollection.Add(new DBParameter("TicketID", entity.TicketID, DbType.Int32));
                    paramCollection.Add(new DBParameter("TicketDate", entity.TicketDate, DbType.DateTime));
                    paramCollection.Add(new DBParameter("ClientID", entity.ClientID, DbType.String));
                    paramCollection.Add(new DBParameter("ProjectID", entity.ProjectID, DbType.Int32));
                    paramCollection.Add(new DBParameter("Subject", entity.Subject, DbType.String));
                    paramCollection.Add(new DBParameter("Description", entity.Description, DbType.String));
                    paramCollection.Add(new DBParameter("PriorityID", entity.PriorityID, DbType.Int32));
                    paramCollection.Add(new DBParameter("AllocatedTo", entity.AllocatedTo, DbType.Int32));
                    paramCollection.Add(new DBParameter("StatusID", entity.StatusID, DbType.Int32));
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

                    paramCollection.Add(new DBParameter("NewTicketID", NewTicketID, DbType.Int32, ParameterDirection.Output));
                    paramCollection.Add(new DBParameter("TicketNo", entity.TicketNo, DbType.String, 50, ParameterDirection.Output));

                    var parameterList = dbHelper.ExecuteNonQueryForOutParameter(TicketRegisterQueries.SaveTicket, paramCollection, CommandType.StoredProcedure);

                    entity.TicketID = Convert.ToInt32(parameterList["NewTicketID"]);
                    entity.TicketNo = parameterList["TicketNo"].ToString();

                    dbHelper.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    dbHelper.RollbackTransaction(transaction);
                    Loggger.LogError("Error in TicketRegisterRepository method of SaveTicket request Repository : parameter :" + Environment.NewLine + ex.StackTrace);
                }
            }
            return entity;
        }

        public IEnumerable<TicketRegisterEntities> GetTicket(int? statusID)
        {
            List<TicketRegisterEntities> data = null;
            using (DBHelper dbHelper = new DBHelper())
            {
                DBParameter param = new DBParameter("StatusID", statusID, DbType.Int32);
                DataTable dt = dbHelper.ExecuteDataTable(TicketRegisterQueries.GetTicket, param, CommandType.StoredProcedure);

                data = dt.AsEnumerable()
                            .Select(row => new TicketRegisterEntities
                            {
                                TicketID = row.Field<int>("TicketID"),
                                TicketNo = row.Field<string>("TicketNo"),
                                strTicketDate = row.Field<DateTime?>("TicketDate")?.ToString("dd-MMM-yyyy") ?? string.Empty,
                                Subject = row.Field<string>("Subject"),
                                Description = row.Field<string>("Description"),
                                PriorityID = row.Field<int?>("PriorityID"),
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

    }
}
