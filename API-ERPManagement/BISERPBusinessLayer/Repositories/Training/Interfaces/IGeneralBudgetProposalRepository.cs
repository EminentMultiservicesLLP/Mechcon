using System.Collections.Generic;
using BISERPBusinessLayer.Entities.Training;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface IGeneralBudgetProposalRepository
    {
        bool CreateGeneralBudgetProposal(GeneralBudgetProposalEntity entity);
        IEnumerable<GeneralBudgetProposalDtEntity> GetbudgetProposalByMonth(int budgetMonth, int budgetYear);
        bool CheckDuplicateItem(string code);
        IEnumerable<GeneralBudgetProposalEntity> GetGeneralBudgetProposal();
        IEnumerable<GeneralBudgetProposalDtEntity> GetAllGeneralBudgetProposalDtl(int budgetId);
        bool AuthCancelGeneralBudgetProposal(GeneralBudgetProposalEntity entity);
    }

  
}
