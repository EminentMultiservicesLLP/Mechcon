using BISERPBusinessLayer.Entities.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Ticket.Interfaces
{
    public interface IDashboardRepository
    {
        IEnumerable<TicketStatusSummaryEntities> GetTicketStatusSummary(string financialYear);
        IEnumerable<TicketStatusRptEntities> GetTicketStatusRpt(string financialYear);
    }
}
