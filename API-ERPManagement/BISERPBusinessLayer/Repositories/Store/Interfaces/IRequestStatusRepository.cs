using BISERPBusinessLayer.Entities.Purchase;
using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IRequestStatusRepository
    {
        List<RequestStatusEntity> RequestStatusReport(DateTime Fromdate, DateTime Todate, int StoreId, string IndentNo);
        int SaveRequestStatus(RequestStatusEntity entity);
        int UpdateIndentRequestStatus(int RequestId);
        int UpdateIssueRequestStatus(int RequestId);
        int UpdateIssueAuthRequestStatus(int IssueId, DBHelper dbhelper);
        int UpdatePMIndentRequestStatus(int RequestId);
        int UpdatePMAuthRequestStatus(int RequestId);
        int UpdatePORequestStatus(int RequestId);

        bool UpdatePOAuthRequestStatus(PurchaseOrderEntities entity, DBHelper dbhelper);
        int UpdateGRNRequestStatus(int GRNId, DBHelper dbhelper);
        int UpdateGRNAuthRequestStatus(int GRNId, DBHelper dbhelper);

        int UpdateMRRequestStatus(int IssueId, DateTime ReturnDate, DBHelper dbhelper);
        int UpdateMRAuthRequestStatus(int ReturnID);

        int UpdatePMAuthRequestStatus(int p, DBHelper dbhelper);
    }
}
