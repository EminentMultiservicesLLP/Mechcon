using System.Collections.Generic;
using BISERPBusinessLayer.Entities.Training;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface IBudgetHeadsRepository
    {
        IEnumerable<BudgetHeadsEntity> GetAllBudgetHeads();
        int CreateBudgetHeads(BudgetHeadsEntity entity);
        bool UpdateBudgetHeads(BudgetHeadsEntity entity);
        bool CheckDuplicateItem(string code,string budgetHead);
        bool CheckDuplicateupdate(string code, int id);
    }
}
