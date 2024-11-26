using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.QueryCollection.Ticket
{
    public class TicketActivityQueries
    {
        public const string GetTicketForActivity = "sp_TKT_GetTicketForActivity";
        public const string GetActivityID = "sp_TKT_GetActivityID";
        public const string SaveActivity = "sp_TKT_SaveActivity";
        public const string GetActivity = "sp_TKT_GetActivity";
    }
}
