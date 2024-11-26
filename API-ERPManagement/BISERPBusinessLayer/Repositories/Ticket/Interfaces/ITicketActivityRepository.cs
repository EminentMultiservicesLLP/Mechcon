using BISERPBusinessLayer.Entities.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Ticket.Interfaces
{
    public interface ITicketActivityRepository
    {
        IEnumerable<TicketRegisterEntities> GetTicketForActivity(int UserID, int? TicketType);
        TicketActivityEntities GetActivityID(int TicketID, int InsertedBy);
        ActivityListEntities SaveActivity(ActivityListEntities model);
        IEnumerable<TicketActivityEntities> GetActivity(int? ticketID);
    }
}
