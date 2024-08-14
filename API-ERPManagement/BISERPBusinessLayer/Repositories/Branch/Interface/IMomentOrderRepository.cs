using BISERPBusinessLayer.Entities.Branch;
using BISERPDataLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Interface
{
    public interface IMomentOrderRepository
    {
        IEnumerable<MomentOrderEntity> GetAllMomentOrderList(int userId);
        int CreateMomentOrder(MomentOrderEntity entity);
        MomentOrderEntity UpdateMomentOrder(MomentOrderEntity entity);
        MomentOrderEntity ClearBatchFullOrderacceptance(MomentOrderEntity entity);

        //bool CheckAcumadeteQty(int orderId);
        //bool CheckTraineeQty(MomentOrderEntity entity);
    }
}
