using BISERPBusinessLayer.Entities.Purchase;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Purchase.Interfaces
{
    public interface IRequestForQuoteDetailRepository
    {
        int CreateRequestForQuoteDetails(int IndentId, RequestForQuoteDetailEntities entity, DBHelper dbhelper);
        List<RequestForQuoteDetailEntities> GetRequestForQuoteDetailById(int IndentId);
        bool UpdateRequestForQuoteAuthQty(RequestForQuoteEntities mainentity, RequestForQuoteDetailEntities entity, DBHelper dbhelper);
    }
}
