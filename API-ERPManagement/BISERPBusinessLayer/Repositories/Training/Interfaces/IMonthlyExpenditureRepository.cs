using System;
using BISERPDataLayer.DataAccess;
using BISERPBusinessLayer.Entities.Training;
using System.Collections.Generic;

namespace BISERPBusinessLayer.Repositories.Training.Interfaces
{
    public interface IMonthlyExpenditureRepository
    {
        bool CreateMonthlyExpenditure(MonthlyExpenditureEntity entity);

        //IEnumerable<MonthlyExpenditureDtEntity> GetExpenditureByMonth(DateTime ExpMonth);

        IEnumerable<MonthlyExpenditureDtEntity> GetExpenditureByMonth(int expMonth, int expYear);

        IEnumerable<MonthlyExpenditureDtEntity> GetActualExpence(int expMonth, int expYear, string budgetHead);

    }
}
