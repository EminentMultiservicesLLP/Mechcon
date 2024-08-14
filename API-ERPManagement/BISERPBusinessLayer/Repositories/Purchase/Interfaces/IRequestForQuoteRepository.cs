using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IRequestForQuoteRepository
    {
        RequestForQuoteEntities CreateRequestForQuote(RequestForQuoteEntities entity, DBHelper dbhelper);
        bool UpdateRequestForQuote(RequestForQuoteEntities entity, DBHelper dbhelper);
        IEnumerable<RequestForQuoteEntities> GetAllRequestForQuote();
        RequestForQuoteEntities GetRequestForQuoteById(int Id);
        bool AuthCancelRequestForQuote(RequestForQuoteEntities entity, DBHelper dbhelper);
        IEnumerable<RequestForQuoteEntities> GetAuthorizedRequestForQuote(int StoreId);
        IEnumerable<RequestForQuoteEntities> RFQforReport();
    }
}
