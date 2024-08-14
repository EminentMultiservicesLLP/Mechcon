using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IRequestForQuoteCommonRepository
    {
        RequestForQuoteEntities CreateRequestForQuote(RequestForQuoteEntities entity);
        bool UpdateRequestForQuote(RequestForQuoteEntities entity);
        List<RFQDeliveryTermEntities> GetRFQDeliveryTerms(int Id);
        List<RFQPaymentTermEntities> GetRFQPaymenterms(int Id);
        bool AuthCancelRequestForQuote(RequestForQuoteEntities entity);
        bool AutoRFQGeneration(int PRId, DBHelper dbhelper);
    }
}
