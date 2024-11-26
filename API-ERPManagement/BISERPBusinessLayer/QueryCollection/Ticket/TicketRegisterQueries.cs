using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.Ticket
{
    public class TicketRegisterQueries
    {
        public const string GetProject = "sp_TKT_GetProject";
        public const string GetPriority = "sp_TKT_GetPriority";
        public const string GetStatus = "sp_TKT_GetStatus";
        public const string SaveTicket = "sp_TKT_SaveTicket";
        public const string GetTicketActivity = "sp_TKT_GetTicketActivity";
        public const string GetTicket = "sp_TKT_GetTicket";
    }
}
