using BISERPBusinessLayer.Entities.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISERPBusinessLayer.Repositories.Branch.Interface
{
    public interface ICashWithdrawalRepository
    {
        int CreateCashWithdrawal(CashWithdrawalEntity Entity);
    }
}
