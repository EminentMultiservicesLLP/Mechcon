using BISERPBusinessLayer.Entities.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Ticket.Interfaces
{
    public interface ITicketRegisterRepository
    {
        IEnumerable<ProjectEntities> GetProject(int? ClientID);
        IEnumerable<PriorityEntities> GetPriority();
        IEnumerable<StatusEntities> GetStatus();
        TicketRegisterEntities SaveTicket(TicketRegisterEntities model);
        IEnumerable<TicketRegisterEntities> GetTicket(int? statusID);
    }
}
