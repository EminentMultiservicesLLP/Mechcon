using System;
using System.Collections.Generic;
using BISERPBusinessLayer.Entities.Common;
using BISERPBusinessLayer.Entities.Masters;

namespace BISERPBusinessLayer.Repositories.Common.Interface
{
    public interface IPettyCashRepository
    {
        int CreatePettyCashTransaction(PettyCashEntity entity);
        IEnumerable<PettyCashEntity> GetPettyCashDEPOSITE(string fromDate, string ToDate, int Type);
        IEnumerable<PettyCashEntity> GetPettyCashWITHDRAWAL( string fromDate, string ToDate,int Type);
        bool UpdatePettyCash(PettyCashEntity entity);
    }
}
