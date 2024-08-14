using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface IRequestRegisterRepository
    {
        IEnumerable<RequestRegisterEntity> GetAllRequestRegister();
        RequestRegisterEntity CreateRequestRegister(RequestRegisterEntity Entity);
        int UpdateRequestRegister(RequestRegisterEntity Entity);
        IEnumerable<RequestRegisterEntity> GetRequestno(int BranchId);
        RequestRegisterEntity GetRequestDetail(int RequisitionId);
        IEnumerable<RequestRegisterEntity> GetRequestNoDeletion(int BranchId);
        IEnumerable<RequestRegisterEntity> GetRequestRegister(DateTime FromDate, DateTime ToDate, int BranchId, int Status);
        bool UpdRequestAcceptance(RequestRegisterEntity Entity);
        IEnumerable<RequestRegisterEntity> RequestRegisterReport(DateTime FromDate, DateTime ToDate, int BranchId);
        IEnumerable<RequestRegisterEntity> RequestNotification(int DueDays);
    }
}
