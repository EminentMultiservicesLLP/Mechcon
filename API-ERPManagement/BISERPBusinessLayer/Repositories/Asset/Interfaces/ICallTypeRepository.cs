using BISERPBusinessLayer.Entities.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Asset.Interfaces
{
    public interface ICallTypeRepository
    {
        IEnumerable<CallTypeEntity> GetAllCallType();
        int CreateCallType(CallTypeEntity entity);
        bool UpdateCallType(CallTypeEntity entity);
        IEnumerable<CallTypeEntity> GetActiveCallType();
        bool CheckDuplicateItem(string code);
        bool CheckDuplicateupdate(string code, int id);
    }
}
