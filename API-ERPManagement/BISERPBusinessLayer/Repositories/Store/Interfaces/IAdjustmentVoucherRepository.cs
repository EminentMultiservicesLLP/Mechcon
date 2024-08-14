using BISERPBusinessLayer.Entities.Store;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Store.Interfaces
{
    public interface IAdjustmentVoucherRepository
    {
        AdjustmentVoucherEntity CreateNewEntry(AdjustmentVoucherEntity entity, DBHelper dbhelper);
    }
}
